namespace Gomar.Utils
{
    public static class ImageHelper
    {
        public static async Task<string> SaveImage(IFormFile imageFile, IWebHostEnvironment hostEnvironment)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToShortDateString() + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(hostEnvironment.WebRootPath, "img", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }

        public static void DeleteImage(string imageName, IWebHostEnvironment hostEnvironment)
        {
            var imagePath = Path.Combine(hostEnvironment.WebRootPath, "img", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }
}
