using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CarbonApi.Client;

/// <summary>
/// Представляет серию данных метрики.
/// </summary>
public class Metric : IStatisticKey
{
    /// <summary>Имя метрики.</summary>
    public string Name { get; }

    /// <summary>Точки данных метрики.</summary>
    public DataPoint[] DataPoints { get; }

    /// <summary>Дата получения данных.</summary>
    public DateTime AcquireDate { get; }

    /// <summary>
    /// Создает новый экземпляр метрики.
    /// </summary>
    /// <param name="name">Имя метрики.</param>
    /// <param name="dataPoints">Точки данных.</param>
    /// <param name="acquireDate">Дата получения.</param>
    public Metric(string name, DataPoint[] dataPoints, DateTime acquireDate)
    {
        this.Name = NormalizeName(name);
        this.DataPoints = dataPoints;
        this.AcquireDate = acquireDate;
    }

    /// <summary>
    /// Возвращает последнюю точку данных, значение которой не равно null.
    /// </summary>
    /// <returns>Точка данных или null, если не найдено.</returns>
    public DataPoint? GetLastNotNull()
    {
        return this.DataPoints?
            .Where(x => x.Value.HasValue)
            .OrderByDescending(x => x.DateTime)
            .FirstOrDefault();
    }

    private static readonly Regex CharsRegex = new Regex("[^a-zA-Z0-9.]", RegexOptions.Compiled);

    private static string NormalizeName(string value)
    {
        return CharsRegex.Replace(value, "_");
    }
}