using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gomar.Models;
using MongoDB.Driver;

namespace Gomar.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _products = database.GetCollection<Product>("Products");
        }

        public Product Create(Product product)
        {
            _products.InsertOne(product);
            return product;
        }

        public IList<Product> Read() =>
            _products.Find(sub => true).ToList();

        public Product Find(string id) =>
            _products.Find(sub => sub.Id == id).SingleOrDefault();

        public void Update(Product product) =>
            _products.ReplaceOne(sub => sub.Id == product.Id, product);

        public void Delete(string id) =>
            _products.DeleteOne(sub => sub.Id == id);
    }
}
