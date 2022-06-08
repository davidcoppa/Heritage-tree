using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;

namespace Events.Core.Common.Queryable
{
    public static class OrderByExtension
    {

        public static IOrderedQueryable<TEntity> OrderingHelper<TEntity>(this IQueryable<TEntity> source, string propertyName, bool descending, bool anotherLevel)
        //(this IQueryable<t> query,  string column,string order) //where T : Entity
        {
            var searchProperty = typeof(TEntity).GetProperty(propertyName);

            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "p");

            //property = "SomeProperty"
            var propertyExpr = Expression.Property(parameter, propertyName);
            var selectorExpr = Expression.Lambda(propertyExpr, parameter);
            Expression queryExpr = source.Expression;

            queryExpr = Expression.Call(
                //type to call method on
                typeof(System.Linq.Queryable),
                //method to call
                (!anotherLevel ? "OrderBy" : "ThenBy") + (descending ? "Descending" : string.Empty),
                //generic types of the order by method
                new Type[] {
                            source.ElementType,
                            searchProperty.PropertyType 
                            },
                //existing expression to call method on
                queryExpr,
                //method parameter, in our case which property to order on
                selectorExpr);


            return (IOrderedQueryable<TEntity>)source.Provider.CreateQuery<TEntity>(queryExpr);

        }

        private static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        {
            return OrderingHelper(source, propertyName, false, false);
        }

        private static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
        {
            return OrderingHelper(source, propertyName, true, false);
        }

        private static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string propertyName)
        {
            return OrderingHelper(source, propertyName, false, true);
        }

        private static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string propertyName)
        {
            return OrderingHelper(source, propertyName, true, true);
        }
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName, string order, bool thenBy = false)
        {
            order ??= "asc";
            propertyName ??= "Id";

            bool direction;

            if (order.Contains("asc"))
            {
                direction = false;
            }
            else
            {
                direction = true;

            }
            return OrderingHelper(source, propertyName, direction, thenBy);

        }

    }



}
