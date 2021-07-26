using AppUTM.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace AppUTM.Services
{
    public class AlmacenarImagen : IAlmacenarImagen
    {
        private readonly IHostingEnvironment _env;        

        public AlmacenarImagen(IHostingEnvironment environment)
        {
            this._env = environment; 
        }

        public Task BorraArchivo(string imagen, string contenedor)
        {
            if(imagen != null)
            {                
                string directorioArchivo = Path.Combine(_env.WebRootPath, contenedor, imagen);
                if (File.Exists(directorioArchivo))
                    File.Delete(directorioArchivo);
            }
            return Task.FromResult(0);
        }

        public async Task<string> EditarArchivo(byte[] contenido, string contenedor, string nombreImagen)
        {
            await BorraArchivo(nombreImagen, contenedor);
            return await GuardarArchivo(contenido, contenedor, nombreImagen);
        }

        public async Task<string> GuardarArchivo(byte[] contenido, string contenedor, string nombre)
        {
            var nombreArchivo = $"{Guid.NewGuid()}-{nombre}";
            string folder = Path.Combine(_env.WebRootPath, contenedor);
            string ruta = Path.Combine(folder, nombreArchivo);
            await File.WriteAllBytesAsync(ruta, contenido);
            return nombreArchivo;       
        }
    }
}
