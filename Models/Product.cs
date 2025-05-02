using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace AuthOnlineApp.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        
        public string Name { get; set; }

        
        public string Description { get; set; }

        [Precision(18, 2)]
        public decimal StartingPrice { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string CreatedByUserId { get; set; }
        public ApplicationUser CreatedByUser { get; set; }

        public ICollection<Bid> Bids { get; set; }

    }
}
