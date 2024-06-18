using System.ComponentModel.DataAnnotations;

namespace MyApiProject.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<Address>? Addresses { get; set; }
    }
}
