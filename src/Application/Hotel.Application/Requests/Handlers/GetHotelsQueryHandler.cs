using AutoMapper;
using HotelsApp.Application.Dtos;
using HotelsApp.Application.Requests.Queries;
using HotelsApp.Data.Abstractions;
using HotelsApp.Shared.Enums;
using HotelsApp.Shared.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelsApp.Application.Requests.Handlers
{
    public class GetHotelsQueryHandler : IRequestHandler<GetHotelsQuery, IResponse>
    {
        #region Fields
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetHotelsQueryHandler> _logger;
        #endregion

        #region Constructors
        public GetHotelsQueryHandler(IHotelRepository hotelRepository,
            IMapper mapper,
            ILogger<GetHotelsQueryHandler> logger)
        {
            _hotelRepository = hotelRepository ?? throw new ArgumentNullException(nameof(hotelRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        #endregion

        #region Public Methods
        public async Task<IResponse> Handle(GetHotelsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start getting list of all hotels");
            var hotels = await _hotelRepository.GetAllHotelAsync();

            if (hotels == null || hotels.Count == 0)
            {
                _logger.LogWarning("There is no hotel in the list");
                return ErrorResponse.Error(ErrorCode.ItemNotFound, "Items are not found");
            }

            _logger.LogInformation("List of all hotels has been provided");

            var hotelDtos = _mapper.Map<List<HotelDto>>(hotels);
            return SuccessResponse.Success<List<HotelDto>>(hotelDtos);
        }
        #endregion
    }
}
