using AppUTM.Api.DTOS.Cupones;
using AppUTM.Api.DTOS.Empresas;
using AppUTM.Api.DTOS.HistorialCupones;
using AppUTM.Api.DTOS.Modules;
using AppUTM.Api.DTOS.ModulesRoles;
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
            CreateMap<User, UserDelete>().ReverseMap();

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

            CreateMap<Module, ModuleCreate>().ReverseMap();
            CreateMap<Module, ModuleReturn>().ReverseMap();
            CreateMap<Module, moduleToBeUpdated>().ReverseMap();

            //ModuleROle
            CreateMap<ModuleRole, ModuleRoleCreate>().ReverseMap();
            //CreateMap<ModuleRole, ModuleRoleDelete>().ReverseMap();
            CreateMap<ModuleRoleReturn, ModuleRole>().ReverseMap().ForMember(x => x.RoleName, y => y.MapFrom(z => z.Role.Nombre));
            CreateMap<ModuleRoleReturn, ModuleRole>().ReverseMap().ForMember(x => x.ModuleName, y => y.MapFrom(z => z.Module.Nombre));
            CreateMap<ModuleRole, ModuleRoleUpdate>().ReverseMap();

            //empresas
            CreateMap<Empresa, EmpresaReturn>().ReverseMap();
            CreateMap<Empresa, EmpresaCreate>().ReverseMap();
            //cupongenerico
            CreateMap<CuponGenerico, CuponGenericoReturn>().ReverseMap();
            CreateMap<CuponGenerico, CuponGenericoCreate>().ReverseMap();
            //cuponImagen
            CreateMap<CuponImagen, CuponImagenReturn>().ReverseMap();
            CreateMap<CuponImagen, CuponImagenCreate>().ReverseMap();
            //HistorialCupones
            CreateMap<HistorialCupones, HistorialCuponesReturn>().ReverseMap();
            CreateMap<HistorialCupones, HistorialCuponesCreate>().ReverseMap();
        }
    }
}