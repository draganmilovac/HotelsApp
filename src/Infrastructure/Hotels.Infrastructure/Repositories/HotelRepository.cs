using HotelsApp.Data.Abstractions;
using HotelsApp.Data.Models;
using HotelsApp.Infrastructure.Models;
using Microsoft.Extensions.Logging;

namespace HotelsApp.Infrastructure.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        #region Fields
        private readonly IList<Hotel> _hotels;
        private readonly ILogger<HotelRepository> _logger;
        #endregion

        #region Constructors
        public HotelRepository(Hotels hotels, ILogger<HotelRepository> logger)
        {
            _hotels = hotels ?? throw new ArgumentNullException(nameof(hotels));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        #endregion

        #region Public Methods
        public async Task<List<Hotel>> GetAllHotelAsync()
        {
            return await Task.Run(() =>
            {
                return _hotels.ToList();
            });
        }

        public async Task<Hotel> GetHotelAsync(long id)
        {
            return await Task.Run(() =>
            {
                return _hotels.SingleOrDefault(x => x.Id == id);
            });
        }

        public async Task<long> CreateHotelAsync(Hotel hotel)
        {
            _logger.LogInformation("Set id for new hotel");
            hotel.Id = IncrementHotelId();
            return await Task.Run(() =>
            {
                _hotels.Add(hotel);
                return hotel.Id;
            });
            _logger.LogInformation($"New hotel with id {hotel.Id} is added to list");
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await Task.Run(() =>
            {
                var hotel = _hotels.SingleOrDefault(x => x.Id == id);
                if(hotel == null)
                {
                    _logger.LogError($"Hotel with id: {id} does not exist and it is not possible to delete hotel");
                    return false;
                }
                _hotels.Remove(hotel);
                _logger.LogInformation($"Hotel with id: {id} successfully deleted nad removed from hotel's list");
                return true;
            });
        }

        public async Task<Hotel> UpdateAsync(Hotel hotel, long id)
        {
            return await Task.Run(() =>
            {
                var hotelToUpdate = _hotels.SingleOrDefault(x => x.Id == id);
                if(hotelToUpdate == null)
                {
                    return null;
                }
                hotelToUpdate.Name = hotel.Name;
                hotelToUpdate.Price = hotel.Price;
                hotelToUpdate.Longitude = hotel.Longitude;
                hotelToUpdate.Latitude = hotel.Latitude;
                return hotelToUpdate;
            });
        }
        #endregion

        #region Private Methods
        public long IncrementHotelId()
        {
            var highestId = _hotels.Any() ? _hotels.Max(x => x.Id) : 1;
            return highestId + 1;
        }
        #endregion
    }
}
