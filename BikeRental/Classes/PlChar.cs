namespace BikeRental.Classes
{
    public static class PlChar
    {
        public static string ReplaceChar(string _chars)
        {
            return _chars.Replace("Œ", "Ś").Replace("¯", "Ż").Replace("£", "Ł").Replace("Ñ", "Ń").Replace("Ê", "Ę").Replace("Æ", "Ć").Replace("¥", "Ą")
                .Replace("¹", "ą").Replace("³", "ł");
        }
    }
}
