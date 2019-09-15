using System;

namespace SampleApp.ViewModels
{
    public class ListResponseViewModel
    {
        public int Total { get; set; }
        public object Resources { get; set; }
    }

    public class UserResponseViewModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get => $"{FirstName} {LastName}"; }
        public DateTime DayOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }

        public bool IsActive { get; set; }
    }
}
