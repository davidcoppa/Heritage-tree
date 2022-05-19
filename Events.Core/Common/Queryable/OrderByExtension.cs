using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Events.Core.Common.Queryable
{
    public static class OrderByExtension
    {

        public static IQueryable<t> OrderBy<t>(this IQueryable<t> query, string column, string order) //where T : Entity
        {
            order ??= "asc";
            column ??= "Id";


            if (order.Contains("asc"))
            {
                order = "OrderBy";

            }
            else
            {
                order = "OrderByDescending";

            }
            ParameterExpression parameter = Expression.Parameter(query.ElementType, "");

            MemberExpression property = Expression.Property(parameter, column);
            LambdaExpression lambda = Expression.Lambda(property, parameter);

            Expression methodCallExpression = Expression.Call(typeof(IQueryable), order,
                                 new Type[] { query.ElementType, property.Type },
                                 query.Expression, Expression.Quote(lambda));

            var ret = query.Provider.CreateQuery<t>(methodCallExpression);

            return ret;

        }

    }

    //public static class WhereExtension
    //{

    //    public static IQueryable<t> Where<t>(this IQueryable<t> query, string search) //where T : Entity
    //    {

    //        if (string.IsNullOrEmpty(search))
    //        {
    //            Task<List<t>>? pp =query.ToListAsync();

    //        }
    //        else
    //        {
            
    //        }
    //        ParameterExpression parameter = Expression.Parameter(query.ElementType, "");

    //        MemberExpression property = Expression.Property(parameter, column);
    //        LambdaExpression lambda = Expression.Lambda(property, parameter);

    //        Expression methodCallExpression = Expression.Call(typeof(IQueryable), order,
    //                             new Type[] { query.ElementType, property.Type },
    //                             query.Expression, Expression.Quote(lambda));

    //        var ret = query.Provider.CreateQuery<t>(methodCallExpression);

    //        return ret;

    //    }

    //}




}
