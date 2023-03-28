using AutoMapper;
using WePayOffer.BL.Models;
using WePayOffer.DAL.Entity;
using WePayOffer.DAL.Extend;

namespace WePayOffer.BL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<ServiceNumber, ServiceNumberVM>().ReverseMap();
            CreateMap<ServiceTransaction, ServiceTransactionVM>().ReverseMap();
            CreateMap<ApplicationUserVM, ApplicationUser>().ReverseMap()
                .ForPath(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForPath(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForPath(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForPath(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
                .ForPath(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Users.UserName));
        }
    }
}
