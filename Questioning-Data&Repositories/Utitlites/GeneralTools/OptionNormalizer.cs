namespace Questioning_Data_Repositories.Utitlites.GeneralTools
{
    public static class OptionNormalizer
    {
        public static string NormalizeOptionId(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            input = input.Trim();

            if (input == "1" || input == "۱" || input == "١") return "1";
            if (input == "2" || input == "۲" || input == "٢") return "2";
            if (input == "3" || input == "۳" || input == "٣") return "3";
            if (input == "4" || input == "۴" || input == "٤") return "4";

            if (input.Equals("A", StringComparison.OrdinalIgnoreCase)) return "1";
            if (input.Equals("B", StringComparison.OrdinalIgnoreCase)) return "2";
            if (input.Equals("C", StringComparison.OrdinalIgnoreCase)) return "3";
            if (input.Equals("D", StringComparison.OrdinalIgnoreCase)) return "4";

            return input;
        }
    }
}
