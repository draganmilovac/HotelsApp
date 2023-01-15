using AutoMapper;
using HotelsApp.Application.Dtos;
using HotelsApp.Application.Requests.Commands;
using HotelsApp.Data.Abstractions;
using HotelsApp.Data.Models;
using HotelsApp.Shared.Enums;
using HotelsApp.Shared.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelsApp.Application.Requests.Handlers
{
    public class HotelUpdateCommandHandler : IRequestHandler<HotelUpdateCommand, IResponse>
    {
        #region Fields
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<HotelUpdateCommandHandler> _logger;
        #endregion

        #region Constructors
        public HotelUpdateCommandHandler(IHotelRepository hotelRepository,
            IMapper mapper,
            ILogger<HotelUpdateCommandHandler> logger)
        {
            _hotelRepository = hotelRepository ?? throw new ArgumentNullException(nameof(hotelRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        #endregion

        #region Public Methods
        public async Task<IResponse> Handle(HotelUpdateCommand request, CancellationToken cancellationToken)
        {
            var hotel = _mapper.Map<Hotel>(request.Hotel);

            if(hotel == null)
            {
                _logger.LogError($"Hotel with id:{request.Id} does not exist");
                return ErrorResponse.Error(ErrorCode.ItemNotFound, "Item not found");
            }

            _logger.LogInformation($"Start updating hotel with id:{hotel.Id}");
            var hotelToUpdate = await _hotelRepository.UpdateAsync(hotel, request.Id);
            var updatedHotel = _mapper.Map<HotelDto>(hotelToUpdate);
            _logger.LogInformation($"Hotel with id:{hotel.Id} is updated");

            return SuccessResponse.Success<HotelDto>(updatedHotel);
        }
        #endregion
    }
}
