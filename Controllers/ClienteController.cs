using Ecommerce2021a.Data;
using Ecommerce2021a.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce2021a.Controllers
{
    public class ClienteController : Controller
    {
        //private static List<ClienteL clientes = new List<Cliente>();

        [HttpGet]
        public IActionResult Index(Cliente novoCliente)
        {
            var user = HttpContext.Session.GetString("user");

            novoCliente = JsonSerializer.Deserialize<Cliente>(user);

            return View(novoCliente);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(IFormCollection cliente)
        {
            string nome = cliente["Nome"];
            string email = cliente["Email"];
            string senha = cliente["Senha"];

            if (nome.Length < 6)
            {
                ViewBag.Mensagem = "Nome deve conter 6 ou mais carecteres";
            }
            if (!email.Contains("@"))
            {
                ViewBag.Mensagem = "Email inválido";
                return View();
            }
            if (senha.Length < 6)
            {
                ViewBag.Mensagem = "Senha deve conter 6 caracteres ou mais";
                return View();
            }

            var novoCliente = new Cliente();
            novoCliente.Nome = cliente["nome"];
            novoCliente.Email = cliente["email"];
            novoCliente.Senha = cliente["senha"];

            using (var data = new ClienteData())
                data.Create(novoCliente);

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IFormCollection cliente)
        {
            string nome = cliente["Nome"];
            string email = cliente["Email"];
            string senha = cliente["Senha"];

            if (nome.Length < 6)
            {
                ViewBag.Mensagem = "Nome deve conter 6 ou mais carecteres";
            }
            if (!email.Contains("@"))
            {
                ViewBag.Mensagem = "Email inválido";
                return View();
            }
            if (senha.Length < 6)
            {
                ViewBag.Mensagem = "Senha deve conter 6 caracteres ou mais";
                return View();
            }

            var novoCliente = new Cliente();
            novoCliente.Nome = cliente["nome"];
            novoCliente.Email = cliente["email"];
            novoCliente.Senha = cliente["senha"];

            using (var data = new ClienteData())
                data.Create(novoCliente);

            return RedirectToAction("Index", novoCliente);
        }

        [HttpPost]
        public IActionResult Read(IFormCollection cliente)
        {
            string nome = cliente["Nome"];
            string email = cliente["Email"];
            string senha = cliente["Senha"];

            if (!email.Equals(" "))
            {
                var cli = new Cliente();

                cli.Nome = cliente["Nome"];
                cli.Email = cliente["Email"];
                cli.Senha = cliente["Senha"];

                Cliente c = new Cliente();

                using (var data = new ClienteData())
                    c = data.Read(cli.Email);

                if (c.Senha == cli.Senha)
                {
                    ViewBag.Mensagem = "Olá";
                    return View("Index", c);
                }
                else
                {
                    ViewBag.Mensagem = "Usuário ou senha inválidos";
                    return View("Index", null);
                }

            }

            return View("Create");
        }

        [HttpGet]
        public IActionResult IndexAdmin()
        {
            using (var data = new ClienteData())
                return View(data.Read());
        }

        public IActionResult Delete(int id)
        {
            using (var data = new ClienteData())
                data.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            using (var data = new ClienteData())
                return View(data.Read(id));
        }

        [HttpPost]
        public IActionResult Update(int id, Cliente cliente)
        {
            cliente.IdCliente = id;

            if (!ModelState.IsValid)
                return View(cliente);

            using (var data = new ClienteData())
                data.Update(cliente);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Login()
        {

            return View(new ClienteViewModel());
        }

        [HttpPost]
        public IActionResult Login(ClienteViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (var data = new ClienteData())
            {
                var user = data.Read(model);

                if (user == null)
                {
                    ViewBag.Message = "Email e/ou senha incorretos!";
                    return View(model);
                }

                HttpContext.Session.SetString("user", JsonSerializer.Serialize<Cliente>(user));

                return RedirectToAction("Index", "Produto");
            }
        }

        [HttpGet]
        public IActionResult Sair()
        {

            HttpContext.Session.Remove("user");

            return RedirectToAction("Login", "Cliente");

        }



    }
}
