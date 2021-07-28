using AppUTM.Api.DTOS.Cupones;
using AppUTM.Api.DTOS.Empresas;
using AppUTM.Api.DTOS.Permissions;
using AppUTM.Api.DTOS.Roles;
using AppUTM.Api.DTOS.Users;
using AppUTM.Core.Models;
using AutoMapper;
using System;

namespace AppUTM.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Roles
            CreateMap<Role, RolesCreate>().ReverseMap();
            CreateMap<Role, RoleReturn>().ReverseMap();
            CreateMap<Role, RoleForUpdateDto>().ReverseMap();

            CreateMap<RolesCreate, Role>().AfterMap(
                ((source, destination) =>
                {
                    destination.CreateAt = DateTime.Now;
                    destination.Status = true;
                }));
            CreateMap<RoleReturn, Role>();
            //Users
            CreateMap<User, UserCreate>().ReverseMap();
            CreateMap<User, UserReturn>().ReverseMap();
            CreateMap<User, UserForUpdateDto>().ReverseMap();
            CreateMap<UserCreate, User>().AfterMap(
                ((source, destination) =>
                {
                    destination.CreateAt = DateTime.Now;
                    destination.Status = true;
                }));

            CreateMap<UserReturn, Role>();
            //Permission
            CreateMap<Permission, PermissionCreate>().ReverseMap();
            CreateMap<Permission, PermissionReturn>().ReverseMap();
            CreateMap<Permission, PermissionForUpdateDto>().ReverseMap();
            CreateMap<PermissionCreate, Permission>().AfterMap(
                ((source, destination) =>
                {
                    destination.CreateAt = DateTime.Now;
                    destination.Status = true;
                }));
            CreateMap<PermissionReturn, Permission>();
            //empresas
            CreateMap<Empresa, EmpresaReturn>().ReverseMap();
            CreateMap<Empresa, EmpresaCreate>().ReverseMap();
            //cupongenerico
            CreateMap<CuponGenerico, CuponGenericoReturn>().ReverseMap();
            CreateMap<CuponGenerico, CuponGenericoCreate>().ReverseMap();
            //cuponImagen
            CreateMap<CuponImagen, CuponImagenReturn>().ReverseMap();
            CreateMap<CuponImagen, CuponImagenCreate>().ReverseMap();
        }
    }
}