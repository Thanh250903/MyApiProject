using MongoDB.Driver;
using MyApiProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace MyApiProject.Services
{
    public class OrderService
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderService(IMongoDatabase database)
        {
            _orders = database.GetCollection<Order>("Orders");
        }

        public async Task<List<Order>> GetAsync() =>
            await _orders.Find(order => true).ToListAsync();

        public async Task<Order> GetAsync(string id) =>
            await _orders.Find<Order>(order => order.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Order order) =>
            await _orders.InsertOneAsync(order);

        public async Task UpdateAsync(string id, Order orderIn) =>
            await _orders.ReplaceOneAsync(order => order.Id == id, orderIn);
        
        public async Task RemoveAsync(string id) => 
            await _orders.DeleteOneAsync(order => order.Id == id);
    }
}
