using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CalculoIndice.DTO;
using CalculoIndice.Models;

namespace CalculoIndice.Controllers
{
    public class CalificacionsController : Controller
    {
        private CalculoIndiceEntities db = new CalculoIndiceEntities();
        private List<Models.Calificacion> calificacions;
        private List<Models.Estudiantes> estudiantes;
        private PaginadorGenerico<Models.Calificacion> _PaginadorAsignatura;
        private readonly int _RegistrosPorPagina = 10;
        // GET: Calificacions
        public ActionResult Index()
        {
            var calificacion = db.Calificacion.Include(c => c.Asignatura).Include(c => c.Estudiantes);
            return View(calificacion.ToList());
        }
        public ActionResult ReporteEstudiantesAsignatura(string buscar, int pagina = 1)
        {
            int _TotalRegistros = 0;
            int _TotalPaginas = 0;

            //Cargar La Data
            calificacions = db.Calificacion.Include(c => c.Asignatura).Include(c => c.Estudiantes).ToList();
            // Filtro de Informacion
            if (!string.IsNullOrEmpty(buscar))
            {
                foreach (var item in buscar.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    calificacions = calificacions.Where(x=>x.Asignatura.ProfesoresId==1 && x.Asignatura.Clave.Contains(item)|| x.Estudiantes.Nombre.Contains(item)).ToList();
                }

            }
            // SISTEMA DE PAGINACIÓN

            // Número total de registros de la tabla Asignatura
            _TotalRegistros = calificacions.Count();
            // Obtenemos la 'página de registros' de la tabla Asignatura
            calificacions = calificacions.OrderBy(x => x.AsignaturaId).ToList();
            // Número total de páginas de la tabla Asignatura
            _TotalPaginas = (int)Math.Ceiling((double)_TotalRegistros / _RegistrosPorPagina);

            // Instanciamos la 'Clase de paginación' y asignamos los nuevos valores
            _PaginadorAsignatura = new PaginadorGenerico<Models.Calificacion>()
            {
                RegistrosPorPagina = _RegistrosPorPagina,
                TotalRegistros = _TotalRegistros,
                TotalPaginas = _TotalPaginas,
                PaginaActual = pagina,
                BusquedaActual = buscar,
                Resultado = calificacions
            };


            // Enviamos a la Vista la 'Clase de paginación'
            return View(_PaginadorAsignatura);

        }
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
