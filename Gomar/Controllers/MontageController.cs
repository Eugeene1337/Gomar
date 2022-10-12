using Gomar.Models;
using Gomar.Services;
using Gomar.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<Montage> Create(Montage montage)
        {
            montage.ImageName = ImageHelper.SaveImage(montage.ImageFile, _hostEnvironment);
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
        public ActionResult Edit(Montage montage)
        {
            var oldMontage = _montageService.Find(montage.Id);
            if (montage.ImageFile != null)
            {
                ImageHelper.DeleteImage(oldMontage.ImageName, _hostEnvironment);
                montage.ImageName = ImageHelper.SaveImage(montage.ImageFile, _hostEnvironment);
            }

            if (ModelState.IsValid)
            {
                _montageService.Update(montage);
                return RedirectToAction("Index");
            }
            return View(montage);
        }

        [HttpGet]
        public ActionResult Delete(string id)
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
            ImageHelper.DeleteImage(montage.ImageName, _hostEnvironment);
            _montageService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
