namespace HotelsApp.Data.Abstractions
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Method that is responsible for creating a hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns>Hotel Id</returns>
        Task<long> CreateHotelAsync(TEntity hotel);

        /// <summary>
        /// Method that is responsible for deleting a hotel
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Bool value depending on whether the hotel has been deleted or not</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// Method that is responsible for updating a existing hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <param name="id"></param>
        /// <returns>Hotel with updated values</returns>
        Task<TEntity> UpdateAsync(TEntity hotel, long id);
    }
}
