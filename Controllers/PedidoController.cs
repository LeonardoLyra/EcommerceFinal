using Ecommerce2021a.Data;
using Ecommerce2021a.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce2021a.Controllers
{
    
    public class PedidoController : Controller
    {
        public IActionResult Index(int id)
        {
            using (var data = new ItemData())
            {
                return View(data.Read(id));
            }   
        }
    }
}
