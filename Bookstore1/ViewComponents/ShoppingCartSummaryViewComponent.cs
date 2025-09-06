using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Bookstore1.Services;
using BookStore1.Models;

namespace Bookstore1.ViewComponents
{
    public class ShoppingCartSummaryViewComponent : ViewComponent
    {
        private readonly ShoppingCartService _shoppingCartService;

        public ShoppingCartSummaryViewComponent(ShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                // If user is not logged in, return an empty cart or handle as anonymous
                // For now, let's return an empty ShoppingCart model
                return View(new BookStore1.Models.ShoppingCart());
            }

            var cart = await _shoppingCartService.GetCart(userId);
            return View(cart);
        }
    }
}