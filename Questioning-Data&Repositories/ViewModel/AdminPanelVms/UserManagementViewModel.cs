using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace Questioning_Data_Repositories.ViewModel.AdminPanelVms
{
    public class UserListFromAdminPanelViewModel
    {
        [AllowNull]
        public string UserId { get; set; }

        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Display(Name = "آدرس ایمیل")]
        public string EmailAddress { get; set; }

        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }

        [Display(Name = "شماره تماس")]
        public string PhoneNumber { get; set; }

        [Display(Name = "پروفایل کاربر")]
        public string ProfileImageName { get; set; }

        [Display(Name = "فعال بودن کاربر")]
        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        [Display(Name = "وضعیت نقش")] 
        public string RolTitle { get; set; }
    }

    public class CreateUserFromAdminPanelViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} باید حداقل 3 کاراکتر و حداکثر 50 کاراکتر باشد")]
        [RegularExpression(@"^[\u0600-\u06FF\s]+$", ErrorMessage = "لطفاً یک {0} معتبر وارد کنید (فقط حروف فارسی).")]
        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} باید حداقل 3 کاراکتر و حداکثر 50 کاراکتر باشد")]
        [RegularExpression(@"^[\u0600-\u06FF\s]+$", ErrorMessage = "لطفاً یک {0} معتبر وارد کنید (فقط حروف فارسی).")]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "{0} باید حداقل 5 و حداکثر 150 کاراکتر باشد")]
        [Display(Name = "نام کاربری")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "{0} باید دارای حروف انگلیسی و عدد باشد.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "{0} باید حداقل 6 و حداکثر 150 کاراکتر باشد.")]
        [EmailAddress(ErrorMessage = "آدرس ایمیل وارد شده معتبر نمی‌باشد، لطفاً یک آدرس ایمیل معتبر وارد کنید.")]
        [RegularExpression(@"^[\w-\.]+@(gmail\.com|yahoo\.com|outlook\.com)$", ErrorMessage = "فقط ایمیل‌های با پسوندهای gmail.com، yahoo.com و outlook.com معتبر هستند.")]
        [Display(Name = "آدرس ایمیل")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "{0} باید 10 رقم باشد.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "لطفاً یک {0} معتبر وارد کنید.")]
        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "{0} باید حداقل 10 و حداکثر 13 کاراکتر باشد")]
        [Display(Name = "شماره تماس")]
        [RegularExpression(@"^[\d+]+$", ErrorMessage = "لطفاً یک شماره تماس معتبر وارد کنید.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "{0} باید حداقل 8 و حداکثر 100 کاراکتر باشد")]
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*[@$!%*?&\\^£`\\\-_\=])(?=.*\d).+$", ErrorMessage = "{0} باید شامل حروف انگلیسی بزرگ و کوچک، علائم خاص، و اعداد باشد")]
        public string Password { get; set; }

        //public string UserProfileName { get; set; }

        [Display(Name = "عکس پروفایل کاربر")]
        public IFormFile UserProfileUpload { get; set; }

        [Display(Name = "وضعیت کاربر")]
        public bool IsActive { get; set; }

        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public string FkRoleId { get; set; }

        //public Role Role { get; set; }
    }

    public class EditUserFromAdminPanelViewModel
    {
        [AllowNull]
        public string UserId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} باید حداقل 3 کاراکتر و حداکثر 50 کاراکتر باشد")]
        [RegularExpression(@"^[\u0600-\u06FF\s]+$", ErrorMessage = "لطفاً یک {0} معتبر وارد کنید (فقط حروف فارسی).")]
        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} باید حداقل 3 کاراکتر و حداکثر 50 کاراکتر باشد")]
        [RegularExpression(@"^[\u0600-\u06FF\s]+$", ErrorMessage = "لطفاً یک {0} معتبر وارد کنید (فقط حروف فارسی).")]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "{0} باید حداقل 5 و حداکثر 150 کاراکتر باشد")]
        [Display(Name = "نام کاربری")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "{0} باید دارای حروف انگلیسی و عدد باشد.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "{0} باید حداقل 6 و حداکثر 150 کاراکتر باشد.")]
        [EmailAddress(ErrorMessage = "آدرس ایمیل وارد شده معتبر نمی‌باشد، لطفاً یک آدرس ایمیل معتبر وارد کنید.")]
        [RegularExpression(@"^[\w-\.]+@(gmail\.com|yahoo\.com|outlook\.com)$", ErrorMessage = "فقط ایمیل‌های با پسوندهای gmail.com، yahoo.com و outlook.com معتبر هستند.")]
        [Display(Name = "آدرس ایمیل")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "{0} باید 10 رقم باشد.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "لطفاً یک {0} معتبر وارد کنید.")]
        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "{0} باید حداقل 10 و حداکثر 13 کاراکتر باشد")]
        [Display(Name = "شماره تماس")]
        [RegularExpression(@"^[\d+]+$", ErrorMessage = "لطفاً یک شماره تماس معتبر وارد کنید.")]
        public string PhoneNumber { get; set; }

        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "{0} باید حداقل 8 و حداکثر 100 کاراکتر باشد")]
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*[@$!%*?&\\^£`\\\-_\=])(?=.*\d).+$", ErrorMessage = "{0} باید شامل حروف انگلیسی بزرگ و کوچک، علائم خاص، و اعداد باشد")]
        public string Password { get; set; }

        [Display(Name = "عکس پروفایل کاربر")]
        public string UserProfileName { get; set; }

        public IFormFile UserProfileUpload { get; set; }

        [Display(Name = "وضعیت کاربر")]
        public bool IsActive { get; set; }

        public string FkRoleId { get; set; }
    }
}
