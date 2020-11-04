namespace Restaurant.Back.DAL.MsSqlServer.Models
{
    public partial class ProductIngredient
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public int ProductId { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual Product Product { get; set; }
    }
}
