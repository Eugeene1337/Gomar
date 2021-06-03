using Gomar.Models;
using Gomar.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gomar.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {

        private readonly ProductService _prodSvc;

        public ProductController(ProductService productService)
        {
            _prodSvc = productService;
        }

        [AllowAnonymous]
        public ActionResult<IList<Product>> Index() => View(_prodSvc.Read());

        [HttpGet]
        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<Product> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _prodSvc.Create(product);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult<Product> Edit(string id) =>
            View(_prodSvc.Find(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _prodSvc.Update(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            _prodSvc.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
