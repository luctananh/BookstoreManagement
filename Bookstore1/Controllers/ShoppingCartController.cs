using Bookstore1.Models;
using Bookstore1.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bookstore1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartApiController : ControllerBase
    {
        private readonly ShoppingCartService _shoppingCartService;

        public CartApiController(ShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpPost("UpdateQuantity")]
        public async Task<IActionResult> UpdateQuantity([FromForm] int bookId, [FromForm] int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            if (quantity > 0)
            {
                await _shoppingCartService.UpdateItemQuantity(userId, bookId, quantity);
            }
            else
            {
                await _shoppingCartService.RemoveItemFromCart(userId, bookId);
            }

            var total = await _shoppingCartService.GetCartTotal(userId);
            return Ok(new { success = true, newTotal = total });
        }

        [HttpPost("RemoveItem")]
        public async Task<IActionResult> RemoveItem([FromForm] int bookId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            await _shoppingCartService.RemoveItemFromCart(userId, bookId);

            var total = await _shoppingCartService.GetCartTotal(userId);
            return Ok(new { success = true, newTotal = total });
        }
    }

    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCartService _shoppingCartService;

        public ShoppingCartController(ShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [Route("ShoppingCart")]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Users");
            }

            var cart = await _shoppingCartService.GetCart(userId);
            var viewModel = new ShoppingCartViewModel();

            if (cart != null && cart.ShoppingCartItems != null)
            {
                foreach (var item in cart.ShoppingCartItems)
                {
                    viewModel.Items.Add(new ShoppingCartItemViewModel
                    {
                        BookId = item.BookId,
                        Title = item.Book.Title,
                        Author = item.Book.Author,
                        ImageUrl = item.Book.ImageUrl,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        IsSelected = true // Default to selected
                    });
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(int bookId, int quantity = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Users");
            }

            await _shoppingCartService.AddItemToCart(userId, bookId, quantity);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyNow(int bookId, int quantity = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Users");
            }

            await _shoppingCartService.AddItemToCart(userId, bookId, quantity);
            return RedirectToAction("Create", "Orders");
        }
    }
}
