using HotelsApp.Data.Filtering;
using HotelsApp.Data.Models;
using HotelsApp.Infrastructure.Repositories;
using HotelsApp.Tests.Lists;
using Microsoft.Extensions.Logging;
using Moq;

namespace HotelsApp.Tests.Repositories
{
    public class LocationHotelRepositoryTests
    {
        private readonly Mock<ILogger<LocationHotelRepository>> _mockLogger;
        private readonly HotelLists _hotelLists;
        private readonly PaginationOptions _paginationOptions;
        public LocationHotelRepositoryTests()
        {
            _mockLogger = new Mock<ILogger<LocationHotelRepository>>();
            _hotelLists = new HotelLists();
            _paginationOptions = new PaginationOptions();
        }

        [Fact]
        public async Task CreateHotelAsync_ShouldPopulateListAndReturnId()
        {
            var locationHotelRepository = new LocationHotelRepository(_hotelLists.Hotels, _mockLogger.Object);
            _paginationOptions.SizePerPage = 10;
            _paginationOptions.Page = 1;
            var result = await locationHotelRepository.GetAllHotelsSortedByCurrentLocation(45.257907355517624, 19.81236695106882, _paginationOptions);
            Assert.NotNull(result.Data);
            Assert.Equal(4, result.TotalCount);
        }
    }
}
