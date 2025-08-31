namespace Questioning_Data_Repositories.Utitlites.GeneralTools
{
    public class FixedText
    {
        // Fix Spaces In EmailAddress and ToLower
        public static string FixedEmail(string emailAddress) => emailAddress.Trim().ToLower();
    }
}
