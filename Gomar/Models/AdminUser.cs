namespace Gomar.Models
{
    public interface IAdminUser
    {
        string Email { get; set; }
        string Name { get; set; }
        string Password { get; set; }
    }

    public class AdminUser : IAdminUser
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
