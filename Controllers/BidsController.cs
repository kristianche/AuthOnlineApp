using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthOnlineApp.Data;
using AuthOnlineApp.Models;

namespace AuthOnlineApp.Controllers
{
    public class BidsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BidsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bids
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bid.ToListAsync());
        }

        // GET: Bids/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bid = await _context.Bid
                .FirstOrDefaultAsync(m => m.BidId == id);
            if (bid == null)
            {
                return NotFound();
            }

            return View(bid);
        }

        // GET: Bids/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bids/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BidId,Price,Date,UserId,ProductId")] Bid bid)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bid);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bid);
        }

        // GET: Bids/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bid = await _context.Bid.FindAsync(id);
            if (bid == null)
            {
                return NotFound();
            }
            return View(bid);
        }

        // POST: Bids/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BidId,Price,Date,UserId,ProductId")] Bid bid)
        {
            if (id != bid.BidId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bid);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BidExists(bid.BidId))
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
            return View(bid);
        }

        // GET: Bids/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bid = await _context.Bid
                .FirstOrDefaultAsync(m => m.BidId == id);
            if (bid == null)
            {
                return NotFound();
            }

            return View(bid);
        }

        // POST: Bids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bid = await _context.Bid.FindAsync(id);
            if (bid != null)
            {
                _context.Bid.Remove(bid);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BidExists(int id)
        {
            return _context.Bid.Any(e => e.BidId == id);
        }
    }
}
