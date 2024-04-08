using System.ComponentModel.DataAnnotations.Schema;

namespace PC_Spiele.Models
{
    public class CategoryGames
    {
        public int Id { get; set; }

        [ForeignKey("Category")]
        public int Category_id { get; set; }
        public virtual Category? Category { get; set; }
        [ForeignKey("Game")]
        public int Game_id { get; set; }
        public virtual Game? Game { get; set; }
    }
}
