using Microsoft.AspNetCore.Identity;

namespace CarRental.Models
{
    public class User : IdentityUser
    {

        public string  FullName { get; set; }
        public string City { get; set; }
        public string PhotoUrl { get; set; }

  
    }
}
