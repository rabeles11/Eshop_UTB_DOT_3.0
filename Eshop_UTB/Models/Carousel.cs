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
    [Table("Carousel")]
    public class Carousel: Entity
    {
        
        [NotMapped]
        [FileContentType("image")]
        public IFormFile Image { get; set; }
        [Required]
        [StringLength(255)]
        public string ImageSrc { get; set; }
        [Required]
        [StringLength(25)]
        public string ImageAlt { get; set; }
        [Required]
        public string CarouselContent { get; set; }
    }
}
