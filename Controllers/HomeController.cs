using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using AuthOnlineApp.Data;
using AuthOnlineApp.Models;
using System.Diagnostics;
    
namespace AuthOnlineApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(string? searchString, decimal? minPrice, decimal? maxPrice)
        {
            ViewData["ShowDashboard"] = true; // Set to true to show the dashboard section on Home page

            var products = _context.Product.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString));
            }

            if (minPrice.HasValue || maxPrice.HasValue)
            {
                var filteredProducts = products
                    .Select(p => new
                    {
                        Product = p,
                        HighestBid = p.Bids != null && p.Bids.Any() ? p.Bids.Max(b => b.Amount) : p.StartingPrice
                    });

                if (minPrice.HasValue)
                {
                    filteredProducts = filteredProducts.Where(p => p.HighestBid >= minPrice.Value);
                }

                if (maxPrice.HasValue)
                {
                    filteredProducts = filteredProducts.Where(p => p.HighestBid <= maxPrice.Value);
                }

                products = filteredProducts.Select(p => p.Product);
            }

            return View(await products.ToListAsync()); // This line uses ToListAsync
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
