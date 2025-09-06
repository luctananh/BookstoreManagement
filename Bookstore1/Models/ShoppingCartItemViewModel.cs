using BookStore1.Models;

namespace Bookstore1.Models
{
    public class ShoppingCartItemViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool IsSelected { get; set; } // Property restored
    }
}