namespace Events.Core.Common.Queryable
{
    public class Pagination
    {
        public int TotalItems { get; private set; } // Equal to PageSize * MaxPage
        public int PageSize { get; private set; } // Number of items per page
        public int MinPage { get; private set; } = 1; // Page index starting from MinPage to MaxPage
        public int MaxPage { get; private set; } //Total pages

        public Pagination(int totalItems, int itemsPerPage)
        {
            if (itemsPerPage < MinPage)
            {
                throw new ArgumentOutOfRangeException(null, $"*** Number of items per page must > 0! ***");
            }

            TotalItems = totalItems;
            PageSize = itemsPerPage;
            MaxPage = CalculateTotalPages(totalItems, itemsPerPage);
        }

        private int CalculateTotalPages(int totalItems, int itemsPerPage)
        {
            int totalPages = totalItems / itemsPerPage;

            if (totalItems % itemsPerPage != 0)
            {
                totalPages++; // Last page with only 1 item left
            }

            return totalPages;
        }

    }
}
