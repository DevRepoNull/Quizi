namespace Questioning_Data_Repositories.StaticValue
{
    public enum AnswerTypeEnumTypes : byte
    {
        [Display(Name = "لطفا نوع جواب را انتخاب کنید")]
        None = 0,
        [Display(Name = "تستی")]
        Test = 1,
        [Display(Name = "تشریحی")]
        Anatomical = 2,
        [Display(Name = "درست و غلط")]
        TrueOrFalse = 3
    }
}
