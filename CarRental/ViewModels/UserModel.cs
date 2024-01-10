using System;
namespace CarRental.ViewModels
{
    public class UserModel
    {
        public string Id { get; set; }

        public string? FullName { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string PhotoUrl { get; set; }

        public string Role { get; set; }
        public string City { get; internal set; }
    }

}

