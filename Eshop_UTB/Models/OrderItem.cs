using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop_UTB.Models
{
    [Table(nameof(OrderItem))]
    public class OrderItem:Entity
    {

        [ForeignKey(nameof(Order))]
        public int OrderID { get; set; }

        [ForeignKey(nameof(Order))]
        public int ProductID { get; set; }

        public int Amount { get; set; }

        public decimal Price { get; set; }
        
        public Order order { get; set; }

        public Product Product { get; set; }
    }
}
