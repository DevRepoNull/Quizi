namespace Questioning_Data_Repositories.Utitlites.GeneralTools
{
    public static class NameGenerator
    {
        public static string GenerateUniqName()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
