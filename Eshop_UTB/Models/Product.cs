using Eshop_UTB.Models.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop_UTB.Models
{
    public class Product : Entity
    {
       
        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }
        [NotMapped]
        [FileContentType("image")]
        public IFormFile Image { get; set; }
        [Required]
        [StringLength(255)]
        public string ImageSrc { get; set; }
        [Required]
        [StringLength(20)]
        public string ImageAlt { get; set; }
        [Required]
        [StringLength(255)]
        public string ProductDescription { get; set; }
        [Required]
        public int price { get; set; }
    }
}
