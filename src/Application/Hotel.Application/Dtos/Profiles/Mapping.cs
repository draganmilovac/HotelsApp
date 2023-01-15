using AutoMapper;
using HotelsApp.Data.Filtering;
using HotelsApp.Data.Models;

namespace HotelsApp.Application.Dtos.Profiles
{
    public class Mapping : Profile
    {
        #region Constructors
        public Mapping()
        {
            CreateMap<HotelDto, Hotel>().ReverseMap();
            CreateMap<SortedHotesDto, Hotel>().ReverseMap();
            CreateMap<PaginationParamsDto, PaginationOptions>().ReverseMap();
        }
        #endregion
    }
}
