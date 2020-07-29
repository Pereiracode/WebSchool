using DAO.Utils;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class LoginController : Controller
    {
        private HttpClient client = new HttpClient();

        public LoginController()
        {
            client.BaseAddress = new Uri("http://localhost:56545/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autenticar(LoginViewModel loginviewmodel)
        {
            HttpResponseMessage resposta = client.GetAsync($"api/usuarios/{loginviewmodel.Login}").Result;
            Usuario usuario = resposta.Content.ReadAsAsync<Usuario>().Result;

            if (usuario != null)
            {
                loginviewmodel.Senha = CriptoHash.GerarHash(loginviewmodel.Senha);

                if (loginviewmodel.Senha.Equals(usuario.Senha))
                {
                    Session["userLogin"] = usuario.Login;
                    return RedirectToAction("Index", "Usuario");
                }
                else
                {
                    ModelState.AddModelError("IdErro", "Usuário e/ou Senha inválidos");
                    return View("Index");
                }
            }
            else
            {
                ModelState.AddModelError("IdErro", "Usuário e/ou Senha inválidos");
                return View("Index");
            }
        }
    }
}