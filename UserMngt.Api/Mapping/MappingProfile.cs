using AutoMapper;
using UserMngt.Api.Resources;
using UserMngt.Core.Models;

namespace UserMngt.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<User, UserResource>();
            
            // Resource to Domain
            CreateMap<UserResource, User>();
            CreateMap<SaveUserResource, User>();
        }
    }
}