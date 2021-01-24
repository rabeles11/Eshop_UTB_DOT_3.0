using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Eshop_UTB.Models;
using Eshop_UTB.Models.Database;
using Eshop_UTB.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eshop_UTB.areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manger))]
    public class CarouselController : Controller
    {
        IWebHostEnvironment Env;
        readonly EshopDBContex EshopDBContext;
       
        public CarouselController(EshopDBContex eshopDBContex,IWebHostEnvironment env)
        {
            this.EshopDBContext = eshopDBContex;
            this.Env = env;
        }
        public async  Task<IActionResult> Select()
        {
            CarouselProductViewModel carousel = new CarouselProductViewModel();
            carousel.Carousels = await EshopDBContext.Carousels.ToListAsync();
            return View(carousel);

        }
        public IActionResult Edit(int id)
        {
            Carousel carouselIt = EshopDBContext.Carousels.Where(carouselItem => carouselItem.ID == id).FirstOrDefault();
            if (carouselIt != null)
            {
                return View(carouselIt);
                
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Carousel carousel)
        {
            if (ModelState.IsValid)
            {
                Carousel carouselIt = EshopDBContext.Carousels.Where(carouselItem => carouselItem.ID == carousel.ID).FirstOrDefault();
                if (carouselIt != null)
                {
                    carouselIt.ImageAlt = carousel.ImageAlt;
                    carouselIt.CarouselContent = carousel.CarouselContent;

                    FileUpload fup = new FileUpload(Env.WebRootPath, "Carousels", "image");
                    carouselIt.ImageSrc = carousel.ImageSrc = await fup.FileUploadAsync(carousel.Image);

                    await EshopDBContext.SaveChangesAsync();

                    return RedirectToAction(nameof(Select));

                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return View(carousel);
            }
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Carousel carousel)
        {
            if (ModelState.IsValid)
            {
                FileUpload fup = new FileUpload(Env.WebRootPath,"Carousels","image");
                carousel.ImageSrc = await fup.FileUploadAsync(carousel.Image);
                EshopDBContext.Carousels.Add(carousel);

                await EshopDBContext.SaveChangesAsync();
                return RedirectToAction(nameof(Select));
            }
            else
            {
                return View(carousel);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            Carousel carouselIt = EshopDBContext.Carousels.Where(carouselItem => carouselItem.ID == id).FirstOrDefault();
            if (carouselIt != null)
            {
                EshopDBContext.Carousels.Remove(carouselIt);
                await EshopDBContext.SaveChangesAsync();
                return RedirectToAction(nameof(Select));
            }
            else
            {
                return NotFound();
            }
        }

    }
}
