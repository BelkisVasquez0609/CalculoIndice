using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CalculoIndice.Models;

namespace CalculoIndice.Controllers
{
    public class ProfesoresController : Controller
    {
        private CalculoIndiceEntities4 db = new CalculoIndiceEntities4();

        // GET: Profesores
        public ActionResult Index()
        {
            return View(db.Profesores.ToList());
        }

        // GET: Profesores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesores profesores = db.Profesores.Find(id);
            if (profesores == null)
            {
                return HttpNotFound();
            }
            return View(profesores);
        }

        // GET: Profesores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profesores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProfesoresId,Nombre,CorreoElectronico,DNI,Direccion,Telefono")] Profesores profesores)
        {
            if (ModelState.IsValid)
            {
                db.Profesores.Add(profesores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(profesores);
        }

        // GET: Profesores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesores profesores = db.Profesores.Find(id);
            if (profesores == null)
            {
                return HttpNotFound();
            }
            return View(profesores);
        }

        // POST: Profesores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProfesoresId,Nombre,CorreoElectronico,DNI,Direccion,Telefono")] Profesores profesores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profesores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profesores);
        }

        // GET: Profesores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesores profesores = db.Profesores.Find(id);
            if (profesores == null)
            {
                return HttpNotFound();
            }
            return View(profesores);
        }

        // POST: Profesores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Profesores profesores = db.Profesores.Find(id);
            db.Profesores.Remove(profesores);
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
