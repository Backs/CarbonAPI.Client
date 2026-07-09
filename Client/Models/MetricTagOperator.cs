namespace CarbonApi.Client.Models;

/// <summary>
/// Операторы сравнения для тегов метрик.
/// </summary>
public enum MetricTagOperator
{
    /// <summary>Равно (=)</summary>
    Equal,
    /// <summary>Соответствует регулярному выражению (=~)</summary>
    Like,
    /// <summary>Не равно (!=)</summary>
    NotEqual,
    /// <summary>Не соответствует регулярному выражению (!=~)</summary>
    NotLike
}