using HotelsApp.Application.Dtos;
using HotelsApp.Shared.Responses;
using MediatR;

namespace HotelsApp.Application.Requests.Queries
{
    public sealed record GetAllHotelsSortedByCurrentLocationQuery(double Latitude, double Longitude, PaginationParamsDto PaginationParamsDto) :IRequest<IResponse>;
}
