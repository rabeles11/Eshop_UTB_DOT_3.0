using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop_UTB.Models.Database
{
    public class ProductHelper
    {
        public static IList<Product> GenerateProduct()
        {
            IList<Product> product = new List<Product>()
            {
                new Product() {
                    ProductName = "Beer",
                    ImageSrc = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcReS3vFskLfPIGH7l4u71voNg8zeR0ySz9aew&usqp=CAU",
                    ImageAlt = "Pivo",
                    ProductDescription = "Thats one is a golden one",
                    price = 400,
                },
                new Product() {
                    ProductName = "Cheese",
                    ImageSrc = "https://wi-images.condecdn.net/image/PD527AaVVwE/crop/1440/0.5235602094240838/f/Cheese_01.jpg",
                    ImageAlt = "Sýr",
                    ProductDescription = "What an awesome yellow cheese",
                    price = 500,
                },
                new Product() {
                    ProductName = "T-shirt",
                    ImageSrc = "https://bezvatriko.cz/25260-thickbox_default/panske-tricko-s-potiskem-polib-si.jpg",
                    ImageAlt = "T-shirt",
                    ProductDescription = "U have to get one now",
                    price = 700,
                },
                new Product() {
                    ProductName = "Dog",
                    ImageSrc = "https://www.i60.cz/images/puppy-1903313-1920.jpg",
                    ImageAlt = "Dog",
                    ProductDescription = "If u feel lonely just get this one lil paw",
                    price = 900,
                }
            };
            return product;
        }
    }
}
