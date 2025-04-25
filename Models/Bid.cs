namespace AuthOnlineApp.Models
{
    public class Bid
    {
        public int BidId { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }

        public Bid()
        {
            
        }
    }
}
