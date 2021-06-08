using Gomar.Models;
using Gomar.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Gomar.Controllers
{
    [Authorize]
    public class MontageController : Controller
    {
        private readonly MontageService _montageService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public MontageController(MontageService montageService, IWebHostEnvironment hostEnvironment)
        {
            _montageService = montageService;
            _hostEnvironment = hostEnvironment;
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

            return View(montages);
        } 
        [HttpGet]
        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Montage>> Create(Montage montage)
        {
            montage.ImageName = await SaveImage(montage.ImageFile);
            if (ModelState.IsValid)
            {
                _montageService.Create(montage);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult<Montage> Edit(string id) =>
            View(_montageService.Find(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Montage montage)
        {
            var oldMontage = _montageService.Find(montage.Id);
            if (montage.ImageFile != null)
            {
                DeleteImage(oldMontage.ImageName);
                montage.ImageName = await SaveImage(montage.ImageFile);
            }

            if (ModelState.IsValid)
            {
                _montageService.Update(montage);
                return RedirectToAction("Index");
            }
            return View(montage);
        }

        [HttpGet]
        public ActionResult Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var montage = _montageService.Find(id);
            if (montage == null)
            {
                return NotFound();
            }
            montage.ImageSrc = String.Format("{0}://{1}{2}/img/{3}", Request.Scheme, Request.Host, Request.PathBase, montage.ImageName);
            return View(montage);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var montage = _montageService.Find(id);
            DeleteImage(montage.ImageName);
            _montageService.Delete(id);
            return RedirectToAction("Index");
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "img", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }

        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "img", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }
}
