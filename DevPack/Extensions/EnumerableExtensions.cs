using System.Linq;

namespace System.Collections.Generic
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Group by amount of itens.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source">The souce itens to group</param>
        /// <param name="amount">The amount itens to group</param>
        /// <returns></returns>
        public static IEnumerable<IGrouping<int, TSource>> GroupByAmount<TSource>(this IEnumerable<TSource> source, int amount)
        {
            int rowNumber = 0;

            return source.GroupBy(x =>
            {
                rowNumber++;
                return (int)Math.Ceiling(rowNumber / (decimal)amount);
            });
        }
    }
}
