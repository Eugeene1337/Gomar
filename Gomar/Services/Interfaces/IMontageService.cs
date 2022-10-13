using Gomar.Models;

namespace Gomar.Services.Interfaces
{
    public interface IMontageService
    {
        public Montage Create(Montage montage);

        public IList<Montage> Read();

        public Montage Find(string id);

        public void Update(Montage montage);

        public void Delete(string id);
    }
}
