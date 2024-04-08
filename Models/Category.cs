namespace PC_Spiele.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CategoryGames> CategoryGames { get; set; } = new List<CategoryGames>();
    }
}
