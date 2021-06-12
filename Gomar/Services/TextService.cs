using Gomar.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gomar.Services
{
    public class TextService
    {
        private readonly IMongoCollection<Text> _texts;

        public TextService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _texts = database.GetCollection<Text>("Texts");
        }

        public Text Create(Text text)
        {
            _texts.InsertOne(text);
            return text;
        }

        public IList<Text> Read() =>
            _texts.Find(sub => true).ToList();

        public Text Find(string id) =>
            _texts.Find(sub => sub.Id == id).SingleOrDefault();

        public void Update(Text text) =>
            _texts.ReplaceOne(sub => sub.Id == text.Id, text);

        public void Delete(string id) =>
            _texts.DeleteOne(sub => sub.Id == id);
    }
}

