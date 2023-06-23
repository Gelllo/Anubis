using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Anubis.Application.Requests.Login;
using Anubis.Application.Requests.Register;
using Anubis.Application.Requests.Users;
using Anubis.Domain.UsersDomain;
using AutoMapper;

namespace Anubis.Application.Mappings
{
    public class RegisterMapping : Profile
    {
        public RegisterMapping()
        {
            CreateMap<RegisterRequest, User>()
                .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserID))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest=> dest.Role, opt => opt.MapFrom(src=>src.Role));
        }
    }
}
