using Quizi.Web.Models.Common;

namespace Quizi.Web.Models.Authentication
{
    public class Permission : BaseEntity
    {
        public string PermissionName { get; set; }
        public string Code { get; set; }
        public Guid? ParentPermission { get; set; }


    }
}
