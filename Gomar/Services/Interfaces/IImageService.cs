namespace Gomar.Services.Interfaces
{
    public interface IImageService
    {
        public string SaveImage(IFormFile imageFile);

        public void DeleteImage(string imageName);
    }
}
