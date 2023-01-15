using HotelsApp.Application.Dtos;
using HotelsApp.Shared.Responses;
using MediatR;

namespace HotelsApp.Application.Requests.Commands
{
    public sealed record HotelCreateCommand(HotelDto Hotel) : IRequest<IResponse<long>>;
}
