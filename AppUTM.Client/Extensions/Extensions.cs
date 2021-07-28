using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;

namespace AppUTM.Client.Extensions
{
    public static class Extensions
    {
        //private static HttpClient _httpClient;

        //private static async Task PrepareAuthenticatedClient(this IConfiguration configuration, ITokenAcquisition tokenAcquisition,  string _scope )
        //{
        //    var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { _scope });
        //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        //    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //}

        //public static async Task<string> Getprofile()
        //{
        //    var json = await _httpClient.GetStringAsync(_configuration["getuseraddress"]);
        //    return json;
        //}

        //public static async Task<string> GetPhoto()
        //{
        //    var resp = await _httpClient.GetAsync(_configuration["photouser"]);
        //    var buffer = await resp.Content.ReadAsByteArrayAsync();
        //    var byteArray = buffer.ToArray();

        //    string base64String = Convert.ToBase64String(byteArray);

        //    return base64String;
        //}
    }
}