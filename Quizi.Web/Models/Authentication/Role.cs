using Quizi.Web.Models.Common;

namespace Quizi.Web.Models.Authentication
{
    public class Role : BaseEntity
    {
        // Role core
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescrition { get; set; }
        public bool RoleActive { get; set; }

    }
}
