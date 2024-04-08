using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PC_Spiele.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Invoice { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public IdentityUser? user { get; set; }
    }
}
