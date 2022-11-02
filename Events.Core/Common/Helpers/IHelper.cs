namespace Events.Core.Common.Helpers
{
    public interface IHelper
    {
        List<T> GetFilter<T>(string sort, string order, string page, string itemsPage, IQueryable<T> data) where T : class;
    }
}
