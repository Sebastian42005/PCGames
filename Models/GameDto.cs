using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PC_Spiele.Models
{
    public class GameDto
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }

        [ForeignKey("PG")]
        public int PG_id { get; set; }
        public virtual PG? PG { get; set; }

        [ForeignKey("Publisher")]
        public int Publisher_id { get; set; }
        public virtual Publisher? Publisher { get; set; }
        public int[] selectedCategories { get; set; }
    }
}
