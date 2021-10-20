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
    public class AsignaturasController : Controller
    {
        private CalculoIndiceEntities4 db = new CalculoIndiceEntities4();
        private List<Models.Asignatura> asignatura;
        private PaginadorGenerico<Models.Asignatura> _PaginadorAsignatura;
        private readonly int _RegistrosPorPagina = 10;
        // GET: Asignaturas
        [CustomAuthorize(1)]
        public ActionResult Index()
        {
            var asignatura = db.Asignatura.Include(a => a.Asignatura2).Include(a => a.Asignatura3).Include(a => a.Profesores);
            return View(asignatura.ToList());
        }
        // GET: Asignaturas General
        [CustomAuthorize(1)]
        public ActionResult ReporteAsignaturas(string buscar, int pagina = 1)
        {
            int _TotalRegistros = 0;
            int _TotalPaginas = 0;

            //Cargar La Data

             asignatura = db.Asignatura.Include(a => a.Asignatura2).Include(a => a.Asignatura3).Include(a => a.Profesores).ToList();

            // Filtro de Informacion
            if (!string.IsNullOrEmpty(buscar))
            {
                foreach (var item in buscar.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries))
                {
                    asignatura = asignatura.Where(x => x.Nombre.Contains(item) || x.Clave.Contains(item)).ToList();
                }

            }
            // SISTEMA DE PAGINACIÓN
          
                // Número total de registros de la tabla Asignatura
                _TotalRegistros = asignatura.Count();
                // Obtenemos la 'página de registros' de la tabla Asignatura
                asignatura = asignatura.OrderBy(x => x.AsignaturaId).ToList();
                // Número total de páginas de la tabla Asignatura
                _TotalPaginas = (int)Math.Ceiling((double)_TotalRegistros / _RegistrosPorPagina);

            // Instanciamos la 'Clase de paginación' y asignamos los nuevos valores
            _PaginadorAsignatura = new PaginadorGenerico<Models.Asignatura>()
                {
                    RegistrosPorPagina = _RegistrosPorPagina,
                    TotalRegistros = _TotalRegistros,
                    TotalPaginas = _TotalPaginas,
                    PaginaActual = pagina,
                    BusquedaActual = buscar,
                    Resultado = asignatura
                };
            

            // Enviamos a la Vista la 'Clase de paginación'
            return View(_PaginadorAsignatura);
           
        }
        // GET: Reporte de asignaturas del profesor
        [CustomAuthorize(2)]
        public ActionResult ReporteAsignaturasProfesor(string buscar, int pagina = 1)
        {
            int _TotalRegistros = 0;
            int _TotalPaginas = 0;
            //Cargar La Data

            asignatura = db.Asignatura.Include(a => a.Asignatura2).Include(a => a.Asignatura3).Include(a => a.Profesores).Where(a => a.ProfesoresId == 1).ToList();

            // Filtro de Informacion
            if (!string.IsNullOrEmpty(buscar))
            {
                foreach (var item in buscar.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    asignatura = asignatura.Where( a=>a.ProfesoresId==1 &&  a.Nombre.Contains(item) || a.Clave.Contains(item)).ToList();       
                }

            }
            // SISTEMA DE PAGINACIÓN

            // Número total de registros de la tabla Asignatura
            _TotalRegistros = asignatura.Count();
            // Obtenemos la 'página de registros' de la tabla Asignatura
            asignatura = asignatura.OrderBy(x => x.AsignaturaId).ToList();
            // Número total de páginas de la tabla Asignatura
            _TotalPaginas = (int)Math.Ceiling((double)_TotalRegistros / _RegistrosPorPagina);

            // Instanciamos la 'Clase de paginación' y asignamos los nuevos valores
            _PaginadorAsignatura = new PaginadorGenerico<Models.Asignatura>()
            {
                RegistrosPorPagina = _RegistrosPorPagina,
                TotalRegistros = _TotalRegistros,
                TotalPaginas = _TotalPaginas,
                PaginaActual = pagina,
                BusquedaActual = buscar,
                Resultado = asignatura
            };


            // Enviamos a la Vista la 'Clase de paginación'
            return View(_PaginadorAsignatura);

        }
        [CustomAuthorize(1)]
        // GET: Asignaturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asignatura asignatura = db.Asignatura.Find(id);
            if (asignatura == null)
            {
                return HttpNotFound();
            }
            return View(asignatura);
        }
        [CustomAuthorize(1)]
        // GET: Asignaturas/Create
        public ActionResult Create()
        {
            ViewBag.CoRequisitos = new SelectList(db.Asignatura, "AsignaturaId", "Clave");
            ViewBag.PreRequisitos = new SelectList(db.Asignatura, "AsignaturaId", "Clave");
            ViewBag.ProfesoresId = new SelectList(db.Profesores, "ProfesoresId", "Nombre");
            return View();
        }
        [CustomAuthorize(1)]
        // POST: Asignaturas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AsignaturaId,Clave,Nombre,Credito,PreRequisitos,CoRequisitos,ProfesoresId")] Asignatura asignatura)
        {
            if (ModelState.IsValid)
            {
                db.Asignatura.Add(asignatura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CoRequisitos = new SelectList(db.Asignatura, "AsignaturaId", "Clave", asignatura.CoRequisitos);
            ViewBag.PreRequisitos = new SelectList(db.Asignatura, "AsignaturaId", "Clave", asignatura.PreRequisitos);
            ViewBag.ProfesoresId = new SelectList(db.Profesores, "ProfesoresId", "Nombre", asignatura.ProfesoresId);
            return View(asignatura);
        }
        [CustomAuthorize(1)]
        // GET: Asignaturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asignatura asignatura = db.Asignatura.Find(id);
            if (asignatura == null)
            {
                return HttpNotFound();
            }
            ViewBag.CoRequisitos = new SelectList(db.Asignatura, "AsignaturaId", "Clave", asignatura.CoRequisitos);
            ViewBag.PreRequisitos = new SelectList(db.Asignatura, "AsignaturaId", "Clave", asignatura.PreRequisitos);
            ViewBag.ProfesoresId = new SelectList(db.Profesores, "ProfesoresId", "Nombre", asignatura.ProfesoresId);
            return View(asignatura);
        }
        [CustomAuthorize(1)]
        // POST: Asignaturas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AsignaturaId,Clave,Nombre,Credito,PreRequisitos,CoRequisitos,ProfesoresId")] Asignatura asignatura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asignatura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CoRequisitos = new SelectList(db.Asignatura, "AsignaturaId", "Clave", asignatura.CoRequisitos);
            ViewBag.PreRequisitos = new SelectList(db.Asignatura, "AsignaturaId", "Clave", asignatura.PreRequisitos);
            ViewBag.ProfesoresId = new SelectList(db.Profesores, "ProfesoresId", "Nombre", asignatura.ProfesoresId);
            return View(asignatura);
        }
        [CustomAuthorize(1)]
        // GET: Asignaturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asignatura asignatura = db.Asignatura.Find(id);
            if (asignatura == null)
            {
                return HttpNotFound();
            }
            return View(asignatura);
        }
        [CustomAuthorize(1)]
        // POST: Asignaturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asignatura asignatura = db.Asignatura.Find(id);
            db.Asignatura.Remove(asignatura);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [CustomAuthorize(1)]
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
