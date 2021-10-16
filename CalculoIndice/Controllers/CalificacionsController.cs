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
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Calificacions
        public ActionResult Index()
        {
            var calificacions = db.Calificacions.Include(c => c.Asignatura).Include(c => c.Estudiantes);
            return View(calificacions.ToList());
        }

        // GET: Calificacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calificacion calificacion = db.Calificacions.Find(id);
            if (calificacion == null)
            {
                return HttpNotFound();
            }
            return View(calificacion);
        }

        // GET: Calificacions/Create
        public ActionResult Create()
        {
        //    ViewBag.AsignaturaId = new SelectList(db.Asignaturas, "AsignaturaId", "Clave");
            ViewBag.EstudiantesId = new SelectList(db.Estudiantes, "EstudiantesId", "Nombre");
            return View();
        }

        // POST: Calificacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CalificacionId,EstudiantesId,AsignaturaId,Calificación,Promedio")] Calificacion calificacion)
        {
            if (ModelState.IsValid)
            {
                db.Calificacions.Add(calificacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

       //     ViewBag.AsignaturaId = new SelectList(db.Asignaturas, "AsignaturaId", "Clave", calificacion.AsignaturaId);
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
            Calificacion calificacion = db.Calificacions.Find(id);
            if (calificacion == null)
            {
                return HttpNotFound();
            }
          //  ViewBag.AsignaturaId = new SelectList(db.Asignaturas, "AsignaturaId", "Clave", calificacion.AsignaturaId);
            ViewBag.EstudiantesId = new SelectList(db.Estudiantes, "EstudiantesId", "Nombre", calificacion.EstudiantesId);
            return View(calificacion);
        }

        // POST: Calificacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
       //     ViewBag.AsignaturaId = new SelectList(db.Asignaturas, "AsignaturaId", "Clave", calificacion.AsignaturaId);
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
            Calificacion calificacion = db.Calificacions.Find(id);
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
            Calificacion calificacion = db.Calificacions.Find(id);
            db.Calificacions.Remove(calificacion);
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
