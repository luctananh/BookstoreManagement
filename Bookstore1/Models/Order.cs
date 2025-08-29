using Bookstore1.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore1.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int CustomerId { get; set; } // FK to Customer

        public DateTime OrderDate { get; set; }

        [Required]
        public string Status { get; set; } // Ví dụ: "Chờ xác nhận", "Đang giao", etc.

        [MaxLength(50)]
        public string PaymentMethod { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }

        public int? PromoId { get; set; } // FK to Promotion, optional

        // Navigation properties
        public Customer Customer { get; set; }
        public Promotion Promotion { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}