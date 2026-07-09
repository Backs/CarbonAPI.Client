using System;
using System.Collections.Generic;

namespace CarbonApi.Client.Models;

/// <summary>
/// Представляет тег метрики, используемый в фильтрации.
/// </summary>
public sealed class MetricTag
{
    /// <summary>
    /// Создает новый экземпляр тега метрики.
    /// </summary>
    public MetricTag(string key, string value, MetricTagOperator op = MetricTagOperator.Equal)
    {
        Key = key ?? throw new ArgumentNullException(nameof(key));
        Value = value ?? throw new ArgumentNullException(nameof(value));
        Operator = op;
    }

    /// <summary>
    /// Имя тега.
    /// </summary>
    public string Key { get; }

    /// <summary>
    /// Значение тега.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Оператор сравнения.
    /// </summary>
    public MetricTagOperator Operator { get; }

    public override string ToString()
    {
        var op = Operators[Operator];
        return $"'{Key}{op}{Value}'";
    }

    private static readonly Dictionary<MetricTagOperator, string> Operators = new()
    {
        { MetricTagOperator.Equal, "=" },
        { MetricTagOperator.Like, "=~" },
        { MetricTagOperator.NotEqual, "!=" },
        { MetricTagOperator.NotLike, "!=~" }
    };
}