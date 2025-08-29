using System.ComponentModel.DataAnnotations;

namespace BookStore1.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        // Navigation property
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}