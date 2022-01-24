using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskLightlamp.models
{
    public class Dbcontextq : IdentityDbContext<ApplicationUser>
    {
        public Dbcontextq(DbContextOptions options) : base(options)
        {
        }
        public DbSet<products> products { get; set; }
      
      
    }
   
}
