using AppUTM.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUTM.Core.Interfaces
{
   public interface IModuleService
    {
        Task<IEnumerable<Module>> GetAllModules();

        Task<Module> GetModuleById(int id);

        Task<Module> CreateModule(Module newModule);

        Task UpdateModule(Module ModuleToBeUpdated, Module module);

        Task DeleteModule(Module module);
    }
}
