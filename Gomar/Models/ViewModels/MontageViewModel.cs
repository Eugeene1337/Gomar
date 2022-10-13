using System.ComponentModel.DataAnnotations;

namespace Gomar.Models.ViewModels
{
    public class MontageViewModel
    {
        [Required(ErrorMessage = "Pole obowiązkowe")]
        public IFormFile ImageFile { get; set; }
    }
}
