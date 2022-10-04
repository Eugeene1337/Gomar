using Gomar.Models;
using Gomar.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Gomar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MontageService _montageService;
        private readonly ProductService _productService;
        private readonly TextService _textService;
        
        public HomeController(ILogger<HomeController> logger, MontageService montageService, ProductService productService, TextService textService)
        {
            _montageService = montageService;
            _productService = productService;
            _textService = textService;
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

            var text = _textService.Read()
               .Where(x => x.Name == "O nas")
               .SingleOrDefault();

            ViewData["Text"] = text.Content;

            return View(montages);
        }

        [Route("Okna")]
        public ActionResult<IList<Product>> Windows()
        {
            var windows = _productService.Read()
                .Select(x => new Product()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category,
                    Description = x.Description,
                    ImageName = x.ImageName,
                    ImageSrc = String.Format("{0}://{1}{2}/img/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName)
                })
                .Where(x => x.Category==Category.Okna)
                .ToList();
            

            return View(windows);
        }

        [Route("Drzwi")]
        public ActionResult<IList<Product>> Doors()
        {
            var doors = _productService.Read()
                .Select(x => new Product()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category,
                    Description = x.Description,
                    ImageName = x.ImageName,
                    ImageSrc = String.Format("{0}://{1}{2}/img/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName)
                })
                .Where(x => x.Category == Category.Drzwi)
                .ToList();


            return View(doors);
        }

        [Route("Bramy")]
        public ActionResult<IList<Product>> Gates()
        {
            var gates = _productService.Read()
                .Select(x => new Product()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category,
                    Description = x.Description,
                    ImageName = x.ImageName,
                    ImageSrc = String.Format("{0}://{1}{2}/img/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName)
                })
                .Where(x => x.Category == Category.Bramy)
                .ToList();


            return View(gates);
        }

        [Route("Rolety")]
        public ActionResult<IList<Product>> Blinds()
        {
            var blinds = _productService.Read()
                .Select(x => new Product()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category,
                    Description = x.Description,
                    ImageName = x.ImageName,
                    ImageSrc = String.Format("{0}://{1}{2}/img/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName)
                })
                .Where(x => x.Category == Category.Rolety)
                .ToList();


            return View(blinds);
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
