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
    public class GetHotelQueryHanlder : IRequestHandler<GetHotelQuery, IResponse>
    {
        #region Fields
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetHotelQueryHanlder> _logger;
        #endregion

        #region Properties
        public GetHotelQueryHanlder(IHotelRepository hotelRepository, 
            IMapper mapper,
            ILogger<GetHotelQueryHanlder> logger)
        {
            _hotelRepository = hotelRepository ?? throw new ArgumentNullException(nameof(hotelRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(hotelRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        #endregion

        #region Public Methods
        public async Task<IResponse> Handle(GetHotelQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start searching for hotel with id {request.Id}");
            var hotel = await _hotelRepository.GetHotelAsync(request.Id);

            if(hotel == null)
            {
                _logger.LogError($"Hotel with id: {request.Id} does not exist.");
                return ErrorResponse.Error(ErrorCode.ItemNotFound, "Items are not found");
            }

            _logger.LogInformation($"Hotel with {request.Id} is found");

            var hotelDto = _mapper.Map<HotelDto>(hotel);
            return SuccessResponse.Success<HotelDto>(hotelDto);
        }
        #endregion
    }
}
