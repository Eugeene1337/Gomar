using Gomar.Models;

namespace Gomar.Services.Interfaces
{
    public interface ITextService
    {
        public Text Create(Text text);

        public IList<Text> Read();

        public Text Find(string id);

        public void Update(Text text);

        public void Delete(string id);
    }
}
