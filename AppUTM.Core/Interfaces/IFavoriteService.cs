using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppUTM.Core.Models;

namespace AppUTM.Core.Interfaces
{
    public interface IFavoriteService
    {
        Task<List<Event>> GetAllFavorites();

        Task<Favorites> GetFavoriteById(int id);

        Task<Favorites> CreateFavorite(Favorites newFavorite);

        Task UpdateFavorite(Favorites favoriteToBeUpdated, Favorites favorite);

        Task DeleteFavorite(Favorites favorite);
    }
}