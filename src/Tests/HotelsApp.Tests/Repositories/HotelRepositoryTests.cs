using HotelsApp.Data.Models;
using HotelsApp.Infrastructure.Repositories;
using HotelsApp.Tests.Lists;
using Microsoft.Extensions.Logging;
using Moq;

namespace HotelsApp.Tests.Repositories
{
    public class HotelRepositoryTests
    {
        #region Fields
        private readonly Mock<ILogger<HotelRepository>> _mockLogger;
        private readonly HotelLists _hotelLists;
        #endregion

        #region Constructors
        public HotelRepositoryTests()
        {
            _mockLogger = new Mock<ILogger<HotelRepository>>();
            _hotelLists = new HotelLists();
        }
        #endregion

        #region Public methods

        [Fact]
        public async Task CreateHotelAsync_ShouldPopulateListAndReturnId()
        {
            Hotel hotel = new Hotel()
            {
                Name = "Hotel 5",
                Price = 60,
                Latitude = 46.257907355517624,
                Longitude = 20.81236695106882
            };
            var hotelRepository = new HotelRepository(_hotelLists.Hotels, _mockLogger.Object);
            var result = await hotelRepository.CreateHotelAsync(hotel);
            Assert.Equal(5,result);
        }

        [Fact]
        public async Task GetHotelElementAsync_ShouldReturnElementResult()
        {
            var hotelRepository = new HotelRepository(_hotelLists.Hotels, _mockLogger.Object);
            var result = await hotelRepository.GetHotelAsync(1);
            Assert.NotNull(result);
            Assert.Equal("Hotel 1", result.Name);
        }
        [Fact]
        public async Task GetHotelElementsAsync_ShouldReturnListOfHotelElements()
        {
            var hotelRepository = new HotelRepository(_hotelLists.Hotels, _mockLogger.Object);
            var result = await hotelRepository.GetAllHotelAsync();
            Assert.Equal(4,result.Count);
            Assert.Equal("Hotel 2", result.Where(x => x.Id == 2).Select(n => n.Name).FirstOrDefault());
        }
        [Fact]
        public async Task UpdateHotelElementsAsync_ShouldUpdateHotelElement()
        {
            Hotel hotel = new Hotel()
            {
                Name = "Hotel 6",
                Price = 20,
                Latitude = 45.237212010810886,
                Longitude = 19.810189977197428
            };
            var hotelRepository = new HotelRepository(_hotelLists.Hotels, _mockLogger.Object);
            var result = await hotelRepository.UpdateAsync(hotel,1);
            Assert.Equal("Hotel 6", result.Name);
        }
        [Fact]
        public async Task DeleteHotelElementsAsync_ShouldRemoveHotelElementFromList()
        {
            var hotelRepository = new HotelRepository(_hotelLists.Hotels, _mockLogger.Object);
            var result = await hotelRepository.DeleteAsync(1);
            Assert.True(result);
        }
        #endregion
    }
}
