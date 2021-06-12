using Gomar.Models;
using Gomar.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gomar.Controllers
{
    [Authorize]
    public class TextController : Controller
    {

        private readonly TextService _textService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public TextController(TextService textService, IWebHostEnvironment hostEnvironment)
        {
            _textService = textService;
            _hostEnvironment = hostEnvironment;
        }

        public ActionResult<IList<Text>> Index() => View(_textService.Read());

        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<Text> Create(Text submission)
        {
            if (ModelState.IsValid)
            {
                _textService.Create(submission);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult<Text> Edit(string id) =>
            View(_textService.Find(id));


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Text text)
        {
            if (ModelState.IsValid)
            {
                _textService.Update(text);
                return RedirectToAction("Index");
            }
            return View(text);
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var text = _textService.Find(id);
            if (text == null)
            {
                return NotFound();
            }
            return View(text);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            _textService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
