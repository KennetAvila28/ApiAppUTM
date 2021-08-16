using AppUTM.Core.Interfaces;
using AppUTM.Core.Models;
using AppUTM.Core.Repositories;
using AppUTM.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppUTM.Services
{
    public class CoordinationService : ICoordinationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DataContext _context;

        public CoordinationService(IUnitOfWork unitOfWork, DataContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<IEnumerable<Coordination>> GetAllCoordinations() => await _context.Coordinations.Include(c => c.Events).ToListAsync();

        public async Task<Coordination> GetCoordinationById(int id) => await _context.Coordinations.Include(c => c.Events)
            .ThenInclude(y => y.Author).SingleOrDefaultAsync(z => z.Id == id);

        public async Task<Coordination> CreateCoordination(Coordination newCoordination)
        {
            await _unitOfWork.Coordinations.Add(newCoordination);
            await _unitOfWork.CommitAsync();
            return newCoordination;
        }

        public async Task UpdateCoordination(Coordination coordinationToBeUpdated)
        {
            coordinationToBeUpdated.UpdateAt = DateTime.Now;
            _unitOfWork.Coordinations.Update(coordinationToBeUpdated);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteCoordination(Coordination coordination)
        {
            _unitOfWork.Events.RemoveRange(coordination.Events);
            _unitOfWork.Coordinations.Remove(coordination);
            await _unitOfWork.CommitAsync();
        }
    }
}