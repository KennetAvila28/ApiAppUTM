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
        Task<IEnumerable<Favorites>> GetAllFavorites();

        Task<Favorites> GetFavoriteById(string clave);

        Task<Favorites> CreateFavorite(Favorites newFavorite);

        Task DeleteFavorite(Favorites favorite);
    }
}