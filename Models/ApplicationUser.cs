using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace AuthOnlineApp.Models 
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Product> Products { get; set; }
        public ICollection<Bid> Bids { get; set; }

    }
}
