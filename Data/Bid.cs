using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AuthOnlineApp.Data
{
    public class Bid
    {
        public int BidId { get; set; }

        [Precision(18, 2)]
        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
