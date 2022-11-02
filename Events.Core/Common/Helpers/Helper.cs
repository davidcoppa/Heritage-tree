using Events.Core.Common.Queryable;
using EventsManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Core.Common.Helpers
{
    public class Helper : IHelper
    {
        public List<T> GetFilter<T>(string sort, string order, string page, string itemsPage, IQueryable<T> data) where T : class
        {

            data = OrderByExtension.OrderBy(data, sort, order);

            int itemsPageInt = int.TryParse(itemsPage, out int items) ? items : int.MaxValue;
            Pagination pagination = new Pagination(data.Count(), itemsPageInt);

            int pageIndex = int.TryParse(page, out int count) ? count : 0;

            List<T> result = data.PagedIndex(pagination, pageIndex).ToList();

            return result;



        }
    }
}
