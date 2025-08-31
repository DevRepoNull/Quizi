namespace Questioning_Data_Repositories.ViewModel.Account
{
    public class SignInViewModel
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
        public string Email { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "{0} باید حداقل 10 و حداکثر 13 کاراکتر باشد")]
        [Display(Name = "شماره تماس")]
        [RegularExpression(@"^[\d+]+$", ErrorMessage = "لطفاً یک شماره تماس معتبر وارد کنید.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "{0} باید 10 رقم باشد.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "لطفاً یک {0} معتبر وارد کنید.")]
        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "{0} باید حداقل 8 و حداکثر 100 کاراکتر باشد")]
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*[@$!%*?&\\^£`\\\-_\=])(?=.*\d).+$", ErrorMessage = "{0} باید شامل حروف انگلیسی بزرگ و کوچک، علائم خاص، و اعداد باشد")]
        public string Password { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "{0} باید حداقل 8 و حداکثر 100 کاراکتر باشد")]
        [Display(Name = "تکرار رمز عبور")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "{0} وارد شده با {1} مطابقت ندارد")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*[@$!%*?&\\^£`\\\-_\=])(?=.*\d).+$", ErrorMessage = "{0} باید شامل حروف انگلیسی بزرگ و کوچک، علائم خاص، و اعداد باشد")]
        public string RePassword { get; set; }

        // Relational Property ViewModel

        public string FkRoleId { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        [MaxLength(150, ErrorMessage = "{0} وارد شده نمی تواند بیشتر از 150 کاراکتر باشد")]
        [Display(Name = "ایمیل یا نام کاربری")]
        public string UserOrEmail { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "{0} باید حداقل 8 و حداکثر 100 کاراکتر باشد")]
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*[@$!%*?&\\^£`\\\-_\=])(?=.*\d).+$", ErrorMessage = "{0} باید شامل حروف انگلیسی بزرگ و کوچک، علائم خاص، و اعداد باشد")]
        public string Password { get; set; }

        [Display(Name = "مرا بخاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}
