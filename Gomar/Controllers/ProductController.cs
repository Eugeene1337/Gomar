using AutoMapper;
using Gomar.Models;
using Gomar.Models.ViewModels;
using Gomar.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gomar.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {

        private readonly IProductService _productService;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IImageService imageService, IMapper mapper)
        {
            _productService = productService;
            _imageService = imageService;
            _mapper = mapper;
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
                })
                .ToList();

            return View(products);
        }

        [HttpGet]
        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<Product> Create(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<Product>(productViewModel);
            product.ImageName = _imageService.SaveImage(product.ImageFile);
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
        public ActionResult Edit(Product product)
        {
            var oldProduct = _productService.Find(product.Id);
            
            if (product.ImageFile != null)
            {
                _imageService.DeleteImage(oldProduct.ImageName);
                product.ImageName = _imageService.SaveImage(product.ImageFile);
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
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var product = _productService.Find(id);
            _imageService.DeleteImage(product.ImageName);
            _productService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
