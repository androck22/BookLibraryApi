using System.ComponentModel.DataAnnotations;

namespace DomainLayer.DTO
{
    public class OrderDto
    {
        [Key]
        public long OrderId { get; set; }
        public string Description { get; set; }
        public long BookId { get; set; }
    }
}
