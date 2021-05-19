using Ecommerce2021a.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce2021a.Controllers
{
    public class CarrinhoController : Controller
    {
        public IActionResult Index()
        {
            List<Item> lista = new List<Item>();

            

            return View();
        }
    }
}
