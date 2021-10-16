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
    public class CalificacionsController : Controller
    {
        private CalculoIndiceEntities db = new CalculoIndiceEntities();

        // GET: Calificacions
        public ActionResult Index()
        {
            var calificacion = db.Calificacion.Include(c => c.Asignatura).Include(c => c.Estudiantes);
            return View(calificacion.ToList());
        }

        // GET: Calificacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calificacion calificacion = db.Calificacion.Find(id);
            if (calificacion == null)
            {
                return HttpNotFound();
            }
            return View(calificacion);
        }

        // GET: Calificacions/Create
        public ActionResult Create()
        {
            ViewBag.AsignaturaId = new SelectList(db.Asignatura, "AsignaturaId", "Clave");
            ViewBag.EstudiantesId = new SelectList(db.Estudiantes, "EstudiantesId", "Nombre");
            return View();
        }

        // POST: Calificacions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CalificacionId,EstudiantesId,AsignaturaId,Calificación,Promedio")] Calificacion calificacion)
        {
            if (ModelState.IsValid)
            {
                db.Calificacion.Add(calificacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AsignaturaId = new SelectList(db.Asignatura, "AsignaturaId", "Clave", calificacion.AsignaturaId);
            ViewBag.EstudiantesId = new SelectList(db.Estudiantes, "EstudiantesId", "Nombre", calificacion.EstudiantesId);
            return View(calificacion);
        }

        // GET: Calificacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calificacion calificacion = db.Calificacion.Find(id);
            if (calificacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.AsignaturaId = new SelectList(db.Asignatura, "AsignaturaId", "Clave", calificacion.AsignaturaId);
            ViewBag.EstudiantesId = new SelectList(db.Estudiantes, "EstudiantesId", "Nombre", calificacion.EstudiantesId);
            return View(calificacion);
        }

        // POST: Calificacions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CalificacionId,EstudiantesId,AsignaturaId,Calificación,Promedio")] Calificacion calificacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(calificacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AsignaturaId = new SelectList(db.Asignatura, "AsignaturaId", "Clave", calificacion.AsignaturaId);
            ViewBag.EstudiantesId = new SelectList(db.Estudiantes, "EstudiantesId", "Nombre", calificacion.EstudiantesId);
            return View(calificacion);
        }

        // GET: Calificacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calificacion calificacion = db.Calificacion.Find(id);
            if (calificacion == null)
            {
                return HttpNotFound();
            }
            return View(calificacion);
        }

        // POST: Calificacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Calificacion calificacion = db.Calificacion.Find(id);
            db.Calificacion.Remove(calificacion);
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
