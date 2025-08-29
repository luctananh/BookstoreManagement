using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore1.Models
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }

        [MaxLength(50)]
        public string DateRange { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Revenue { get; set; }

        public string TopBooks { get; set; } // JSON hoặc TEXT cho danh sách

        public DateTime GeneratedDate { get; set; }
    }
}