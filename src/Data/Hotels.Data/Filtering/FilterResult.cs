namespace HotelsApp.Data.Filtering
{
    public class FilterResult<T>
    {
        #region Costructors
        public FilterResult()
        {
            Data = new List<T>();
        }

        public FilterResult(IEnumerable<T> data, long totalCount)
        {
            Data = data;
            TotalCount = totalCount;
        }
        #endregion

        #region Properties
        public IEnumerable<T> Data { get; set; }
        public long TotalCount { get; set; }
        #endregion
    }
}
