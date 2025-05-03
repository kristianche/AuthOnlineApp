using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace AuthOnlineApp.Data 
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Product> Products { get; set; }
        public ICollection<Bid> Bids { get; set; }

    }
}
