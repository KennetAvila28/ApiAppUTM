using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUTM.Core.Interfaces
{
    public interface IAuthorizationServices
    {
        public bool ValidateUser(string UserName, string moduleName, bool isWrite);
    }
}
