using AppUTM.Api.DTOS.Cupones;
using AppUTM.Api.DTOS.Empresas;
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

            CreateMap<Empresa, EmpresaReturn>().ReverseMap();
            CreateMap<Empresa, EmpresaCreate>().ReverseMap();

            CreateMap<CuponGenerico, CuponGenericoReturn>().ReverseMap();
            CreateMap<CuponGenerico, CuponGenericoCreate>().ReverseMap();
            CreateMap<CuponImagen, CuponImagenReturn>().ReverseMap();
            CreateMap<CuponImagen, CuponImagenCreate>().ReverseMap();
        }
    }
}