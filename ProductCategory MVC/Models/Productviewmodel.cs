namespace ProductCategory_MVC.Models
{
    public class Productviewmodel
    {
        public int productId { get; set; }
        public string? productName { get; set; }
        public decimal productPrice { get; set; }
        public string? Description { get; set; }
        public string? categoryName { get; set; }
    }
}
