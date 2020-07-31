using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using Model.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using WebSite.Filters;

namespace WebSite.Controllers
{
    [EnviaTokenFilter]
    public class UsuarioController : Controller
    {

        public HttpClient client = new HttpClient();

        public UsuarioController()
        {
            client.BaseAddress = new Uri("http://localhost:56545/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        // GET: Usuario
        public ActionResult Index()
        {
            HttpResponseMessage resposta = client.GetAsync("api/usuarios").Result;

            string str = resposta.Content.ReadAsStringAsync().Result;

            List<Usuario> model = JsonConvert.DeserializeObject<List<Usuario>>(str);
            return View(model);
        }

        // GET: Usuario/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HttpResponseMessage resposta = client.GetAsync($"api/usuarios/{id}").Result;

            string str = resposta.Content.ReadAsStringAsync().Result;

            Usuario model = JsonConvert.DeserializeObject<Usuario>(str);            

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Login,Senha,Email")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage resposta = client.PostAsJsonAsync("api/usuarios", usuario).Result;
                string str = resposta.Content.ReadAsStringAsync().Result;



                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HttpResponseMessage resposta = client.GetAsync($"api/usuarios/{id}").Result;

            Usuario model = resposta.Content.ReadAsAsync<Usuario>().Result;

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Usuario/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Login,Senha,Email")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage resposta = client.PutAsJsonAsync($"api/usuarios/{usuario.Login}", usuario).Result;
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HttpResponseMessage resposta = client.GetAsync($"api/usuarios/{id}").Result;

            Usuario model = resposta.Content.ReadAsAsync<Usuario>().Result;

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HttpResponseMessage resposta = client.GetAsync($"api/usuarios/{id}").Result;
            Usuario model = resposta.Content.ReadAsAsync<Usuario>().Result;

            resposta = client.DeleteAsync($"api/usuarios/{id}").Result;

            return RedirectToAction("Index");
        }
    }
}
