using System.Linq.Expressions;

namespace Events.Core.Common.Queryable
{
    public static class PaginationExtension
    {

        public static IQueryable<t> PagedIndex<t>(this IQueryable<t> query, Pagination pagination, int pageIndex) //where T : Entity
        {
            if (pageIndex < pagination.MinPage || pageIndex > pagination.MaxPage)
            {
                throw new ArgumentOutOfRangeException(null,
                $"*** Page index must >= {pagination.MinPage} and =< { pagination.MaxPage }! * **");
            }

            // Return IQueryable<t> to enable chained-methods calls
            return query
                .Skip(GetSkip(pageIndex, pagination.PageSize))
                .Take(pagination.PageSize);
        }

        private static int GetSkip(int pageIndex, int take)
        {
            return (pageIndex - 1) * take;
        }

    }

   
}
