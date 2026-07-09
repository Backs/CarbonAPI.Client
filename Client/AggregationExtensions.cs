using CarbonApi.Client.Aggregation;

namespace CarbonApi.Client;

public static class AggregationExtensions
{
    extension<T>(T container) where T : IAggregationContainer<T>
    {
        /// <summary>
        /// Присваивает псевдоним (alias) серии метрик.
        /// <para>Пример: <c>series</c> -> <c>alias(series, 'newName')</c></para>
        /// </summary>
        /// <param name="name">Новое имя серии.</param>
        public T Alias(string name) => container.AddAggregation(new AliasAggregation(name));

        /// <summary>
        /// Формирует имя серии на основе указанных индексов узлов в исходном пути метрики.
        /// <para>Пример: <c>a.b.c.d</c> -> <c>aliasByNode(a.b.c.d, 0, 2)</c> -> <c>a.c</c></para>
        /// </summary>
        /// <param name="nodeIndices">Индексы узлов (начиная с 0), которые составят новое имя.</param>
        public T AliasByNode(params int[] nodeIndices) => container.AddAggregation(new AliasByNodeAggregation(nodeIndices));

        /// <summary>
        /// Формирует имя серии на основе значений указанных тегов.
        /// <para>Пример: <c>series{host=h1, app=a1}</c> -> <c>aliasByTags(series, 'host')</c> -> <c>h1</c></para>
        /// </summary>
        /// <param name="tags">Список тегов, значения которых станут именем серии.</param>
        public T AliasByTags(params string[] tags) => container.AddAggregation(new AliasByTagsAggregation(tags));

        /// <summary>
        /// Изменяет имя серии с помощью поиска по регулярному выражению и замены.
        /// <para>Пример: <c>aliasSub(series, '^.*\.(\w+)$', '\1')</c></para>
        /// </summary>
        /// <param name="searchPattern">Регулярное выражение для поиска части имени.</param>
        /// <param name="replacePattern">Шаблон для замены (поддерживает группы захвата, например \1).</param>
        public T AliasSub(string searchPattern, string replacePattern) => container.AddAggregation(new AliasSubAggregation(searchPattern, replacePattern));

        /// <summary>
        /// Вычисляет значение каждой точки серии как процент от значения другой метрики в тот же момент времени.
        /// <para>Пример: <c>series</c> -> <c>asPercent(series, total.metric.path)</c></para>
        /// </summary>
        /// <param name="totalMetricPath">Путь к метрике, которая принимается за 100%.</param>
        public T AsPercent(string totalMetricPath) => container.AddAggregation(new AsPercentAggregation(totalMetricPath));

        /// <summary>
        /// Группирует серии по указанным тегам и объединяет их внутри каждой группы с помощью агрегирующей функции.
        /// <para>Пример: <c>groupByTags(series, 'sum', 'environment')</c></para>
        /// </summary>
        /// <param name="func">Функция агрегации (sum, avg, max, min и др.).</param>
        /// <param name="tags">Теги, по которым происходит группировка.</param>
        public T GroupByTags(string func, params string[] tags) => container.AddAggregation(new GroupByTagsAggregation(func, tags));

        /// <summary>
        /// Из множества серий выбирает одну, значения которой в каждый момент времени являются максимальными среди всех серий.
        /// <para>Пример: <c>series1, series2</c> -> <c>maxSeries(series1, series2)</c> -> <c>max(series1[t], series2[t])</c></para>
        /// </summary>
        public T MaxSeries() => container.AddAggregation(new MaxSeriesAggregation());

        /// <summary>
        /// Вычисляет скользящее среднее серии за указанный период времени. Сглаживает график.
        /// <para>Пример: <c>series</c> -> <c>movingAverage(series, '5min')</c></para>
        /// </summary>
        /// <param name="period">Период времени (например, '1h', '30s').</param>
        public T MovingAverage(string period) => container.AddAggregation(new MovingAverageAggregation(period));

        /// <summary>
        /// Вычисляет скользящую сумму значений серии за указанный период времени.
        /// <para>Пример: <c>series</c> -> <c>movingSum(series, '10min')</c></para>
        /// </summary>
        /// <param name="period">Период времени (например, '1h', '30s').</param>
        public T MovingSum(string period) => container.AddAggregation(new MovingSumAggregation(period));

        /// <summary>
        /// Суммирует значения всех серий в каждой точке, возвращая одну результирующую серию.
        /// <para>Пример: <c>series1, series2</c> -> <c>sum(series1, series2)</c> -> <c>series1[t] + series2[t]</c></para>
        /// </summary>
        public T Sum() => container.AddAggregation(new SumAggregation());

        /// <summary>
        /// Суммирует серии, схлопывая указанные части пути. Полезно при использовании масок.
        /// <para>Пример: <c>servers.*.cpu</c> -> <c>sumSeriesWithWildcards(servers.*.cpu, 1)</c> -> суммирует CPU по всем серверам.</para>
        /// </summary>
        /// <param name="nodeIndices">Индексы узлов пути, по которым будет произведено суммирование.</param>
        public T SumSeriesWithWildcards(params int[] nodeIndices) => container.AddAggregation(new SumSeriesWithWildCadsAggregation(nodeIndices));

        /// <summary>
        /// Разделяет данные на временные интервалы (bucket) и применяет функцию агрегации к каждому интервалу.
        /// <para>Пример: <c>series</c> -> <c>summarize(series, '1h', 'avg')</c></para>
        /// <para>Все точки за каждый час заменяются одной точкой с их средним значением.</para>
        /// </summary>
        /// <param name="summarizePeriod">Интервал времени (например, '1d', '15m').</param>
        /// <param name="summarizeFunc">Функция агрегации (sum, avg, max, min, last).</param>
        public T Summarize(string summarizePeriod, string summarizeFunc) => container.AddAggregation(new SummarizeAggregation(summarizePeriod, summarizeFunc));
    }
}