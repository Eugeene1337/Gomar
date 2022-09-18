using System.ComponentModel.DataAnnotations;

namespace Gomar.Models
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = "Pole obowiązkowe")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole obowiązkowe")]
        public string Password { get; set; }
    }
}
