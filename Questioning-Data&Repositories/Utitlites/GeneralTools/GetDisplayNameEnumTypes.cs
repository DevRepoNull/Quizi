namespace Questioning_Data_Repositories.Utitlites.GeneralTools
{
    public static class GetDisplayNameEnumTypes
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                .GetField(enumValue.ToString())?
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .FirstOrDefault() is DisplayAttribute displayAttribute
                ? displayAttribute.Name
                : enumValue.ToString();
        }
    }
}
