using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace AuthOnlineApp.Data
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot be longer than 100 characters!")]
        public string Name { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Starting price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Starting price must be greater than zero!")]
        [Precision(18, 2)]
        public decimal StartingPrice { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime Deadline { get; set; }

        [Required(ErrorMessage = "CreatedByUserId is required.")]
        public string CreatedByUserId { get; set; }

        public virtual ApplicationUser? CreatedByUser { get; set; }

        public virtual ICollection<Bid>? Bids { get; set; }
    }
}
