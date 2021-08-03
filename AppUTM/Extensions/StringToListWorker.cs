using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AppUTM.Models.Users;
using static System.Int32;

namespace AppUTM.Extensions
{
    public static class StringToListWorker
    {
        /// <summary>
        /// Convert json string to list worker
        /// </summary>
        /// <returns>Workers list </returns>
        public static async Task<IList<UserCreate>> Convert(HttpClient http, string input)
        {
            var users = new List<UserCreate>();
            var jsonElements = JsonSerializer.Deserialize<IList<JsonElement>>(input);
            var currentUsers = await GetExtensions.GetAllUsers(http);
            if (jsonElements != null)
                users.AddRange(from jsonElement in jsonElements
                               select JsonDocument.Parse(jsonElement.ToString() ?? string.Empty)
                    into jDoc
                               select jDoc.RootElement
                    into rootElement
                               let user = new UserCreate
                               {
                                   ClaveEmpleado = Parse(rootElement.GetProperty("ClaveEmpleado").GetString() ?? string.Empty),
                                   Nombres = rootElement.GetProperty("PrimerNombre").GetString() + " " + rootElement.GetProperty("SegundoNombre").GetString(),
                                   ApellidoPaterno = rootElement.GetProperty("PrimerApellido").GetString(),
                                   ApellidoMaterno = rootElement.GetProperty("SegundoApellido").GetString(),
                                   Correo = rootElement.GetProperty("CorreoInstitucional").GetString()
                               }
                               where currentUsers.All(u => u.ClaveEmpleado != Parse(rootElement.GetProperty("ClaveEmpleado").GetString()!))
                               select user);
            return users;
        }
    }
}