# LightBulb
How I build Thos Project
FirstStep Do Authorization With JWT   

1-install 
*EntityFrameworkCore.Authentication.JwtBearer
*EntityFrameworkcore.entityFrameworkCore
*EntityFrameworkcore.entityFrameworkCore.Design
*EntityFrameworkcore.entityFrameworkCore.sqlserver
*EntityFrameworkcore.visualstudio.web.codegenerated.design
*EntityFrameworkcore.identityModel.Tokens.jwt

2-IN AppsettingJsonFile
add   "JWT": {
    "Key": "S/BXt+FPnrEEqyyzP0sVjCIWKrO1uIgg1a86j9vqhPM=",
    "Issuer": "security",
    "Audience": "User",
    "DurationInDays": 30

  }

3-Add NewFolder Add Class for Jwt And put properties 
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }

        public double DurationInDays { get; set; }

    }
    
    
 4-In Startup Add configuration for Jwt
 services.Configure<Jwt>(Configuration.GetSection("JWT"));
    
 5-add Authertiction Class inhertied from identityUser
    to  add more property
     public class ApplicationUser:IdentityUser
    {
        [Required,MaxLength(30)]
        public string FirstName { get; set; }
        [Required,MaxLength(30)]
        public string LastName { get; set; }
        [Required,MaxLength(3)]
        public int Age { get; set; }
        [MaxLength(40)]
        public string Country { get; set; }

       
        public  List<products> Products { get; set; }
    }
    
    
    6-do NewDbcontext class inhertied from  Dbcontext
        public class Dbcontextq : IdentityDbContext<ApplicationUser>
    {
        public Dbcontextq(DbContextOptions options) : base(options)
        {
        }
        public DbSet<products> products { get; set; }
      
      
    }
    
    
    7-InstartApp add configuration for New Authertiction Class
     services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<Dbcontextq>();
   
     * add configuration for Dbcontext
    services.AddDbContext<Dbcontextq>(options =>
            options.UseSqlServer(
             Configuration.GetConnectionString("con")));
    
    * add configuration for Authentication and jwt
           services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(
                o => {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = Configuration["JW:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwt:Key"]))
                    };
  
               } );  

    8-add Interface IAuthservices  in services folder
    there is two function without implementation for register and Login 
        public interface IAuthservices
    {
        Task<Authmodel> RegisterAsync(RegisterModel modil);
        Task<Authmodel> GetTokenAsync(TokenRequestModel model);
        
    }
    
    
    ** add  Authservices class inherted from  IAuthservices  in services folder
        public class Authservices : IAuthservices
    {
      
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Jwt _jwt;

        public Authservices(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<Jwt> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }

        public async Task<Authmodel> RegisterAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new Authmodel { message = "Email is already registered!" };

            if (await _userManager.FindByNameAsync(model.UserName) is not null)
                return new Authmodel { message = "Username is already registered!" };

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Age=model.Age,
                Country=model.Country
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new Authmodel { message = errors };
            }

            await _userManager.AddToRoleAsync(user, "User"); //make any register become user

            var jwtSecurityToken = await CreateJwtToken(user);

            return new Authmodel
            {
                Email = user.Email,
                Expireon = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = user.UserName
            };
        }

        public async Task<Authmodel> GetTokenAsync(TokenRequestModel model)
        {
            var authModel = new Authmodel();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.message = "Email or Password is incorrect!";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.UserName = user.UserName;
            authModel.Expireon = jwtSecurityToken.ValidTo;
            authModel.Roles = rolesList.ToList();

            return authModel;
        }

  

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

    }
    
    
    9-In Models We Have
      Model
     product
     Application User
     ViewMode as
     ProductviewModel
    AuthModel
    registerModel
    TokenRequestModel
    
    
    
    10-IN AuthContoroller We Use Authservices instaded of Dbcontext to make functions
    In HomeController Make Simple Crud operation

    
