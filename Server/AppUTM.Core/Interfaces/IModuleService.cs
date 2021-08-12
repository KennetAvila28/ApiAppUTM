using AppUTM.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppUTM.Core.Interfaces
{
    public interface IModuleService
    {
        Task<IEnumerable<Module>> GetAllModules();

        Task<Module> GetModuleById(int id);

        Task<Module> CreateModule(Module newModule);

        Task UpdateModule(Module ModuleToBeUpdated);

        Task DeleteModule(Module Module);
    }
}