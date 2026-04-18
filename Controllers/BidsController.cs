using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthOnlineApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace AuthOnlineApp.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                var items = _context.Bid
                    .Include(b => b.Product)
                    .Include(b => b.User);

                return View(await items.ToListAsync());
            }
            else
            {
                var userId = (await _userManager.GetUserAsync(User)).Id;

                var items = _context.Bid
                    .Where(item => item.UserId == userId)
                    .Include(b => b.Product)
                    .Include(b => b.User);

                return View(await items.ToListAsync());
            }
        }

        // GET: Bids/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var bid = await _context.Bid
                .Include(b => b.Product)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BidId == id);

            if (bid == null) return NotFound();

            return View(bid);
        }

        // GET: Bids/Create
        public async Task<IActionResult> Create(int productId)
        {
            var product = await _context.Product.FindAsync(productId);
            var user = await _userManager.GetUserAsync(User);

            if (product == null) return NotFound();

            if (product.CreatedByUserId == user.Id)
            {
                TempData["Error"] = "You can't bid for your own product.";
                return RedirectToAction("Details", "Products", new { id = productId });
            }

            ViewData["ProductId"] = new SelectList(
                _context.Set<Product>().Where(p => p.Deadline > DateTime.Now),
                "ProductId",
                "Name",
                productId
            );

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", user.Id);

            return View();
        }

        // POST: Bids/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BidId,Amount,CreatedAt,ProductId,UserId")] Bid bid)
        {
            if (ModelState.IsValid)
            {
                var product = await _context.Product.FindAsync(bid.ProductId);
                var user = await _userManager.GetUserAsync(User);

                if (product.CreatedByUserId == user.Id)
                {
                    ModelState.AddModelError("", "You can't bid for your own product.");
                }

                if (bid.Amount <= product.StartingPrice)
                {
                    ModelState.AddModelError("Amount", "Amount should be at least equal to starting price.");
                }

                if (ModelState.IsValid)
                {
                    _context.Add(bid);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Name", bid.ProductId);

            var userId = (await _userManager.GetUserAsync(User)).Id;
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userId);

            return View(bid);
        }

        // GET: Bids/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var bid = await _context.Bid.FindAsync(id);
            if (bid == null) return NotFound();

            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "Name", bid.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bid.UserId);

            return View(bid);
        }

        // POST: Bids/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BidId,Amount,CreatedAt,ProductId,UserId")] Bid bid)
        {
            if (id != bid.BidId) return NotFound();

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
                        return NotFound();
                    else
                        throw;
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
            if (id == null) return NotFound();

            var bid = await _context.Bid
                .Include(b => b.Product)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BidId == id);

            if (bid == null) return NotFound();

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