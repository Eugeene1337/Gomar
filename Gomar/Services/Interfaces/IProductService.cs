using Gomar.Models;

namespace Gomar.Services.Interfaces
{
    public interface IProductService
    {
        public Product Create(Product product);

        public IList<Product> Read();

        public Product Find(string id);

        public void Update(Product product);

        public void Delete(string id);
    }
}
