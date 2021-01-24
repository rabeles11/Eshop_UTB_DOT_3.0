using Eshop_UTB.Models;
using Eshop_UTB.Models.Database;
using Eshop_UTB.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop_UTB.areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manger))]
    public class ProductController: Controller
    {
        IWebHostEnvironment Env;
        readonly EshopDBContex EshopDBContext;

        public ProductController(EshopDBContex eshopDBContex, IWebHostEnvironment env)
        {
            this.EshopDBContext = eshopDBContex;
            this.Env = env;
        }

        public async Task<IActionResult> Select()
        {
            CarouselProductViewModel product = new CarouselProductViewModel();
            product.Products = await EshopDBContext.Products.ToListAsync();
            return View(product);

        }
        public IActionResult Edit(int id)
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
        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                Product productIt = EshopDBContext.Products.Where(productIt => productIt.ID == product.ID).FirstOrDefault();
                if (productIt != null)
                {
                    productIt.ProductName = product.ProductName;
                    productIt.ImageAlt = product.ImageAlt;
                    FileUpload fup = new FileUpload(Env.WebRootPath, "Products", "image");
                    productIt.ImageSrc = product.ImageSrc = await fup.FileUploadAsync(product.Image);
                    productIt.ProductDescription = product.ProductDescription;
                    productIt.price = product.price;

                    await EshopDBContext.SaveChangesAsync();

                    return RedirectToAction(nameof(Select));
                    // tady spravit :-D 
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return View(product);
            }
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                FileUpload fup = new FileUpload(Env.WebRootPath, "Products", "image");
                product.ImageSrc = await fup.FileUploadAsync(product.Image);
                EshopDBContext.Products.Add(product);

                await EshopDBContext.SaveChangesAsync();
                return RedirectToAction(nameof(Select));
            }
            else
            {
                return View(product );
            }
        }
        public IActionResult Delete(int id)
        {
            Product productIt = EshopDBContext.Products.Where(productIt => productIt.ID == id).FirstOrDefault();
            if (productIt != null)
            {
                EshopDBContext.Products.Remove(productIt);
                EshopDBContext.SaveChanges();
                return RedirectToAction(nameof(Select));
            }
            else
            {
                return NotFound();
            }
        }

    }
}

