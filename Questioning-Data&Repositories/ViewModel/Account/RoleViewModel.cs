namespace Questioning_Data_Repositories.ViewModel.Account
{
    public class RoleViewModel
    {
        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} مورد نظر حداقل از 3 تا 50 کاراکتر می تواند باشد")]
        public string RoleName { get; set; }

        [Display(Name = "توضیحات  درباره نقش")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "{0} مورد نظر حداقل باید از 10 تا 200 کاراکتر می تواند باشد")]
        public string RoleDescription { get; set; }

        [Display(Name = "فعال بودن نقش")]
        public bool RoleActive { get; set; }
    }
}
