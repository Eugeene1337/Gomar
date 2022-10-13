using Gomar.Models;
using Gomar.Services;
using Gomar.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gomar.Controllers
{
    [Authorize]
    public class TextController : Controller
    {

        private readonly ITextService _textService;

        public TextController(ITextService textService)
        {
            _textService = textService;
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
