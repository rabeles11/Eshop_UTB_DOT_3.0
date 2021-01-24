using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Eshop_UTB.Models;
using Eshop_UTB.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;

namespace Eshop_UTB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        CarouselProductViewModel carouselproduct;
        EshopDBContex eshopDBContex;
        public HomeController(ILogger<HomeController> logger, EshopDBContex eshopDBContex)
        {
            carouselproduct = new CarouselProductViewModel();
            this.eshopDBContex = eshopDBContex;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {

            carouselproduct.Carousels = await eshopDBContex.Carousels.ToListAsync();
            carouselproduct.Products = await eshopDBContex.Products.ToListAsync();
            return View(carouselproduct);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var featureException = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            this._logger.LogError("Exception occured: " + featureException.Error.ToString() + Environment.NewLine +"Exception Path: " +featureException.Path);
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ErrorCodeStatus(int? statuscode = null)
        {
            string originalURL = String.Empty;
            var features = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            if(features != null)
            {
                originalURL = features.OriginalPathBase + features.OriginalPath + features.OriginalQueryString;
            }

            var statCode = statuscode.HasValue ? statuscode.Value : 0;
            this._logger.LogWarning("Status Code: "+ statCode + "Original URL: " + originalURL);
            
            if(statCode == 404)
            {
                _404ViewModel vm404 = new _404ViewModel()
                {
                    StatusCode = statCode

                };
                return View(statuscode.ToString(),vm404);
            }
            ErrorCodeStatusViewModel vm = new ErrorCodeStatusViewModel()
            {
                StatusCode = statCode,
                OriginalURL = originalURL
            };

            return View(vm);
        }
    }
}
