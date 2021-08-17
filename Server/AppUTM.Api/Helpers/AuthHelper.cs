using AppUTM.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AppUTM.Api.Helpers
{
    public static class AuthHelper
    {
        public static bool Authorize(HttpRequest request, int moduleId, IAuthorizationService authorization)
        {
            var header = request.Headers.TryGetValue("UserUTM", out var userEmail);
            if (!header)
            {
                return false;
            }
            var result = authorization.ValidateUser(userEmail, moduleId, false);
            return result;
        }
    }
}