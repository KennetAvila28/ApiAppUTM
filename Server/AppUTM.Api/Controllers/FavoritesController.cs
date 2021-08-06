using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppUTM.Api.DTOS.Favorites;
using AppUTM.Api.Responses;
using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace AppUTM.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFavoriteService _Favoriteservice;

        public FavoritesController(IMapper mapper, IFavoriteService Favoriteservice)
        {
            _mapper = mapper;
            _Favoriteservice = Favoriteservice;
        }

        // GET: api/<FavoriteController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favorites>>> Get()
        {
            try
            {
                var favorites = await _Favoriteservice.GetAllFavorites();
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
        [HttpGet("{id}")]
        public async Task<ActionResult<Favorites>> Get(int id)
        {
            try
            {
                var selectedFavorite = await _Favoriteservice.GetFavoriteById(id);
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
        public async Task<ActionResult> Post(FavoriteCreate FavoriteCreate)
        {
            try
            {
                var favoriteEntity = _mapper.Map<FavoriteCreate, Favorites>(FavoriteCreate);
                await _Favoriteservice.CreateFavorite(favoriteEntity);
                var favoriteResponse = _mapper.Map<Favorites, FavoriteReturn>(favoriteEntity);
                var response = new ApiResponse<FavoriteReturn>(favoriteResponse);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/<FavoriteController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var favoriteForDelete = await _Favoriteservice.GetFavoriteById(id);
                await _Favoriteservice.DeleteFavorite(favoriteForDelete);
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