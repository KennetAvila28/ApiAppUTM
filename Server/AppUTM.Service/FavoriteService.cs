using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AppUTM.Core.Repositories;
using AppUTM.Data;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Event>> GetAllFavorites() => await _context.Events.Include(x => x.EventFavorite).ToListAsync();

        public async Task<Favorites> GetFavoriteById(int id) =>
            await _context.Favorites.Include(x => x.EventsfFavorites).ThenInclude(y => y.Event)
                .SingleOrDefaultAsync(z => z.Id == id);

        public async Task<Favorites> CreateFavorite(Favorites newFavorite)
        {
            await _unitOfWork.Favorites.Add(newFavorite);
            await _unitOfWork.CommitAsync();
            return newFavorite;
        }

        public Task UpdateFavorite(Favorites favoriteToBeUpdated)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteFavorite(Favorites favorite)
        {
            _unitOfWork.EventFavorite.RemoveRange(favorite.EventsfFavorites);
            _unitOfWork.Favorites.Remove(favorite);
            await _unitOfWork.CommitAsync();
        }
    }
}