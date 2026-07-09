namespace CarbonApi.Client.Aggregation;

/// <summary>
/// Интерфейс агрегации метрик.
/// </summary>
public interface IAggregation
{
    /// <summary>
    /// Применяет агрегацию к указанному пути метрики.
    /// </summary>
    /// <param name="path">Исходный путь или выражение метрики.</param>
    /// <returns>Выражение с примененной агрегацией.</returns>
    string Apply(string path);
}