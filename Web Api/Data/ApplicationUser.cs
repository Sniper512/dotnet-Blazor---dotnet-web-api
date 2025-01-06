using Microsoft.AspNetCore.Identity;

namespace Web_Api.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
