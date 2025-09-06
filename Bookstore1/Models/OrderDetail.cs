using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore1.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }

        public int OrderId { get; set; } // FK to Order

        public int Id { get; set; } // FK to Book

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal PricePerItem { get; set; }

        // Navigation properties
        public Order Order { get; set; }
        public Book Book { get; set; }
    }
}