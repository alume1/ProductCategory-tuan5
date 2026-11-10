using System.ComponentModel.DataAnnotations;

namespace ProductCategory_MVC.Models
{
    public class Category
    {
        [Key]
        public int categoryId { get; set; }

        [Required(ErrorMessage = "Phai nhap ten danh muc")]
        [StringLength(100)]
        public string? categoryName { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}