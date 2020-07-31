using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAO.Dao;
using Model.Models;

namespace WebSite.Controllers
{
    public class AlunoDisciplinaController : Controller
    {
        private EscolaWebContext db = new EscolaWebContext();

        // GET: AlunoDisciplina
        public ActionResult Index()
        {
            var alunoDisciplinas = db.AlunoDisciplinas.Include(a => a.Aluno).Include(a => a.Disciplina);
            return View(alunoDisciplinas.ToList());
        }

        // GET: AlunoDisciplina/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlunoDisciplina alunoDisciplina = db.AlunoDisciplinas.Find(id);
            if (alunoDisciplina == null)
            {
                return HttpNotFound();
            }
            return View(alunoDisciplina);
        }

        // GET: AlunoDisciplina/Create
        public ActionResult Create()
        {
            ViewBag.NrMatricula = new SelectList(db.Alunos, "NrMatricula", "Nome");
            ViewBag.DisciplinaId = new SelectList(db.Disciplinas, "Id", "Descricao");
            return View();
        }

        // POST: AlunoDisciplina/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DataMatricula,Status,DisciplinaId,NrMatricula")] AlunoDisciplina alunoDisciplina)
        {
            if (ModelState.IsValid)
            {
                db.AlunoDisciplinas.Add(alunoDisciplina);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NrMatricula = new SelectList(db.Alunos, "NrMatricula", "Nome", alunoDisciplina.NrMatricula);
            ViewBag.DisciplinaId = new SelectList(db.Disciplinas, "Id", "Descricao", alunoDisciplina.DisciplinaId);
            return View(alunoDisciplina);
        }

        // GET: AlunoDisciplina/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlunoDisciplina alunoDisciplina = db.AlunoDisciplinas.Find(id);
            if (alunoDisciplina == null)
            {
                return HttpNotFound();
            }
            ViewBag.NrMatricula = new SelectList(db.Alunos, "NrMatricula", "Nome", alunoDisciplina.NrMatricula);
            ViewBag.DisciplinaId = new SelectList(db.Disciplinas, "Id", "Descricao", alunoDisciplina.DisciplinaId);
            return View(alunoDisciplina);
        }

        // POST: AlunoDisciplina/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DataMatricula,Status,DisciplinaId,NrMatricula")] AlunoDisciplina alunoDisciplina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alunoDisciplina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NrMatricula = new SelectList(db.Alunos, "NrMatricula", "Nome", alunoDisciplina.NrMatricula);
            ViewBag.DisciplinaId = new SelectList(db.Disciplinas, "Id", "Descricao", alunoDisciplina.DisciplinaId);
            return View(alunoDisciplina);
        }

        // GET: AlunoDisciplina/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlunoDisciplina alunoDisciplina = db.AlunoDisciplinas.Find(id);
            if (alunoDisciplina == null)
            {
                return HttpNotFound();
            }
            return View(alunoDisciplina);
        }

        // POST: AlunoDisciplina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AlunoDisciplina alunoDisciplina = db.AlunoDisciplinas.Find(id);
            db.AlunoDisciplinas.Remove(alunoDisciplina);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
