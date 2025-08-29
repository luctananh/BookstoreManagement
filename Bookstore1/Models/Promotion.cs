using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore1.Models
{
    public class Promotion
    {
        [Key]
        public int PromoId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        [Required]
        public string DiscountType { get; set; } // Ví dụ: "%" hoặc "Số tiền"

        [Column(TypeName = "decimal(10,2)")]
        public decimal Value { get; set; }

        public string Conditions { get; set; }

        public DateTime ExpiryDate { get; set; }

        // Navigation property
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}