using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
    public class Order
    {
        [Key]
        public long OrderId { get; set; }
        public string Description { get; set; }
        public long BookId { get; set; }
    }
}
