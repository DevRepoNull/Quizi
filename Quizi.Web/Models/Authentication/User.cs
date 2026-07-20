using Quizi.Web.Models.Common;

namespace Quizi.Web.Models.Authentication
{
    public class User : BaseEntity
    {
        // User core
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Nationalcode { get; set; }
        public bool IsActive { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Userprofile { get; set; }

        
    }
}
