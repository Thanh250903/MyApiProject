using MongoDB.Driver;
using MyApiProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApiProject.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(IMongoDatabase database)
        {
            _products = database.GetCollection<Product>("Products");
        }

        public async Task<List<Product>> GetAsync() =>
            await _products.Find(product => true).ToListAsync();

        public async Task<Product> GetAsync(string id) =>
            await _products.Find<Product>(product => product.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Product product) =>
            await _products.InsertOneAsync(product);

        public async Task UpdateAsync(string id, Product productIn) =>
            await _products.ReplaceOneAsync(product => product.Id == id, productIn);

        public async Task RemoveAsync(string id) =>
            await _products.DeleteOneAsync(product => product.Id == id);
    }
}


