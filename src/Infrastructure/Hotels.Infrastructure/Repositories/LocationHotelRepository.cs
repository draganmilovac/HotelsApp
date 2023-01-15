using GeoCoordinatePortable;
using HotelsApp.Data.Abstractions;
using HotelsApp.Data.Filtering;
using HotelsApp.Data.Models;
using HotelsApp.Infrastructure.Extensions;
using HotelsApp.Infrastructure.Models;
using Microsoft.Extensions.Logging;

namespace HotelsApp.Infrastructure.Repositories
{
    public class LocationHotelRepository : ILocationHotelRepository
    {
        #region Fields
        private readonly IList<Hotel> _hotels;
        private readonly ILogger<LocationHotelRepository> _logger;
        #endregion

        #region Constructors
        public LocationHotelRepository(Hotels hotels, ILogger<LocationHotelRepository> logger)
        {
            _hotels = hotels ?? throw new ArgumentNullException(nameof(hotels));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        #endregion

        #region Public methods

        public async Task<FilterResult<Hotel>> GetAllHotelsSortedByCurrentLocation(double latitude, double longitude, PaginationOptions paginationOptions)
        {
            List<Hotel> hotels = new List<Hotel>();
            var currentLocation = new GeoCoordinate(latitude, longitude);
            _logger.LogInformation($"Current location is x: {latitude} and y: {longitude}");
            foreach (var hotel in _hotels)
            {
                var hotelLocation = new GeoCoordinate(hotel.Latitude, hotel.Longitude);
                _logger.LogInformation($"Location for hotel with id: {hotel.Id} is x: {hotel.Latitude} and y: {hotel.Longitude}");
                //it will return distance in km
                var distance = currentLocation.GetDistanceTo(hotelLocation) / 1000;
                _logger.LogInformation($"Distance between hotel with id {hotel.Id} and current location is: {distance}km");
                hotels.Add(new Hotel()
                {
                    Distance = distance,
                    Price = hotel.Price,
                    Name = hotel.Name
                });
            }

            //Ordered list of hotels
            var hotelsOrderedByDistanceAndPrice = hotels
                .OrderBy(x => x.Distance)
                .ThenBy(p => p.Price);

            var filteredHotels = await hotelsOrderedByDistanceAndPrice
                .ApplyPagination(paginationOptions);

            return filteredHotels;
        }
        #endregion
    }
}
