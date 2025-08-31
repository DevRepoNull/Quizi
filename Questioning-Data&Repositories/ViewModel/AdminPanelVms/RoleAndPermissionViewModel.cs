using System.Diagnostics.CodeAnalysis;

namespace Questioning_Data_Repositories.ViewModel.AdminPanelVms
{
    public class RoleListFromAdminPanelViewModel
    {
        [Display(Name = "عنوان نقش")]
        public string RoleName { get; set; }

        [Display(Name = "وضعیت نقش")]
        public bool RolActive { get; set; }

        [Display(Name = "توضیحات")]
        public string RoleDescription { get; set; }

        public string RoleId { get; set; }
    }

    public class AddRoleFromAdminPanelViewModel
    {
        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "تعداد کاراکترهای {0} نمی تواند کمتر از 4 کاراکتر و بیشتر از 20 کاراکتر باشد")]
        [Unicode]
        public string RoleName { get; set; }

        [Display(Name = "توضیحات نقش")]
        [StringLength(300, MinimumLength = 5, ErrorMessage = "تعداد کاراکترهای {0} نمی تواند کمتر از 5 کاراکتر و بیشتر از 300 کاراکتر باشد")]
        [Unicode]
        public string RoleDescription { get; set; }

        [Display(Name = "وضعیت نقش")]
        [Required(ErrorMessage = "لطفا {0} را مشخص کنید")]
        public bool RoleActive { get; set; }
    }

    public class EditRoleFromAdminPanel
    {
        public string RoleId { get; set; }

        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "تعداد کاراکترهای {0} نمی تواند کمتر از 4 کاراکتر و بیشتر از 20 کاراکتر باشد")]
        [Unicode]
        public string RoleName { get; set; }

        [Display(Name = "توضیحات نقش")]
        [StringLength(300, MinimumLength = 5, ErrorMessage = "تعداد کاراکترهای {0} نمی تواند کمتر از 5 کاراکتر و بیشتر از 300 کاراکتر باشد")]
        [Unicode]
        public string RoleDescription { get; set; }

        [Display(Name = "وضعیت نقش")]
        [Required(ErrorMessage = "لطفا {0} را مشخص کنید")]
        public bool RoleActive { get; set; }
    }
}
