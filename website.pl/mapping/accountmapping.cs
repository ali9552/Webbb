using AutoMapper;
using website.dal.NewFolder;
using website.pl.dtos.accountdto;

namespace website.pl.mapping
{
    public class accountmapping : Profile
    {
        public accountmapping()
        {
            CreateMap<signupdto, appuser>()
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.username))  // Mapping username to appuser's UserName
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.email));  // Mapping email to appuser's Email

            // You can map additional properties if needed, for example:
            // .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.firstname));
            // .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.lastname));

            // Reverse mapping if needed
            CreateMap<appuser, signupdto>()
                .ForMember(dest => dest.username, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email));
        }
    }
}
