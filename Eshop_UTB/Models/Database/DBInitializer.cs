
using Eshop_UTB.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop_UTB.Models.Database
{
    public static class DBInitializer
    {
        public static void Initialize(EshopDBContex dBContex)
        {

            dBContex.Database.EnsureCreated();

            if (dBContex.Carousels.Count() == 0)
            {
                IList<Carousel> carousels = CarouselHelper.GenerateCarousel();
                foreach (var item in carousels)
                    dBContex.Carousels.Add(item);
                dBContex.SaveChanges();
            }
            if (dBContex.Products.Count() == 0)
            { 
                IList<Product> products = ProductHelper.GenerateProduct();
                foreach (var item in products)
                    dBContex.Products.Add(item);
                dBContex.SaveChanges();
                
            }

           
        }
        public async static void EnsureRoleCreated(IServiceProvider serviceProvider)
        {
            using (var services = serviceProvider.CreateScope())
            {
                RoleManager<Role> roleManager = services.ServiceProvider.GetRequiredService<RoleManager<Role>>();

                string [] roles = Enum.GetNames(typeof(Roles));

                foreach (var role in roles)
                {
                   await roleManager.CreateAsync(new Role(role));
                }
            }
        }
        public async static void EnsureAdminCreated(IServiceProvider serviceProvider)
        {
            using (var services = serviceProvider.CreateScope())
            {
                UserManager<User> userManager = services.ServiceProvider.GetRequiredService<UserManager<User>>();
                User admin  = new User() 
                { 
                    UserName = "admin", 
                    Email = "admin_rabel@utb.cz", 
                    Name = "David", 
                    LastName = "Rábel", 
                    EmailConfirmed = true 
                };
                var password = "    admin";

                User adminInDatabase = await userManager.FindByNameAsync(admin.UserName);

                if (adminInDatabase == null)
                {
                    IdentityResult iResult = await userManager.CreateAsync(admin, password);

                    if (iResult.Succeeded)
                    {
                        string[] roles = Enum.GetNames(typeof(Roles));

                        foreach (var role in roles)
                        {
                            await userManager.AddToRoleAsync(admin, role);
                        }
                    }
                    else if (iResult.Errors != null && iResult.Errors.Count() > 0)
                    {
                        foreach ( var error in iResult.Errors)
                        {
                            Debug.WriteLine("Error during role creation: " + error.Code + " -> " + error.Description);
                        }
                       
                    }
                }
                User manager = new User()
                {
                    UserName = "manager",
                    Email = "manager_rabel@utb.cz",
                    Name = "David",
                    LastName = "Rábel",
                    EmailConfirmed = true
                };
               
                    
                User managerInDatabase = await userManager.FindByNameAsync(manager.UserName);

                if (managerInDatabase == null)
                {
                    IdentityResult iResult = await userManager.CreateAsync(manager, password);

                    if (iResult.Succeeded)
                    {
                        string[] roles = Enum.GetNames(typeof(Roles));

                        foreach (var role in roles)
                        {
                            if(role != Roles.Admin.ToString()) 
                                await userManager.AddToRoleAsync(manager, role);
                        }
                    }
                    else if (iResult.Errors != null && iResult.Errors.Count() > 0)
                    {
                        foreach (var error in iResult.Errors)
                        {
                            Debug.WriteLine("Error during role creation: " + error.Code + " -> " + error.Description);
                        }

                    }
                }
            }
        }

    }
}
