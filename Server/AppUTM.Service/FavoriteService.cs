using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AppUTM.Core.Repositories;
using AppUTM.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppUTM.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DataContext _context;

        public FavoriteService(IUnitOfWork unitOfWork, DataContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<IEnumerable<Favorites>> GetAllFavorites() => await _context.Favorites.Include(x => x.Events).ToListAsync();

        public async Task<Favorites> GetFavoriteById(int id) =>
            await _context.Favorites.Include(x => x.Events)
                .SingleOrDefaultAsync(z => z.Id == id);

        public async Task<Favorites> CreateFavorite(Favorites newFavorite)
        {
            if (!await _context.Favorites.AnyAsync(x => x.Clave != newFavorite.Clave)) return newFavorite = null;
            await _unitOfWork.Favorites.Add(newFavorite);
            await _unitOfWork.CommitAsync();
            return newFavorite;
        }

        public async Task DeleteFavorite(Favorites favorite)
        {
            _unitOfWork.Favorites.Remove(favorite);
            await _unitOfWork.CommitAsync();
        }
    }
}