using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthOnlineApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis;

namespace AuthOnlineApp.Controllers
{
    public class BidsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BidsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Bids
        [Authorize]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                var items = _context.Bid
                    .Include(b => b.Product).Include(b => b.User);
                return View(await items.ToListAsync());
            }
            else
            {
                var userId = (await _userManager.GetUserAsync(User)).Id;
                var items = _context.Bid
                    .Where(item => item.UserId == userId)
                    .Include(b => b.Product).Include(b => b.User);
                return View(await items.ToListAsync());
            }
        }

        // GET: Bids/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bid = await _context.Bid
                .Include(b => b.Product)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BidId == id);
            if (bid == null)
            {
                return NotFound();
            }

            return View(bid);
        }

        // GET: Bids/Create
        [Authorize]
        public async Task<IActionResult> CreateAsync(int productId)
        {
            var product = await _context.Product.FindAsync(productId);
            var user = await _userManager.GetUserAsync(User);

            if (product == null)
            {
                return NotFound();
            }

            if (product.CreatedByUserId == user.Id)
            {
                return Forbid();
            }
            ViewData["ProductId"] = new SelectList(_context.Set<Product>()
                .Where(item => item.Deadline > DateTime.Now), "ProductId", "Name", productId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", user.Id);
            return View();
        }

        [Authorize]
        // POST: Bids/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BidId,Amount,CreatedAt,ProductId,UserId")] Bid bid)
        {
            if (ModelState.IsValid)
            {
                var product = _context.Product.Find(bid.ProductId);
                var user = await _userManager.GetUserAsync(User);


                if (product.CreatedByUserId == user.Id)
                {
                    return Forbid();
                }

                var startingPrice = product.StartingPrice;
                if (bid.Amount <= startingPrice)
                {
                    ModelState.AddModelError("Amount", "Amount should be at least equal to starting price.");
                }
                else
                {
                    _context.Add(bid);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Name", bid.ProductId);
            var userId = (await _userManager.GetUserAsync(User)).Id;
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userId);
            return View();
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
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Name", bid.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bid.UserId);
            return View(bid);
        }

        // POST: Bids/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BidId,Amount,CreatedAt,ProductId,UserId")] Bid bid)
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
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Name", bid.ProductId);
            var userId = (await _userManager.GetUserAsync(User)).Id;
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userId);
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
                .Include(b => b.Product)
                .Include(b => b.User)
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
