using System.Collections.Generic;

namespace Bookstore1.Models
{
    public class ShoppingCartViewModel
    {
        public List<ShoppingCartItemViewModel> Items { get; set; } = new List<ShoppingCartItemViewModel>();
    }
}
