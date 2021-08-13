using System;
using System.Buffers.Text;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace AppUTM.Helpers
{
    internal static class ImageHelper
    {
        /// <summary>
        /// Convert a image from IFormFile to Base64
        /// </summary>
        /// <param name="image"></param>
        /// <returns>string</returns>
        public static async Task<string> ImageToBase64(InputFileChangeEventArgs e)
        {
            try
            {
                var imgFile = e.File;
                var buffers = new byte[imgFile.Size];
                await imgFile.OpenReadStream().ReadAsync(buffers);
                return Convert.ToBase64String(buffers);
            }
            catch
            {
                throw new ArgumentException("La imagen supera el tamaño maximo, selecciona otra imagen (512kb)");
            }
        }
    }
}