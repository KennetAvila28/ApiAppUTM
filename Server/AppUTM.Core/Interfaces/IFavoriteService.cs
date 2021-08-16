using AppUTM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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