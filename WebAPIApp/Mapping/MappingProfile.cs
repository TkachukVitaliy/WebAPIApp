using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIApp.Models;
using WebAPIApp.ViewModel;

namespace WebAPIApp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate.ToLocalTime()))
                .ForMember(dest => dest.EditDate, opt => opt.MapFrom(src => src.EditDate.HasValue ? src.EditDate.Value.ToLocalTime() : (DateTime?)null));

            this.CreateMap<UserViewModel, User>()
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.CreationDate, DateTimeKind.Local)))
                .ForMember(dest => dest.EditDate, opt => opt.MapFrom(src => src.EditDate.HasValue ? DateTime.SpecifyKind(src.EditDate.Value, DateTimeKind.Local) : (DateTime?)null));

            this.CreateMap<UserCreateViewModel, User>()
                .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.EditDate, opt => opt.NullSubstitute(null));

            this.CreateMap<UserUpdateViewModel, User>()
                .ForMember(dest => dest.EditDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
    
}
