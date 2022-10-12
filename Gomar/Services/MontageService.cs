using Gomar.Models;
using MongoDB.Driver;

namespace Gomar.Services
{
    public class MontageService
    {
        private readonly IMongoCollection<Montage> _montages;

        public MontageService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _montages = database.GetCollection<Montage>("Montages");
        }

        public Montage Create(Montage montage)
        {
            _montages.InsertOne(montage);
            return montage;
        }

        public IList<Montage> Read() =>
            _montages.Find(sub => true).ToList();

        public Montage Find(string id) =>
            _montages.Find(sub => sub.Id == id).SingleOrDefault();

        public void Update(Montage montage) =>
            _montages.ReplaceOne(sub => sub.Id == montage.Id, montage);

        public void Delete(string id) =>
            _montages.DeleteOne(sub => sub.Id == id);
    }
}
