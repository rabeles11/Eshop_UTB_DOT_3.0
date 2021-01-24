using Eshop_UTB.Models.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop_UTB.Models
{
    [Table(nameof(Order))]
    public class Order : Entity
    {
        
        [StringLength(25)]
        [Required]
        public string OrderNumber { get; set; }
        [Required]
        public double TotalPrice { get; set; }
       
        public string StavReklamace { get; set; }
        public string PopisReklamace { get; set; }
        public string DuvodReklamace { get; set; }

        [ForeignKey(nameof(Identity.User))]
        public int UserId { get; set; }
        public User User { get; set; }

        public IList<OrderItem> OrderItems { get; set; }
        
    }
}
