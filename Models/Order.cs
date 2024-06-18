using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MyApiProject.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("UserId")]
        public int UserId { get; set; }
        [BsonElement("Items")]
        public List<OrderItem> Items { get; set; }
    }
}
