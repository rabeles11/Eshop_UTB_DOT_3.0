using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eshop_UTB.Models;
using Eshop_UTB.Models.Database;
using Microsoft.AspNetCore.Authorization;
using Eshop_UTB.Models.Identity;

namespace Eshop_UTB.areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manger))]
    public class OrdersController : Controller
    {
        private readonly EshopDBContex _context;

        public OrdersController(EshopDBContex context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var eshopDBContex = _context.Orders.Include(o => o.User);
            return View(await eshopDBContex.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public async Task<IActionResult> StavReklamace(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public async Task<IActionResult> ReklamaceDecline(int id)
        {
            if (ModelState.IsValid)
            {
                Order orderIt = _context.Orders.Where(orderIt => orderIt.ID == id).FirstOrDefault();
                if (orderIt != null)
                {
                    orderIt.StavReklamace = "Reklamace zamítnuta";


                    await _context.SaveChangesAsync();

                    return View(orderIt);

                }

                return NotFound();
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> ZamitnutíReklamaceText(Order order)
        {
            if (ModelState.IsValid)
            {
                Order orderIt = _context.Orders.Where(orderIt => orderIt.OrderNumber == order.OrderNumber).FirstOrDefault();
                if (orderIt != null)
                {
                    orderIt.DuvodReklamace = order.DuvodReklamace;


                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));

                }

                return NotFound();
            }
            else
            {
                return NotFound();
            }
        }
        
        public async Task<IActionResult> ReklamaceFinished(int id)
        {
            if (ModelState.IsValid)
            {
                Order orderIt = _context.Orders.Where(orderIt => orderIt.ID == id).FirstOrDefault();
                if (orderIt != null)
                {
                    orderIt.StavReklamace = "Reklamace dokončena";


                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));

                }

                return NotFound();
            }
            else
            {
                return NotFound();
            }
        }
        public async Task<IActionResult> ReklamaceAccept(int id)
        {
            if (ModelState.IsValid)
            {
                Order orderIt = _context.Orders.Where(orderIt => orderIt.ID == id).FirstOrDefault();
                if (orderIt != null)
                {
                    orderIt.StavReklamace = "Reklamace potvrzena";


                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));

                }

                return NotFound();
            }
            else
            {
                return NotFound();
            }
        }


        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderNumber,TotalPrice,UserId,ID,DateTimeCreated")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", order.UserId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", order.UserId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderNumber,TotalPrice,UserId,ID,DateTimeCreated")] Order order)
        {
            if (id != order.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", order.UserId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.ID == id);
        }
    }
}
