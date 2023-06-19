using Anubis.Application.Requests.Users;
using Anubis.Application.Responses.Users;
using Anubis.Domain;
using AutoMapper;

namespace Anubis.Application.UserMappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<CreateUserRequest, User>()
                .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserID))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));
            CreateMap<User, CreateUserResponse>()
                .ForMember(dest => dest.User, opt =>
                    opt.MapFrom(src => new User()
                    {
                        Email = src.Email,
                        FirstName = src.FirstName,
                        UserID = src.UserID,
                        LastName = src.LastName,
                        Id = src.Id
                    }));

            CreateMap<UpdateUserRequest, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserID))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));
            CreateMap<User, UpdateUserResponse>()
                .ForMember(dest => dest.User, opt =>
                    opt.MapFrom(src => new User()
                    {
                        Email = src.Email,
                        FirstName = src.FirstName,
                        UserID = src.UserID,
                        LastName = src.LastName,
                        Id = src.Id
                    }));

            CreateMap<DeleteUserRequest, int>();
        }
    }
}