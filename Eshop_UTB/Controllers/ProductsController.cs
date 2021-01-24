using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop_UTB.Models;
using Eshop_UTB.Models.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Eshop_UTB.Controllers
{
    public class ProductsController : Controller
    {
        readonly EshopDBContex EshopDBContext;

        public ProductsController(EshopDBContex eshopDBContex)
        {
            this.EshopDBContext = eshopDBContex;
            
        }
        public IActionResult Detail(int id)
        {
            Product productIt = EshopDBContext.Products.Where(productIt => productIt.ID == id).FirstOrDefault();
            if (productIt != null)
            {
                return View(productIt);
                // tady spravit :-D 
            }
            else
            {
                return NotFound();
            }
        }
    }
}
