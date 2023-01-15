using AutoMapper;
using HotelsApp.Application.Dtos;
using HotelsApp.Application.Requests.Queries;
using HotelsApp.Data.Abstractions;
using HotelsApp.Data.Filtering;
using HotelsApp.Shared.Enums;
using HotelsApp.Shared.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelsApp.Application.Requests.Handlers
{
    public class GetAllHotelsSortedByCurrentLocationQueryHandler : IRequestHandler<GetAllHotelsSortedByCurrentLocationQuery, IResponse>
    {
        #region Fields
        private readonly ILocationHotelRepository _locationHotelRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllHotelsSortedByCurrentLocationQueryHandler> _logger;
        #endregion

        #region Constructors
        public GetAllHotelsSortedByCurrentLocationQueryHandler(ILocationHotelRepository locationHotelRepository,
            IMapper mapper,
            ILogger<GetAllHotelsSortedByCurrentLocationQueryHandler> logger)
        {
            _locationHotelRepository = locationHotelRepository ?? throw new ArgumentNullException(nameof(locationHotelRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        #endregion

        #region Public Methods
        public async Task<IResponse> Handle(GetAllHotelsSortedByCurrentLocationQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Prepare list of all hotels");

            var paginationOptions = _mapper.Map<PaginationOptions>(request.PaginationParamsDto);
            var hotels = await _locationHotelRepository
                .GetAllHotelsSortedByCurrentLocation(request.Latitude, request.Longitude, paginationOptions);

            if (hotels.Data.Count() == 0)
            {
                _logger.LogError($"List of hotels is empty.");
                return ErrorResponse.Error(ErrorCode.ItemNotFound, "Items are not found");
            }
            _logger.LogInformation("List of all hotels is prepared");

            return SuccessResponse.Success(new FilterResponse<SortedHotesDto>()
            {
                Data = _mapper.Map<List<SortedHotesDto>>(hotels.Data),
                TotalCount = hotels.TotalCount
            });
        }
        #endregion
    }
}
