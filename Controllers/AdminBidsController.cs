using AuthOnlineApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthOnlineApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBidsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminBidsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var bids = _context.Bid.ToList();
            return View(bids);
        }
    }
}
