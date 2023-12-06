using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_153503_KISELEVA.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WEB_153503_KISELEVA.Controllers
{
    public class Home : Controller
    {
        public IActionResult Index()
        {
            ViewData["LabTitle"] = "Лабораторная работа №2";

            List<ListDemo> listItems = new(){
            new ListDemo { Id = 1, Name = "Item 1" },
            new ListDemo { Id = 2, Name = "Item 2" },
            new ListDemo { Id = 3, Name = "Item 3" },
            };

            SelectList selectList = new(listItems, "Id", "Name");
            ViewData["ListDemo"] = selectList;

            return View();
        }
    }
}

