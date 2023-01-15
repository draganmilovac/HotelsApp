using HotelsApp.Data.Models;

namespace HotelsApp.Data.Abstractions
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        /// <summary>
        /// Method that retrieves all created hotels in a list
        /// </summary>
        /// <returns>List of hotels</returns>
        Task<List<Hotel>> GetAllHotelAsync();

        /// <summary>
        /// Method that retrieves hotel from list based on hotel Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Hotel from hotel list</returns>
        Task<Hotel> GetHotelAsync(long id);
    }
}
