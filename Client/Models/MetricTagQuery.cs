using System;
using System.Collections.Generic;
using CarbonApi.Client.Aggregation;

namespace CarbonApi.Client.Models;

/// <summary>
/// Представляет запрос к CarbonAPI на основе тегов.
/// </summary>
public sealed class MetricTagQuery : IAggregationContainer<MetricTagQuery>
{
    private readonly List<MetricTag> tags;

    private readonly List<IAggregation> aggregations;

    public MetricTagQuery(List<MetricTag> tags, List<IAggregation> aggregations, string? period)
    {
        this.tags = tags;
        this.aggregations = aggregations;
        this.Period = period;
    }

    /// <summary>
    /// Возвращает или устанавливает период времени для запроса.
    /// </summary>
    public string? Period { get; private set; }

    /// <summary>
    /// Добавляет тег к условиям поиска метрик.
    /// </summary>
    public MetricTagQuery AddTag(MetricTag tag)
    {
        if (tag == null)
        {
            throw new ArgumentNullException(nameof(tag));
        }

        this.tags.Add(tag);
        return this;
    }

    /// <summary>
    /// Добавляет условие фильтрации по тегу.
    /// <para>Пример: <c>AddTag("host", "server-1")</c> -> <c>{host="server-1"}</c></para>
    /// </summary>
    public MetricTagQuery AddTag(string key, string value, MetricTagOperator op = MetricTagOperator.Equal)
    {
        this.tags.Add(new MetricTag(key, value, op));
        return this;
    }

    /// <summary>
    /// Добавляет агрегацию (Graphite-функцию) к запросу.
    /// <para>Обычно вызывается автоматически через методы расширения, такие как <c>.Sum()</c> или <c>.Alias()</c>.</para>
    /// </summary>
    public MetricTagQuery AddAggregation(IAggregation aggregation)
    {
        if (aggregation == null)
        {
            throw new ArgumentNullException(nameof(aggregation));
        }

        this.aggregations.Add(aggregation);
        return this;
    }

    /// <summary>
    /// Устанавливает относительный период времени, за который нужно получить данные.
    /// <para>Пример: <c>WithPeriod("1h")</c>, <c>WithPeriod("30m")</c>, <c>WithPeriod("7d")</c>.</para>
    /// </summary>
    /// <param name="period">Строка периода в формате Graphite.</param>
    public MetricTagQuery WithPeriod(string period)
    {
        this.Period = period;
        return this;
    }

    public override string ToString()
    {
        var result = $"seriesByTag({string.Join(", ", this.tags)})";

        return this.aggregations.Count switch
        {
            0 => result,
            1 => this.aggregations[0].Apply(result),
            _ => new CompositeAggregation(this.aggregations.AsReadOnly()).Apply(result)
        };
    }
}