using AppUTM.Api.DTOS.Modules;
using AppUTM.Api.DTOS.Roles;
using AppUTM.Api.DTOS.Users;
using AppUTM.Core.Models;
using AutoMapper;
using System;
using AppUTM.Api.DTOS.Coordinations;
using AppUTM.Api.DTOS.Events;
using AppUTM.Api.DTOS.Favorites;

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
            //Module
            CreateMap<Module, ModuleCreate>().ReverseMap();
            CreateMap<Module, ModuleReturn>().ReverseMap();
            CreateMap<Module, ModuleForUpdateDto>().ReverseMap();
            CreateMap<ModuleCreate, Module>().AfterMap(
                ((source, destination) =>
                {
                    destination.CreateAt = DateTime.Now;
                    destination.Status = true;
                }));
            CreateMap<ModuleReturn, Module>();
            //Events
            CreateMap<Event, EventCreate>().ReverseMap();
            CreateMap<Event, EventReturn>().ReverseMap();
            CreateMap<Event, EventForUpdateDto>().ReverseMap();
            CreateMap<EventCreate, Event>().AfterMap(
                ((source, destination) =>
                {
                    destination.CreateAt = DateTime.Now;
                    destination.Status = true;
                }));
            CreateMap<EventReturn, Event>();

            CreateMap<Coordination, CoordinationCreate>().ReverseMap();
            CreateMap<Coordination, CoordinationReturn>().ReverseMap();
            CreateMap<Coordination, CoordinationForUpdateDto>().ReverseMap();
            CreateMap<CoordinationCreate, Coordination>().AfterMap(
                ((source, destination) =>
                {
                    destination.CreateAt = DateTime.Now;
                    destination.Status = true;
                }));
            CreateMap<CoordinationReturn, Coordination>();

            CreateMap<Favorites, FavoriteCreate>().ReverseMap();
            CreateMap<Favorites, FavoriteReturn>().ReverseMap();
            CreateMap<FavoriteReturn, Favorites>();
        }
    }
}