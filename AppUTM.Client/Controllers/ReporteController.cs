using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppUTM.Client.Controllers
{
    public class ReporteController : Controller
    {
        public IActionResult Reporte()
        {
            return View();
        }
    }
}
