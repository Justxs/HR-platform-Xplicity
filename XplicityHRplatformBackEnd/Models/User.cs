using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace XplicityHRplatformBackEnd.Models
{
    public class User : IdentityUser
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
