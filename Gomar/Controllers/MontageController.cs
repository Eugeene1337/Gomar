using AutoMapper;
using Gomar.Models;
using Gomar.Models.ViewModels;
using Gomar.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gomar.Controllers
{
    [Authorize]
    public class MontageController : Controller
    {
        private readonly IMontageService _montageService;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public MontageController(IMontageService montageService, IImageService imageService, IMapper mapper)
        {
            _montageService = montageService;
            _imageService = imageService;
            _mapper = mapper;
        }

        public ActionResult<IList<Montage>> Index()
        {
            var montages = _montageService.Read()
                .Select(x => new Montage()
                {
                    Id = x.Id,
                    ImageName = x.ImageName,
                })
                .ToList();

            return View(montages);
        }

        [HttpGet]
        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<Montage> Create(MontageViewModel montageViewModel)
        {
            var montage = _mapper.Map<Montage>(montageViewModel);
            montage.ImageName = _imageService.SaveImage(montage.ImageFile);
            if (ModelState.IsValid)
            {
                _montageService.Create(montage);
                return RedirectToAction("Index");
            }
            return View(montageViewModel);
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
                _imageService.DeleteImage(oldMontage.ImageName);
                montage.ImageName = _imageService.SaveImage(montage.ImageFile);
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
            return View(montage);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var montage = _montageService.Find(id);
            _imageService.DeleteImage(montage.ImageName);
            _montageService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
