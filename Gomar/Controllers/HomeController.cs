using Gomar.Models;
using Gomar.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Gomar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MontageService _montageService;
        public HomeController(ILogger<HomeController> logger, MontageService montageService)
        {
            _montageService = montageService;
            _logger = logger;
        }

        public ActionResult<IList<Montage>> Index()
        {
            var montages = _montageService.Read()
                .Select(x => new Montage()
                {
                    Id = x.Id,
                    ImageName = x.ImageName,
                    ImageSrc = String.Format("{0}://{1}{2}/img/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName)
                })
                .ToList();
            int rows = montages.Count % 3 == 0 ? montages.Count / 3 : montages.Count / 3 + 1;
            ViewData["Rows"] = rows;

            return View(montages);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
