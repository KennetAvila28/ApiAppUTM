using AppUTM.Api.DTOS.Favorites;
using AppUTM.Api.Responses;
using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppUTM.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFavoriteService _favoriteService;

        public FavoritesController(IMapper mapper, IFavoriteService favoriteservice)
        {
            _mapper = mapper;
            _favoriteService = favoriteservice;
        }

        // GET: api/<FavoriteController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favorites>>> Get()
        {
            try
            {
                var favorites = await _favoriteService.GetAllFavorites();
                var favoriteList = _mapper.Map<IEnumerable<Favorites>, IEnumerable<FavoriteReturn>>(favorites);
                var response = new ApiResponse<IEnumerable<FavoriteReturn>>(favoriteList);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/<FavoriteController>/5
        [HttpGet("{clave}")]
        public async Task<ActionResult<Favorites>> Get(string clave)
        {
            try
            {
                var selectedFavorite = await _favoriteService.GetFavoriteById(clave);
                var favoriteDto = _mapper.Map<Favorites, FavoriteReturn>(selectedFavorite);
                var response = new ApiResponse<FavoriteReturn>(favoriteDto);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/<FavoriteController>
        [HttpPost]
        public async Task<ActionResult> Post(FavoriteCreate favoriteCreate)
        {
            try
            {
                var favoriteEntity = _mapper.Map<FavoriteCreate, Favorites>(favoriteCreate);
                var newfavorite = await _favoriteService.CreateFavorite(favoriteEntity);
                if (newfavorite == null) return Ok("Evento agregado");
                var favoriteResponse = _mapper.Map<Favorites, FavoriteReturn>(favoriteEntity);
                var response = new ApiResponse<FavoriteReturn>(favoriteResponse);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException);
            }
        }

        // DELETE api/<FavoriteController>/5
        [HttpDelete]
        public async Task<ActionResult> Delete(FavoriteCreate favoriteCreate)
        {
            try
            {
                var favoriteEntity = _mapper.Map<FavoriteCreate, Favorites>(favoriteCreate);
                await _favoriteService.DeleteFavorite(favoriteEntity);
                var response = new ApiResponse<bool>(true);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}