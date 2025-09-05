using Microsoft.AspNetCore.Mvc;

namespace Bookstore1.Models
{
    public class Search : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
  