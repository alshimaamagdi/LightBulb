using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskLightlamp.models
{
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
}
