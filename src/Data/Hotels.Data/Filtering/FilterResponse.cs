namespace HotelsApp.Data.Filtering
{
    public class FilterResponse<T>
    {
        #region Properties
        public IEnumerable<T> Data { get; set; }
        public long TotalCount { get; set; }
        #endregion
    }
}
