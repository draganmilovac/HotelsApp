using AutoMapper;
using HotelsApp.Application.Requests.Commands;
using HotelsApp.Data.Abstractions;
using HotelsApp.Data.Models;
using HotelsApp.Shared.Enums;
using HotelsApp.Shared.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelsApp.Application.Requests.Handlers
{
    public class HotelCreateCommandHandler : IRequestHandler<HotelCreateCommand, IResponse<long>>
    {
        #region Fields
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<HotelCreateCommandHandler> _logger;
        #endregion

        #region Constructors
        public HotelCreateCommandHandler(IHotelRepository hotelRepository, 
            IMapper mapper,
            ILogger<HotelCreateCommandHandler> logger)
        {
            _hotelRepository = hotelRepository ?? throw new ArgumentNullException(nameof(hotelRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        #endregion

        #region Public Methods
        public async Task<IResponse<long>> Handle(HotelCreateCommand request, CancellationToken cancellationToken)
        {
            if(request.Hotel == null)
            {
                _logger.LogError("Hotel does not exists");
                return ErrorResponse.Error<long>(ErrorCode.ItemNotFound, "Item not found");
            }

            var hotel = _mapper.Map<Hotel>(request.Hotel);
            _logger.LogInformation("Start creating new hotel and add in hotel list");
            var response = await _hotelRepository.CreateHotelAsync(hotel);

            _logger.LogInformation("New hotel is created and added to list");

            return SuccessResponse.Success<long>(response);
        }
        #endregion
    }
}
