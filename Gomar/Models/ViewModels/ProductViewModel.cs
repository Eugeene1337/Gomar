using System.ComponentModel.DataAnnotations;

namespace Gomar.Models.ViewModels
{
    public class ProductViewModel
    {
        [Required(ErrorMessage = "Pole obowiązkowe")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole obowiązkowe")]
        public Category Category { get; set; }

        [Required(ErrorMessage = "Pole obowiązkowe")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Pole obowiązkowe")]
        public IFormFile ImageFile { get; set; }
    }
}
