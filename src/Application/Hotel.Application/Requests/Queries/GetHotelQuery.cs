using HotelsApp.Shared.Responses;
using MediatR;

namespace HotelsApp.Application.Requests.Queries
{
    public sealed record GetHotelQuery(long Id) : IRequest<IResponse>;

}
