using MongoDB.Bson.Serialization.Attributes;

namespace MyApiProject.Models
{
    public class OrderItem
    {
        [BsonElement("ProductId")]
        public string ProductId { get; set; }
        [BsonElement("Quantity")]
        public int Quantity { get; set; }
    }
}
