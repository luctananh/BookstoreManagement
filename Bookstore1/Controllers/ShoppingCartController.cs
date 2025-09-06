using Bookstore1.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BookStore1.Models;
using System.Threading.Tasks;

namespace Bookstore1.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCartService _shoppingCartService;

        public ShoppingCartController(ShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                // Handle anonymous users or redirect to login
                return RedirectToAction("Login", "Users"); // Assuming UsersController handles login
            }

            var cart = await _shoppingCartService.GetCart(userId);
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(int bookId, int quantity = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Users");
            }

            await _shoppingCartService.AddItemToCart(userId, bookId, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(int bookId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Users");
            }

            await _shoppingCartService.RemoveItemFromCart(userId, bookId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int bookId, int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Users");
            }

            await _shoppingCartService.UpdateItemQuantity(userId, bookId, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Users");
            }

            await _shoppingCartService.ClearCart(userId);
            return RedirectToAction("Index");
        }
    }
}