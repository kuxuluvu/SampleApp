using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
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
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DayOfBirth { get; set; }
        public string ImageUrl { get; set; }
        public string FullName
        {
            get => $"{FirstName} {LastName}";
        }
    }
}
