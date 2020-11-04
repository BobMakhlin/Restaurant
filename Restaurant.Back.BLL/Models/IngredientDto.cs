namespace Restaurant.Back.BLL.Models
{
    public class IngredientDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Photo { get; set; }
        public decimal Price { get; set; }
        public bool IsEnabled { get; set; }
    }
}
