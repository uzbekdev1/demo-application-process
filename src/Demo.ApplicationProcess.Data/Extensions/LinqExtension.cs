using System.Linq;
using System.Linq.Expressions;

namespace Demo.ApplicationProcess.Data.Extensions
{
    public static class LinqExtension
    {

        /// <summary>
        /// Order by props
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderByFx<T>(this IQueryable<T> source, string sort, string order)
        {
            if (string.IsNullOrWhiteSpace(sort))
            {
                return source;
            }

            var parameter = Expression.Parameter(source.ElementType, "");
            var property = Expression.Property(parameter, sort);
            var lambda = Expression.Lambda(property, parameter);
            var methodName = order == "Desc" ? "OrderByDescending" : "OrderBy";
            var methodCallExpression = Expression.Call(typeof(Queryable), methodName, new[] { source.ElementType, property.Type }, source.Expression, Expression.Quote(lambda));

            return source.Provider.CreateQuery<T>(methodCallExpression);
        }

    }
}
