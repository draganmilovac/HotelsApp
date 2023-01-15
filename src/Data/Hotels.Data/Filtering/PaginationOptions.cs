namespace HotelsApp.Data.Filtering
{
    public class PaginationOptions
    {
        #region Properties
        public int Page { get; set; }
        public int SizePerPage { get; set; }
        public static PaginationOptions All => new PaginationOptions { Page = 1, SizePerPage = int.MaxValue };
        #endregion
    }
}
