// lớp này sẽ tương tác với MongoDB
// sử dụng IMongoClient và IMongoDatabse
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyApiProject.Models;

namespace MyApiProject.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");
        public IMongoCollection<Order> Orders => _database.GetCollection<Order>("Orders");
    }
}
