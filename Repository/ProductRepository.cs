using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyApiProject.Models;
namespace MyApiProject.Repository
{
    public class ProductRepository
    {
        private readonly IMongoCollection<Product> _products;
        public ProductRepository(IMongoClient mongoClient, IOptions<MongoDBSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _products = database.GetCollection<Product>("Products");
        }

        // Implement methods to interact with MongoDB collections
        // Example: GetProducts, InsertProduct, UpdateProduct, DeleteProduct, etc.

    }

}
