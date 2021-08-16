using AppUTM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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