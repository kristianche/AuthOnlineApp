using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AuthOnlineApp.Data
{
    public class Bid
    {
        public int BidId { get; set; }

        [Required(ErrorMessage = "Bid amount is required!")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Bid amount must be greater than zero!")]
        [Precision(18, 2)]
        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Product ID is required.")]
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }

        [Required(ErrorMessage = "User ID is required!")]
        public string UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
