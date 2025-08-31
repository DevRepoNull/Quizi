using System.Diagnostics.CodeAnalysis;

namespace Questioning_Data_Repositories.ViewModel.AdminPanelVms
{
    public class CategoryViewModel
    {
        [AllowNull]
        public ulong CategoryId { get; set; }

        [Display(Name = "عنوان دسته بندی")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} وارد شده نمی تواند کمتر از 3 کاراکتر و بیشتر از 100 کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Unicode]
        public string CategoryName { get; set; }
    }
}
