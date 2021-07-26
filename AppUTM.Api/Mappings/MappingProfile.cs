using AppUTM.Api.DTOS.Cupones;
using AppUTM.Api.DTOS.Empresas;
using AppUTM.Api.DTOS.Roles;
using AppUTM.Core.Models;
using AutoMapper;
using System;
using AppUTM.Api.DTOS.Permissions;
using AppUTM.Api.DTOS.Users;
using AutoMapper.Internal;

namespace AppUTM.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //roles
            CreateMap<RolesCreate, Role>();//de la entidad pasara a al pedido del dto
            CreateMap<Role, RoleReturn>();//de la entidad pasa a tener una respuesta del dto
            CreateMap<RolesCreate, Role>()
                .ForMember(destination => destination.RolePermissions, act => act.MapFrom(source => source.RolePermissions))
                .AfterMap(//recibiendo de DTO y pasas a entidad esto se usa para los registros
                (source, destination) =>
            {
                destination.CreateAt = DateTime.Now;
            });
            CreateMap<UserReturn, User>();
            CreateMap<UserCreate, User>();//de la entidad pasara a al pedido del dto
            CreateMap<User, UserReturn>();//de la entidad pasa a tener una respuesta del dto
            CreateMap<UserCreate, User>()
                .ForMember(destination => destination.UserRoles, act => act.MapFrom(source => source.UserRoles))
                .AfterMap(//recibiendo de DTO y pasas a entidad esto se usa para los registros
                    (source, destination) =>
                    {
                        destination.CreateAt = DateTime.Now;
                    });
            CreateMap<PermissionReturn, Permission>();
            CreateMap<PermissionCreate, Permission>();//de la entidad pasara a al pedido del dto
            CreateMap<Permission, PermissionReturn>();//de la entidad pasa a tener una respuesta del dto
            CreateMap<PermissionCreate, Permission>()
                .AfterMap(//recibiendo de DTO y pasas a entidad esto se usa para los registros
                    (source, destination) =>
                    {
                        destination.CreateAt = DateTime.Now;
                    });
            CreateMap<RoleReturn, Role>();
            CreateMap<Empresa, EmpresaReturn>().ReverseMap();
            CreateMap<Empresa, EmpresaCreate>().ReverseMap();

            CreateMap<CuponGenerico, CuponGenericoReturn>().ReverseMap();
            CreateMap<CuponGenerico, CuponGenericoCreate>().ReverseMap();
            CreateMap<CuponImagen, CuponImagenReturn>().ReverseMap();
            CreateMap<CuponImagen, CuponImagenCreate>().ReverseMap();
        }
    }
}