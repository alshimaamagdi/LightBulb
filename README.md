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
    
    
    7-IstartApp add configuration for New Authertiction Class
     services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<Dbcontextq>();
    
