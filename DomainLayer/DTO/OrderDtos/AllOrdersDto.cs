using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTO.OrderDtos
{
    public class AllOrdersDto
    {
        public long OrderId { get; set; }
        public string Description { get; set; }
        public DateTime DateOfOrder { get; set; }
        public long BookId { get; set; }
    }
}
