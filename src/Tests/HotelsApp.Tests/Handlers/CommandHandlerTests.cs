using AutoMapper;
using HotelsApp.Application.Dtos;
using HotelsApp.Application.Dtos.Profiles;
using HotelsApp.Application.Requests.Commands;
using HotelsApp.Application.Requests.Handlers;
using HotelsApp.Application.Requests.Queries;
using HotelsApp.Data.Abstractions;
using HotelsApp.Data.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace HotelsApp.Tests.Handlers
{
    public class CommandHandlerTests
    {
        #region Fields
        private readonly Mock<IHotelRepository> _mockHotelRepository;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<HotelCreateCommandHandler>> _createCommandMockLogger;
        private readonly Mock<ILogger<HotelDeleteCommandHandler>> _deleteCommandMockLogger;
        private readonly Mock<ILogger<HotelUpdateCommandHandler>> _updateCommandMockLogger;
        private readonly Mock<ILogger<GetHotelQueryHanlder>> _getHotelQueryHanlderMockLogger;
        private readonly Mock<ILogger<GetHotelsQueryHandler>> _getGetHotelsQueryHandlerMockLogger;
        #endregion

        #region Constructors
        public CommandHandlerTests()
        {
            _mockHotelRepository = new Mock<IHotelRepository>();
            _createCommandMockLogger = new Mock<ILogger<HotelCreateCommandHandler>>();
            _deleteCommandMockLogger = new Mock<ILogger<HotelDeleteCommandHandler>>();
            _updateCommandMockLogger = new Mock<ILogger<HotelUpdateCommandHandler>>();
            _getHotelQueryHanlderMockLogger = new Mock<ILogger<GetHotelQueryHanlder>>();
            _getGetHotelsQueryHandlerMockLogger = new Mock<ILogger<GetHotelsQueryHandler>>();

            var mockMapper = ConfigureMapping();
            _mapper = mockMapper.CreateMapper();
        }
        #endregion

        #region Public methods
        [Fact]
        public async Task GivenAnNullValueForHotelDto_ShouldNotCreateNewHotelAndNotReturnSuccessResult()
        {
            var hotelCreateCommand = new HotelCreateCommand(null);

            var hotelCreateCommandHandler =
                new HotelCreateCommandHandler(_mockHotelRepository.Object,
                _mapper,
                _createCommandMockLogger.Object);
            var result = await hotelCreateCommandHandler.Handle(hotelCreateCommand, default);
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GivenAnNotNullValueForHotelDto_ShouldReturnSuccessResultAnCreateNewHotel()
        {
            HotelDto hotelDto = new HotelDto()
            {
                Name = "Hotel 1",
                Price = 50,
                Latitude = 45.257907355517624,
                Longitude = 19.81236695106882
            };
            var hotelCreateCommand = new HotelCreateCommand(hotelDto);
            _mockHotelRepository
               .Setup(x => x.CreateHotelAsync(It.IsAny<Hotel>()))
               .Returns(Task.FromResult<long>(2));

            var hotelCreateCommandHandler =
                new HotelCreateCommandHandler(_mockHotelRepository.Object,
                _mapper,
                _createCommandMockLogger.Object);

            var result = await hotelCreateCommandHandler.Handle(hotelCreateCommand, default);
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Result);
        }

        [Fact]
        public async Task GivenHotelId_ShouldSuccessfullyRemoveItemFromHotelList()
        {
            var hotelDeleteCommand = new HotelDeleteCommand(2);
            _mockHotelRepository
               .Setup(x => x.DeleteAsync(It.IsAny<long>()))
               .Returns(Task.FromResult<bool>(true));

            var hotelDeleteCommandHandler =
                new HotelDeleteCommandHandler(_mockHotelRepository.Object,
                _mapper,
                _deleteCommandMockLogger.Object);

            var result = await hotelDeleteCommandHandler.Handle(hotelDeleteCommand, default);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GivenAnNullValueForHotelDto_ShouldNotSuccessfullyUpdateHotel()
        {
            var hotelUpdateCommand = new HotelUpdateCommand(null, 1);
            var hotelUpdateCommandHandler =
                new HotelUpdateCommandHandler(_mockHotelRepository.Object,
                _mapper,
                _updateCommandMockLogger.Object);
            var result = await hotelUpdateCommandHandler.Handle(hotelUpdateCommand, default);
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GivenHotelDto_ShouldSuccessfullyUpdateHotel()
        {
            HotelDto hotelDto = new HotelDto()
            {
                Name = "Hotel 1",
                Price = 50,
                Latitude = 45.257907355517624,
                Longitude = 19.81236695106882
            };
            var hotelUpdateCommand = new HotelUpdateCommand(hotelDto, 1);

            var hotelUpdateCommandHandler =
                new HotelUpdateCommandHandler(_mockHotelRepository.Object,
                _mapper,
                _updateCommandMockLogger.Object);
            _mockHotelRepository
                .Setup(x => x.UpdateAsync(It.IsAny<Hotel>(), It.IsAny<long>()))
                .Returns(Task.FromResult<Hotel>(_mapper.Map<Hotel>(hotelDto)));
            var result = await hotelUpdateCommandHandler.Handle(hotelUpdateCommand, default);
            Assert.True(result.IsSuccess);
        }
        [Fact]
        public async Task GivenHotelId_ShouldSuccessfullyReturnHotel()
        {

            var getHotelQuery = new GetHotelQuery(1);

            var getHotelQueryHandler =
                new GetHotelQueryHanlder(_mockHotelRepository.Object,
                _mapper,
                _getHotelQueryHanlderMockLogger.Object);
            _mockHotelRepository
                .Setup(x => x.GetHotelAsync(It.IsAny<long>()))
                .Returns(Task.FromResult<Hotel>(new Hotel()));
            var result = await getHotelQueryHandler.Handle(getHotelQuery, default);
            Assert.True(result.IsSuccess);
        }
        [Fact]
        public async Task GivenHotelId_ShouldNotSuccessfullyReturnHotel()
        {

            var getHotelQuery = new GetHotelQuery(1);

            var getHotelQueryHandler =
                new GetHotelQueryHanlder(_mockHotelRepository.Object,
                _mapper,
                _getHotelQueryHanlderMockLogger.Object);
            _mockHotelRepository
                .Setup(x => x.GetHotelAsync(It.IsAny<long>()))
                .Returns(Task.FromResult<Hotel>(null));
            var result = await getHotelQueryHandler.Handle(getHotelQuery, default);
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task ListOfHotels_ShouldNotSuccessfullyReturnHotels()
        {

            var getHotelsQuery = new GetHotelsQuery();

            var getHotelsQueryHandler =
                new GetHotelsQueryHandler(_mockHotelRepository.Object,
                _mapper,
                _getGetHotelsQueryHandlerMockLogger.Object);
            _mockHotelRepository
                .Setup(x => x.GetAllHotelAsync())
                .Returns(Task.FromResult<List<Hotel>>(null));
            var result = await getHotelsQueryHandler.Handle(getHotelsQuery, default);
            Assert.False(result.IsSuccess);
        }
        [Fact]
        public async Task ListOfHotels_ShouldSuccessfullyReturnHotels()
        {
            List<Hotel> hotels = new List<Hotel>();
            Hotel hotel = new Hotel()
            {
                Name = "Hotel 1",
                Price = 50,
                Latitude = 45.257907355517624,
                Longitude = 19.81236695106882
            };
            hotels.Add(hotel);
            var getHotelsQuery = new GetHotelsQuery();

            var getHotelsQueryHandler =
                new GetHotelsQueryHandler(_mockHotelRepository.Object,
                _mapper,
                _getGetHotelsQueryHandlerMockLogger.Object);
            _mockHotelRepository
                .Setup(x => x.GetAllHotelAsync())
                .Returns(Task.FromResult<List<Hotel>>(hotels));
            var result = await getHotelsQueryHandler.Handle(getHotelsQuery, default);
            Assert.True(result.IsSuccess);
        }
        #endregion

        #region Private methods
        private MapperConfiguration ConfigureMapping()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Mapping());
            });
        }
        #endregion
    }
}
