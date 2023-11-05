using Microsoft.AspNetCore.Mvc;

namespace WEB_153503_Kiseleva.Components
{
    public class CartViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
