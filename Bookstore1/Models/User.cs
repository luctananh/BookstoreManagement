using System.ComponentModel.DataAnnotations;

namespace BookStore1.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; } // Hashed

        [Required]
        public string Role { get; set; } // Ví dụ: "Admin", "Staff", "Customer"

        [MaxLength(255)]
        public string Email { get; set; }
    }
}