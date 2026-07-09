namespace CarbonApi.Client.Models;

/// <summary>
/// Вспомогательный класс для формирования строк периодов времени.
/// </summary>
public static class Period
{
    /// <summary>
    /// Создает строку периода в часах (например, "5h").
    /// </summary>
    public static string FromHours(int hours)
    {
        return $"{hours}h";
    }

    /// <summary>
    /// Создает строку периода в минутах (например, "10m").
    /// </summary>
    public static string FromMinutes(int minutes)
    {
        return $"{minutes}m";
    }
}