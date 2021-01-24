using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop_UTB.Models;
using Eshop_UTB.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eshop_UTB.Models
{
    public class CarouselProductViewModel
    {
        public IList<Carousel> Carousels { get; set; }
        public IList<Product> Products { get; set; }

        readonly EshopDBContex EshopDBContext;
 
        public CarouselProductViewModel ()
            {
            // zde jak dát asysnc?
 
            
            //Products = EshopDBContext.Products.ToList();

            //Carousels = FakeDB.FakeDB.Carousels;
            //Products = FakeDB.FakeDB.Products;

        }


      
    }
}
