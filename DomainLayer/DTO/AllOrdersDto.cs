using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTO
{
    public class AllOrdersDto
    {
        [Key]
        public long OrderId { get; set; }
        public string Description { get; set; }
        public long BookId { get; set; }
    }
}
