using HotelsApp.Application.Dtos;
using HotelsApp.Shared.Responses;
using MediatR;

namespace HotelsApp.Application.Requests.Commands
{
    public sealed record HotelUpdateCommand(HotelDto Hotel, long Id) : IRequest<IResponse>;
}
