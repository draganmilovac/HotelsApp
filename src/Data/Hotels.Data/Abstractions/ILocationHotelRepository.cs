using HotelsApp.Data.Filtering;
using HotelsApp.Data.Models;

namespace HotelsApp.Data.Abstractions
{
    public interface ILocationHotelRepository
    {
        /// <summary>
        /// Method that retrieves all created hotels in a list sorted by Price and Location
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="paginationOptions"></param>
        /// <returns>Sorted list of hotels</returns>
        Task<FilterResult<Hotel>> GetAllHotelsSortedByCurrentLocation(double latitude, double longitude, PaginationOptions paginationOptions);
    }
}
