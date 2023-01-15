using AutoMapper;
using HotelsApp.Application.Requests.Commands;
using HotelsApp.Data.Abstractions;
using HotelsApp.Shared.Enums;
using HotelsApp.Shared.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelsApp.Application.Requests.Handlers
{
    public class HotelDeleteCommandHandler : IRequestHandler<HotelDeleteCommand, IResponse>
    {
        #region Fields
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<HotelDeleteCommandHandler> _logger;
        #endregion

        #region Constructors
        public HotelDeleteCommandHandler(IHotelRepository hotelRepository,
            IMapper mapper,
            ILogger<HotelDeleteCommandHandler> logger)
        {
            _hotelRepository = hotelRepository ?? throw new ArgumentNullException(nameof(hotelRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        #endregion

        #region Public Methods
        public async Task<IResponse> Handle(HotelDeleteCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start deleting hotel");

            var result = await _hotelRepository.DeleteAsync(request.Id);

            return result ? SuccessResponse.Success(result)
                : ErrorResponse.Error(ErrorCode.ItemNotFound, "Item not found");
        }
        #endregion
    }
}
