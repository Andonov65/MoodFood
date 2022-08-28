using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domasna.Domain.DomainModels;
using Domasna.Repository;

namespace Domasna.Controllers
{
    public class TicketProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TicketProducts
        public async Task<IActionResult> Index()
        {
            return View(await _context.TicketProducts.ToListAsync());
        }

        // GET: TicketProducts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketProduct = await _context.TicketProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketProduct == null)
            {
                return NotFound();
            }

            return View(ticketProduct);
        }

        // GET: TicketProducts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TicketProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketProductName,TicketProductImage,TicketProductDescription,TicketProductsMacros,Id")] TicketProduct ticketProduct)
        {
            if (ModelState.IsValid)
            {
                ticketProduct.Id = Guid.NewGuid();
                _context.Add(ticketProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticketProduct);
        }

        // GET: TicketProducts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketProduct = await _context.TicketProducts.FindAsync(id);
            if (ticketProduct == null)
            {
                return NotFound();
            }
            return View(ticketProduct);
        }

        // POST: TicketProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TicketProductName,TicketProductImage,TicketProductDescription,TicketProductsMacros,Id")] TicketProduct ticketProduct)
        {
            if (id != ticketProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketProductExists(ticketProduct.Id))
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
            return View(ticketProduct);
        }

        // GET: TicketProducts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketProduct = await _context.TicketProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketProduct == null)
            {
                return NotFound();
            }

            return View(ticketProduct);
        }

        // POST: TicketProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ticketProduct = await _context.TicketProducts.FindAsync(id);
            _context.TicketProducts.Remove(ticketProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketProductExists(Guid id)
        {
            return _context.TicketProducts.Any(e => e.Id == id);
        }
    }
}
