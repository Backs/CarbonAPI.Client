namespace CarbonApi.Client.Models;

/// <summary>
/// Содержит список доступных функций для агрегации в методе Summarize.
/// </summary>
public static class SummarizeFunc
{
    /// <summary>Сумма.</summary>
    public const string Sum = "sum";
    /// <summary>Среднее значение.</summary>
    public const string Avg = "avg";
    /// <summary>Минимальное значение.</summary>
    public const string Min = "min";
    /// <summary>Максимальное значение.</summary>
    public const string Max = "max";
    /// <summary>Последнее значение.</summary>
    public const string Last = "last";
}