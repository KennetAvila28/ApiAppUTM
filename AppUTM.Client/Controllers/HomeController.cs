using AppUTM.Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web.Resource;

namespace AppUTM.Client.Controllers
{
    [AuthorizeForScopes(ScopeKeySection = "EventAdmin:EventAdminScope")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly IConfiguration _configuration;
        public HomeController(ITokenAcquisition tokenAcquisition, ILogger<HomeController> logger,
            IConfiguration configuration)
        {
            _tokenAcquisition = tokenAcquisition;
            _logger = logger;
            _configuration = configuration;
        }

        [Authorize]
        [AuthorizeForScopes(ScopeKeySection = "EventAdmin:EventAdminScope")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}