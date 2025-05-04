using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace AuthOnlineApp.Data 
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }

    }
}
