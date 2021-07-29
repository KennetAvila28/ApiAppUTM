using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace AppUTM.Api.Helpers
{
    public static class ImageHelper
    {
        /// <summary>
        /// Convert a image from IFormFile to Base64
        /// </summary>
        /// <param name="image"></param>
        /// <returns>string</returns>
        public static string ImageToBase64(IFormFile image)
        {
            if (image == null) throw new ArgumentNullException(nameof(image));
            using var ms = new MemoryStream();
            image.CopyToAsync(ms);
            var bytes = ms.ToArray();
            return Convert.ToBase64String(bytes);
        }
    }
}