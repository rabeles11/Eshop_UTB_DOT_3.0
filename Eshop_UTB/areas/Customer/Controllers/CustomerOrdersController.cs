using Eshop_UTB.Models;
using Eshop_UTB.Models.ApplicationServices;
using Eshop_UTB.Models.Database;
using Eshop_UTB.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop_UTB.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = nameof(Roles.Customer))]
    public class CustomerOrdersController : Controller
    {

        ISecurityApplicationService iSecure;
        EshopDBContex EshopDBContext;

        public CustomerOrdersController(ISecurityApplicationService iSecure, EshopDBContex eshopDBContext)
        {
            this.iSecure = iSecure;
            EshopDBContext = eshopDBContext;
        }
        public async Task<IActionResult> EditWaranty(int id)
        {
            if (ModelState.IsValid)
            {
                Order orderIt = EshopDBContext.Orders.Where(orderIt => orderIt.ID == id).FirstOrDefault();
                if (orderIt != null)
                {
                    orderIt.StavReklamace = "Žádost podána";


                    await EshopDBContext.SaveChangesAsync();

                    return View(orderIt);

                }

                return NotFound();
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> PopisReklamace(Order order)
        {
            if (ModelState.IsValid)
            {
                Order orderIt = EshopDBContext.Orders.Where(orderIt => orderIt.OrderNumber == order.OrderNumber).FirstOrDefault();
                if (orderIt != null)
                {
                    orderIt.PopisReklamace = order.PopisReklamace;


                    await EshopDBContext.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));

                }

                return NotFound();
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                User currentUser = await iSecure.GetCurrentUser(User);
                if (currentUser != null)
                {
                    IList<Order> userOrders = await this.EshopDBContext.Orders
                                                                        .Where(or => or.UserId == currentUser.Id)
                                                                        .Include(o => o.User)
                                                                        .Include(o => o.OrderItems)
                                                                        .ThenInclude(oi => oi.Product)
                                                                        .ToListAsync();
                    return View(userOrders);
                }
            }

            return NotFound();
        }
    }
}
