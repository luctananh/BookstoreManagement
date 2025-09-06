using Bookstore1.Data;
using BookStore1.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore1.Services
{
    public class ShoppingCartService
    {
        private readonly Bookstore1Context _context;

        public ShoppingCartService(Bookstore1Context context)
        {
            _context = context;
        }

        public async Task<ShoppingCart> GetCart(string userIdString)
        {
            if (!int.TryParse(userIdString, out int userId))
            {
                throw new ArgumentException("Invalid User ID format.");
            }

            var cart = await _context.ShoppingCart
                .Include(sc => sc.ShoppingCartItems)
                .ThenInclude(sci => sci.Book)
                .FirstOrDefaultAsync(sc => sc.UserId == userId);

            if (cart == null)
            {
                cart = new ShoppingCart { UserId = userId };
                _context.ShoppingCart.Add(cart);
                await _context.SaveChangesAsync();
            }

            return cart;
        }

        public async Task AddItemToCart(string userIdString, int bookId, int quantity)
        {
            if (!int.TryParse(userIdString, out int userId))
            {
                throw new ArgumentException("Invalid User ID format.");
            }
            var cart = await GetCart(userIdString);
            var book = await _context.Book.FindAsync(bookId);

            if (book == null)
            {
                throw new Exception("Book not found.");
            }

            var cartItem = cart.ShoppingCartItems.FirstOrDefault(item => item.BookId == bookId);

            if (cartItem == null)
            {
                cartItem = new ShoppingCartItem
                {
                    BookId = bookId,
                    Quantity = quantity,
                    Price = book.Price // Assuming Book has a Price property
                };
                cart.ShoppingCartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
                cartItem.Price = book.Price; // Update price in case it changed
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveItemFromCart(string userIdString, int bookId)
        {
            if (!int.TryParse(userIdString, out int userId))
            {
                throw new ArgumentException("Invalid User ID format.");
            }
            var cart = await GetCart(userIdString);
            var cartItem = cart.ShoppingCartItems.FirstOrDefault(item => item.BookId == bookId);

            if (cartItem != null)
            {
                cart.ShoppingCartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveItemsFromCart(string userIdString, int[] bookIds)
        {
            if (!int.TryParse(userIdString, out int userId))
            {
                throw new ArgumentException("Invalid User ID format.");
            }
            var cart = await GetCart(userIdString);
            var itemsToRemove = cart.ShoppingCartItems.Where(item => bookIds.Contains(item.BookId)).ToList();

            if (itemsToRemove.Any())
            {
                foreach (var item in itemsToRemove)
                {
                    cart.ShoppingCartItems.Remove(item);
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateItemQuantity(string userIdString, int bookId, int quantity)
        {
            if (!int.TryParse(userIdString, out int userId))
            {
                throw new ArgumentException("Invalid User ID format.");
            }
            var cart = await GetCart(userIdString);
            var cartItem = cart.ShoppingCartItems.FirstOrDefault(item => item.BookId == bookId);

            if (cartItem != null)
            {
                if (quantity <= 0)
                {
                    cart.ShoppingCartItems.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity = quantity;
                    var book = await _context.Book.FindAsync(bookId);
                    if (book != null)
                    {
                        cartItem.Price = book.Price; // Update price in case it changed
                    }
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearCart(string userIdString)
        {
            if (!int.TryParse(userIdString, out int userId))
            {
                throw new ArgumentException("Invalid User ID format.");
            }
            var cart = await GetCart(userIdString);
            cart.ShoppingCartItems.Clear();
            await _context.SaveChangesAsync();
        }

        public async Task<decimal> GetCartTotal(string userIdString)
        {
            if (!int.TryParse(userIdString, out int userId))
            {
                throw new ArgumentException("Invalid User ID format.");
            }
            var cart = await GetCart(userIdString);
            return cart.ShoppingCartItems.Sum(item => item.Quantity * item.Price);
        }
    }
}