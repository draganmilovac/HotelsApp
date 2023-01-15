using HotelsApp.Data.Filtering;

namespace HotelsApp.Infrastructure.Extensions
{
    public static class IOrderedEnumerableExtensions
    {
        #region Public methods
        public static async Task<FilterResult<T>> ApplyPagination<T>(this IEnumerable<T> elements,
            PaginationOptions paginationOptions,
            bool forItems = false)
        {
            int pageNumber = 1;
            int pageSize = 1;
            if (paginationOptions != null)
            {
                if (paginationOptions.Page > 0)
                    pageNumber = paginationOptions.Page;
                if (paginationOptions.SizePerPage > 0)
                    pageSize = paginationOptions.SizePerPage;
            }
            var total = elements.Count();
            var query = elements.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            return new FilterResult<T>(query, total);
        }
        #endregion
    }
}
