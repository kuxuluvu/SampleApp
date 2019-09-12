using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.ViewModels
{
    public class UserViewModel
    {
        [MaxLength(50)]
        [MinLength(3)]
        public string Username { get; set; }
        [MaxLength(50)]
        [MinLength(3)]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public DateTime DayOfBirth { get; set; }

        public string FullName
        {
            get => $"{FirstName} {LastName}";
        }
    }
}
