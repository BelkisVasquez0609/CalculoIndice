using System;
using System.Collections.Generic;
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
    public class Estudiantes1Controller : Controller
    {
        private CalculoIndiceEntities4 db = new CalculoIndiceEntities4();
        private List<Models.Estudiantes> estudiantes;
        private PaginadorGenerico<Models.Estudiantes> _PaginadorAsignatura;
        private readonly int _RegistrosPorPagina = 10;

        // GET: Estudiantes1
       
        public ActionResult Index()
        {
            var estudiantes = db.Estudiantes.Include(e => e.Carrera);
            return View(estudiantes.ToList());
        }
        // GET: Estudiantes por Ranking
        [CustomAuthorize(1)]
        public ActionResult EstudiantesRanking(string buscar, int pagina = 1)
        {
            int _TotalRegistros = 0;
            int _TotalPaginas = 0;

            //Cargar La Data

            estudiantes = db.Estudiantes.Include(e => e.Carrera).ToList();

            // Filtro de Informacion
            if (!string.IsNullOrEmpty(buscar))
            {
                foreach (var item in buscar.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    estudiantes = db.Estudiantes.Include(e => e.Carrera).ToList();
                }

            }
            // SISTEMA DE PAGINACIÓN

            // Número total de registros de la tabla Asignatura
            _TotalRegistros = estudiantes.Count();
            // Obtenemos la 'página de registros' de la tabla Asignatura
            estudiantes = estudiantes.OrderByDescending(x => x.Indice).ToList();
            // Número total de páginas de la tabla Asignatura
            _TotalPaginas = (int)Math.Ceiling((double)_TotalRegistros / _RegistrosPorPagina);

            // Instanciamos la 'Clase de paginación' y asignamos los nuevos valores
            _PaginadorAsignatura = new PaginadorGenerico<Models.Estudiantes>()
            {
                RegistrosPorPagina = _RegistrosPorPagina,
                TotalRegistros = _TotalRegistros,
                TotalPaginas = _TotalPaginas,
                PaginaActual = pagina,
                BusquedaActual = buscar,
                Resultado = estudiantes
            };


            // Enviamos a la Vista la 'Clase de paginación'
            return View(_PaginadorAsignatura);

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
