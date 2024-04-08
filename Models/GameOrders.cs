using System.ComponentModel.DataAnnotations.Schema;

namespace PC_Spiele.Models
{
    public class GameOrders
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        [ForeignKey("Game")]
        public int Game_id { get; set; }
        public virtual Game? Game { get; set; }
        [ForeignKey("Order")]
        public int Order_id { get; set; }
        public virtual Order? Order { get; set; }

        public double getTotalPrice()
        {
            return Amount * Game!.Price;
        }
    }
}
