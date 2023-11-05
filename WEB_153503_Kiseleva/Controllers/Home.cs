using Microsoft.AspNetCore.Mvc;

namespace WEB_153503_Kiseleva.Controllers
{
    public class Home : Controller
    {
        public IActionResult Index()
        {
            ViewData["Message"] = "Лабораторная работа №2";
            return View();
        }

        public class ListDemo
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public ActionResult MenuPartial() 
        {
            return PartialView("_MenuPartial");
        }

        public ActionResult UserInfoPartial()
        {
            return PartialView("_UserInfoPartial");
        }
    }
}
