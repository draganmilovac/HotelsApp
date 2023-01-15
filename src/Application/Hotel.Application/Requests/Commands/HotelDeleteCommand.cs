using HotelsApp.Shared.Responses;
using MediatR;

namespace HotelsApp.Application.Requests.Commands
{
    public sealed record HotelDeleteCommand(long Id) : IRequest<IResponse>;
}
