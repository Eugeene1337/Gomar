using Gomar.Models;
using Gomar.Services;
using Gomar.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gomar.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {

        private readonly ProductService _productService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(ProductService productService, IWebHostEnvironment hostEnvironment)
        {
            _productService = productService;
            _hostEnvironment = hostEnvironment;
        }

        public ActionResult<IList<Product>> Index()
        {
            var products = _productService.Read()
                .Select(x => new Product()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category,
                    Description = x.Description,
                    ImageName = x.ImageName,
                    ImageSrc = String.Format("{0}://{1}{2}/img/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName)
                })
                .ToList();

            return View(products);
        }

        [HttpGet]
        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            product.ImageName = await ImageHelper.SaveImage(product.ImageFile, _hostEnvironment);
            if (ModelState.IsValid)
            {
                _productService.Create(product);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult<Product> Edit(string id) =>
            View(_productService.Find(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Product product)
        {
            var oldProduct = _productService.Find(product.Id);
            
            if (product.ImageFile != null)
            {
                ImageHelper.DeleteImage(oldProduct.ImageName, _hostEnvironment);
                product.ImageName = await ImageHelper.SaveImage(product.ImageFile, _hostEnvironment);
            }
            else
            {
                product.ImageName = oldProduct.ImageName;
            }
            

            if (ModelState.IsValid)
            {
                _productService.Update(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _productService.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            product.ImageSrc = String.Format("{0}://{1}{2}/img/{3}", Request.Scheme, Request.Host, Request.PathBase, product.ImageName);
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var product = _productService.Find(id);
            ImageHelper.DeleteImage(product.ImageName, _hostEnvironment);
            _productService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
