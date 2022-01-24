using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskLightlamp.models
{
    public class RegisterModel
    {
        [Required, MaxLength(30)]
        public string FirstName { get; set; }
        [Required, MaxLength(30)]
        public string LastName { get; set; }
        [Required, MaxLength(30)]
        public string UserName { get; set; }
        [Required]
        [StringLength(16, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 5)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string Email { get; set; }
        [Required, MaxLength(30)]
        public string Password { get; set; }
        [Required]
        public int Age { get; set; }
        [MaxLength(40)]
        public string Country { get; set; }
    }
}
