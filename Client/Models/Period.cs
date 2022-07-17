namespace CarbonApi.Client.Models
{
    public static class Period
    {
        public static string FromHours(int hours)
        {
            return $"{hours}h";
        }

        public static string FromMinutes(int minutes)
        {
            return $"{minutes}m";
        }
    }
}
