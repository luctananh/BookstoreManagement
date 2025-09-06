using Bookstore1.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore1.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [MaxLength(255)]
        public string Author { get; set; }

        [Required]
        [MaxLength(255)]
        public string Publisher { get; set; }

        public int CategoryId { get; set; } // FK to Category

        public int Stock { get; set; }

        [Column(TypeName = "decimal(10,3)")]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        // Navigation properties
        [ValidateNever]
        public Category Category { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}