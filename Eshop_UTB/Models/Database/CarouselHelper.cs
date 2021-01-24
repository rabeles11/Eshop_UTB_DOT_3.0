using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop_UTB.Models.Database
{
    public class CarouselHelper
    {
        public static IList<Carousel> GenerateCarousel()
        {
            IList<Carousel> carousel = new List<Carousel>()
            {
                new Carousel() {
                    ImageSrc = "https://www.cnews.cz/wp-content/uploads/2018/06/google-fotky-photos-1600-vignette.jpg",
                    ImageAlt = "First slide",
                    CarouselContent = "Vítej v karuselu 1"
                },
                new Carousel() {
                    ImageSrc = "https://www.svetandroida.cz/media/2017/05/archiv-google-fotky.png",
                    ImageAlt = "Second slide",
                    CarouselContent = "Vítej v karuselu 2"
                },
                new Carousel() {
                    ImageSrc = "https://images.obi.cz/product/CZ/1500x1500/106794_2.jpg",
                    ImageAlt = "Third slide",
                    CarouselContent = "Vítej v karuselu 3 "
                },
                new Carousel() {
                    ImageSrc = "https://www.i60.cz/images/puppy-1903313-1920.jpg",
                    ImageAlt = "Fourth slide",
                    CarouselContent = ""
                }
            };
            return carousel;
        }
    }
}
