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

        public async Task<Favorites> GetFavoriteById(string clave) =>
            await _context.Favorites.Include(x => x.Events).AsNoTracking()
                .SingleOrDefaultAsync(z => z.Clave == clave);

        public async Task<Favorites> CreateFavorite(Favorites newFavorite)
        {
            var favorites = await GetFavoriteById(newFavorite.Clave);
            if (favorites != null)
            {
                var eventFavorite = new EventFavorites
                {
                    FavoriteId = favorites.Id,
                    EventId = newFavorite.EventId
                };
                await _context.EventFavorites.AddAsync(eventFavorite);
                await _unitOfWork.CommitAsync();
                return newFavorite = null;
            }

            var evebntId = await _unitOfWork.Events.GetById(newFavorite.EventId);
            newFavorite.Events = new List<Event>();
            newFavorite.Events.Add(evebntId);
            await _unitOfWork.Favorites.Add(newFavorite);
            await _unitOfWork.CommitAsync();
            return newFavorite;
        }

        public async Task DeleteFavorite(Favorites favorite)
        {
            var favorites = await GetFavoriteById(favorite.Clave);
            var eventFavorite = new EventFavorites
            {
                FavoriteId = favorites.Id,
                EventId = favorite.EventId
            };
            _context.EventFavorites.Remove(eventFavorite);
            await _unitOfWork.CommitAsync();
        }
    }
}