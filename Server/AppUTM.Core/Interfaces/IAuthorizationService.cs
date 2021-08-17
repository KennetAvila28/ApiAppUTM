using AppUTM.Core.Models;

namespace AppUTM.Core.Interfaces
{
    public interface IAuthorizationService
    {
        public bool ValidateUser(string userName, int moduleId, bool isWrite);

        public bool Login(string email);

        public User GetUserByEmail(string email);
    }
}