using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCategory_MVC.Models
{
    public class Product
    {
        [Key]
        public int productId { get; set; }

        [Required(ErrorMessage = "phai nhap ten san pham")]
        [StringLength(100)]
        public string? productName { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 100000, ErrorMessage = "Gia phai tu 0 den 100000")]
        public decimal productPrice { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [ForeignKey("Category")]
        public int categoryId { get; set; }

        public Category? Category { get; set; }
    }
}