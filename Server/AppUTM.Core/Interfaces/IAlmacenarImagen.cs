using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUTM.Core.Interfaces
{
    public interface IAlmacenarImagen
    {
        Task<string> GuardarArchivo(byte[] imagen, string contenedor, string nombre);
        Task<string> EditarArchivo(byte[] contenido, string contenedor, string ruta);
        Task BorraArchivo(string ruta, string contenedor);
    }
}
