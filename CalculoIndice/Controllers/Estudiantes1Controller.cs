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
    public class Estudiantes1Controller : Controller
    {
        private CalculoIndiceEntities3 db = new CalculoIndiceEntities3();

        // GET: Estudiantes1
        public ActionResult Index()
        {
            var estudiantes = db.Estudiantes.Include(e => e.Carrera);
            return View(estudiantes.ToList());
        }

        // GET: Estudiantes1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiantes estudiantes = db.Estudiantes.Find(id);
            if (estudiantes == null)
            {
                return HttpNotFound();
            }
            return View(estudiantes);
        }

        // GET: Estudiantes1/Create
        public ActionResult Create()
        {
            ViewBag.CarreraId = new SelectList(db.Carrera, "CarreraId", "Descripcion");
            return View();
        }

        // POST: Estudiantes1/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EstudiantesId,Matricula,CarreraId,Nombre,CorreoElectronico,DNI,Direccion,Telefono,Indice")] Estudiantes estudiantes)
        {
            if (ModelState.IsValid)
            {
                db.Estudiantes.Add(estudiantes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarreraId = new SelectList(db.Carrera, "CarreraId", "Descripcion", estudiantes.CarreraId);
            return View(estudiantes);
        }

        // GET: Estudiantes1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiantes estudiantes = db.Estudiantes.Find(id);
            if (estudiantes == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarreraId = new SelectList(db.Carrera, "CarreraId", "Descripcion", estudiantes.CarreraId);
            return View(estudiantes);
        }

        // POST: Estudiantes1/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EstudiantesId,Matricula,CarreraId,Nombre,CorreoElectronico,DNI,Direccion,Telefono,Indice")] Estudiantes estudiantes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estudiantes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarreraId = new SelectList(db.Carrera, "CarreraId", "Descripcion", estudiantes.CarreraId);
            return View(estudiantes);
        }

        // GET: Estudiantes1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiantes estudiantes = db.Estudiantes.Find(id);
            if (estudiantes == null)
            {
                return HttpNotFound();
            }
            return View(estudiantes);
        }

        // POST: Estudiantes1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estudiantes estudiantes = db.Estudiantes.Find(id);
            db.Estudiantes.Remove(estudiantes);
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
