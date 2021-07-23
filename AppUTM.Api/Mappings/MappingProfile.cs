using AppUTM.Api.DTOS.Roles;
using AppUTM.Core.Models;
using AutoMapper;

namespace AppUTM.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Role, RoleReturn>();
            CreateMap<RolesCreate, Role>();
        }
    }
}