using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Automated_Wedding_Application.Data;

namespace Automated_Wedding_Application.Models
{
    public class CustomerCartsController : Controller
    {
        private readonly Automated_Wedding_ApplicationContext _context;

        public CustomerCartsController(Automated_Wedding_ApplicationContext context)
        {
            _context = context;
        }

        // GET: CustomerCarts
        public async Task<IActionResult> Index()
        {
            return View(await _context.CustomerCart.ToListAsync());
        }

        // GET: CustomerCarts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerCart = await _context.CustomerCart
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (customerCart == null)
            {
                return NotFound();
            }

            return View(customerCart);
        }

        // GET: CustomerCarts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerCarts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartId,UserId,PlannerId,ServiceId,ServiceName,Quantity,ServiceCost,DeliveryDate,Checkout")] CustomerCart customerCart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerCart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerCart);
        }

        // GET: CustomerCarts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerCart = await _context.CustomerCart.FindAsync(id);
            if (customerCart == null)
            {
                return NotFound();
            }
            return View(customerCart);
        }

        // POST: CustomerCarts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartId,UserId,PlannerId,ServiceId,ServiceName,Quantity,ServiceCost,DeliveryDate,Checkout")] CustomerCart customerCart)
        {
            if (id != customerCart.CartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerCart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerCartExists(customerCart.CartId))
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
            return View(customerCart);
        }

        // GET: CustomerCarts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerCart = await _context.CustomerCart
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (customerCart == null)
            {
                return NotFound();
            }

            return View(customerCart);
        }

        // POST: CustomerCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerCart = await _context.CustomerCart.FindAsync(id);
            _context.CustomerCart.Remove(customerCart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerCartExists(int id)
        {
            return _context.CustomerCart.Any(e => e.CartId == id);
        }
    }
}
