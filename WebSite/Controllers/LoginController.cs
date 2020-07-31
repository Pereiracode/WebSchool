using DAO.Utils;
using Model.Models;
using Newtonsoft.Json;
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
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
        }

        // GET: Login
        public ActionResult Index()
        {
            HttpCookie cookie = new HttpCookie("cor_preferida");
            cookie.Value = "xaxa";
            cookie.Expires = DateTime.Now.AddMinutes(15);
            cookie.HttpOnly = true;

            cookie.Values.Add("cor", "blue");
            cookie.Values.Add("tamanho", "80");
            Response.Cookies.Add(cookie);

            var cookie2 = Request.Cookies["cor_preferida"];

            if (cookie2 == null)
            {
                ViewBag.cor = "red";
            }
            else
            {
                ViewBag.cor = cookie2.Values.Get("cor");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Autenticar(LoginViewModel loginviewmodel)
        {
            if (AutorizacaoEToken(loginviewmodel.Login, loginviewmodel.Senha))
            {
                Session["userLogin"] = loginviewmodel.Login;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("IdErro", "Usuário e/ou Senha inválidos");
                return View("Index");
            }
        }

        public ActionResult Desautenticar()
        {
            Session["userLogin"] = null;
            return RedirectToAction("Index", "Home");
        }

        private bool AutorizacaoEToken(string login, string senha)
        {
            HttpResponseMessage resposta;

            var httpContent = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("username", login),
                new KeyValuePair<string, string>("password", senha),
                new KeyValuePair<string, string>("grant_type", "password")
            }
                );

            resposta = client.PostAsync($"/token", httpContent).Result;

            var str = resposta.Content.ReadAsStringAsync().Result;
            var objAnonimo = new { access_token = "", token_type = "", expires_in = "" };
            var objMaterial = JsonConvert.DeserializeAnonymousType(str, objAnonimo);

            if (objMaterial.access_token.Equals(""))
                return false;

            Session["access_token"] = objMaterial.access_token;
            Session["token_type"] = objMaterial.token_type;

            return true;
            //client.DefaultRequestHeaders.Authorization =
            //  new AuthenticationHeaderValue(objMaterial.token_type, objMaterial.access_token);
        }
    }
}