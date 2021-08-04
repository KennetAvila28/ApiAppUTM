using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppUTM.Core.Models;

namespace AppUTM.Core.Interfaces
{
    public interface ICoordinationService
    {
        Task<IEnumerable<Coordination>> GetAllCoordinations();

        Task<Coordination> GetCoordinationById(int id);

        Task<Coordination> CreateCoordination(Coordination newCoordination);

        Task UpdateCoordination(Coordination CoordinationToBeUpdated);

        Task DeleteCoordination(Coordination @Coordination);
    }
}