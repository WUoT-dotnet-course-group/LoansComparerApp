using LoansComparer.CrossCutting.DTO;
using LoansComparer.CrossCutting.Enums;
using LoansComparer.CrossCutting.Utils;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LoansComparer.DataPersistence.Utils
{
    public static class QueryExtension
    {
        /// <summary>
        /// Sort query entities using sort column header and entity property name mapping.
        /// </summary>
        /// <typeparam name="TResult">Result type where sorting mappings are declared</typeparam>
        /// <typeparam name="TEntity">Entity type on which sorting is applied</typeparam>
        /// <param name="query">Query resulting in collection of <typeparamref name="TEntity"/> entities</param>
        /// <param name="sortOrder">Sorting order/></param>
        /// <param name="sortHeader">Header name of a UI column to be sorted by</param>
        /// <returns>The <paramref name="query"/> ordered by <paramref name="sortHeader"/> UI column in <paramref name="sortOrder"/> order</returns>
        /// <exception cref="ArgumentException"></exception>
        public static IQueryable<TEntity> Sort<TResult, TEntity>(this IQueryable<TEntity> query, SortOrder sortOrder, string sortHeader)
        {
            var resultSortByProperty = typeof(TResult).GetProperties()
                .SingleOrDefault(p => p.GetCustomAttributes(false).OfType<SortHeaderAttribute>().SingleOrDefault()?.HeaderName == sortHeader);
            if (resultSortByProperty is null)
            {
                throw new ArgumentException(Resources.InvalidSortedColumnHeaderName);
            }

            var entitySortByPropertyName = resultSortByProperty.GetCustomAttributes(false).OfType<EntityPropertyNameAttribute>().Single().PropertyName;
            var propertyExpression = ToPropertyByNameExpression<TEntity>(entitySortByPropertyName);

            return sortOrder switch
            {
                SortOrder.Ascending => query.OrderBy(propertyExpression),
                SortOrder.Descending => query.OrderByDescending(propertyExpression),
                _ => query,
            };
        }

        public static async Task<PaginatedResponse<T>> Paginate<T>(this IQueryable<T> query, int pageIndex, int pageSize)
            => new PaginatedResponse<T>
            {
                Items = await query.Skip(pageSize * pageIndex).Take(pageSize).ToListAsync(),
                TotalNumber = await query.CountAsync()
            };

        /// <summary>
        /// Returns property with a name specified in <paramref name="propertyName"/> of an object with a type <typeparamref name="T"/> 
        /// </summary>
        /// <returns>Expression transforming property name in an object of a type <typeparamref name="T"/> to the exact property value</returns>
        private static Expression<Func<T, object>> ToPropertyByNameExpression<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T));
            var propertyAsObject = Expression.Convert(Expression.Property(parameter, propertyName), typeof(object));

            return Expression.Lambda<Func<T, object>>(propertyAsObject, parameter);
        }
    }
}
