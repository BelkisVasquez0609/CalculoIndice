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
using System.Web.Security;
namespace CalculoIndice.Controllers
{
    
    public class HomeController : Controller
    {
        private CalculoIndiceEntities4 db = new CalculoIndiceEntities4();
        private List<Models.Calificacion> calificacions;
        private List<Models.Asignatura> asignatura;
        private PaginadorGenerico<Models.Calificacion> _PaginadorCalificacion;
        private PaginadorGenerico<Models.Asignatura> _PaginadorAsignatura;
        private readonly int _RegistrosPorPagina = 10;

        [AllowAnonymous]
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult Dashboards()
        {
            ViewBag.IdRolUsuario = IdRolUsuario;
            return PartialView();
        }
        public ActionResult UserPerfil()
        {
            ViewBag.Usuario = CurrentUser;
            return PartialView();
        }
        public ActionResult Menu()
        {
            ViewBag.Usuario = CurrentUser;
            ViewBag.IdRolUsuario = IdRolUsuario;

            return PartialView();
        }
        public ActionResult CambioContraseña()
        {
            return View();
        }
        public ActionResult SolicitarRegistro()
        {
            return View();
        }
        [CustomAuthorize(1)]
        public ActionResult DAdmin(string buscar, int pagina = 1)
        {
            int _TotalRegistros = 0;
            int _TotalPaginas = 0;

            //Cargar La Data

            asignatura = db.Asignatura.Include(a => a.Asignatura2).Include(a => a.Asignatura3).Include(a => a.Profesores).ToList();

            // Filtro de Informacion
            if (!string.IsNullOrEmpty(buscar))
            {
                foreach (var item in buscar.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
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
        [CustomAuthorize(2)]
        public ActionResult DProfesor(string buscar, int pagina = 1)
        {

            int _TotalRegistros = 0;
            int _TotalPaginas = 0;
            //Cargar La Data

            asignatura = db.Asignatura.Include(a => a.Asignatura2).Include(a => a.Asignatura3).Include(a => a.Profesores).Where(a => a.ProfesoresId == HomeController.IdProfile).ToList();

            // Filtro de Informacion
            if (!string.IsNullOrEmpty(buscar))
            {
                foreach (var item in buscar.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    asignatura = asignatura.Where(a => a.ProfesoresId == 1 && a.Nombre.Contains(item) || a.Clave.Contains(item)).ToList();
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
            return View();
        }
        [CustomAuthorize(3)]
        public ActionResult About(int EstudianteID , string buscar, int pagina = 1)
        {
            int _TotalRegistros = 0;
            int _TotalPaginas = 0;

            //Cargar La Data
            calificacions = db.Calificacion.Include(c => c.Asignatura).Include(c => c.Estudiantes).Where(x => x.Estudiantes.EstudiantesId == HomeController.IdProfile).ToList();

            // Filtro de Informacion
            if (!string.IsNullOrEmpty(buscar))
            {
                foreach (var item in buscar.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    calificacions = db.Calificacion.Include(c => c.Asignatura).Include(c => c.Estudiantes).Where(x => x.Estudiantes.EstudiantesId == HomeController.IdProfile).ToList();
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
            _PaginadorCalificacion = new PaginadorGenerico<Models.Calificacion>()
            {
                RegistrosPorPagina = _RegistrosPorPagina,
                TotalRegistros = _TotalRegistros,
                TotalPaginas = _TotalPaginas,
                PaginaActual = pagina,
                Resultado = calificacions
            };

            var indice = (from s in db.Estudiantes
                          where s.EstudiantesId == EstudianteID
                          select s.Indice.Value).Single();

            ViewBag.Indice = 4.0;
            ViewBag.Message =  indice;
            ViewBag.Date = DateTime.Now.ToShortDateString();

            // Enviamos a la Vista la 'Clase de paginación'
            return View(_PaginadorAsignatura);
        }

     
          public ActionResult Contact()
        {
            ViewBag.Message = "Your Error page.";

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(DTO.Usuarios user)
        {
            CalculoIndiceEntities4 usersEntities = new CalculoIndiceEntities4();
            int? userId = usersEntities.Validate_User(user.Username, user.Password).FirstOrDefault();

            string message = string.Empty;
            switch (userId.Value)
            {
                case -1:
                    message = "Username and/or password is incorrect.";
                    break;
                case -2:
                    message = "Account has not been activated.";
                    break;
                default:
                    FormsAuthentication.SetAuthCookie(user.Username, true);

                    System.Web.HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(
            new System.Security.Principal.GenericIdentity(user.Username),
            new string[] { /* fill roles if any */ });

                    CurrentUser = System.Web.HttpContext.Current.User.Identity.Name;

                    IdProfile = (from s in db.Usuario
                                where s.Username == CurrentUser
                                 select s.PerfilId.Value).Single();

                    IdRolUsuario = (from s in db.Usuario
                                   where s.Username == CurrentUser
                                    select s.RolId.Value).Single();
                    return RedirectToAction("Dashboards", new { EstudianteID = userId});
            }
            
            ViewBag.Message = message;
            return View("Index");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        public static string CurrentUser = "";
        public static int IdProfile = 0;
        public static int IdRolUsuario = 0;

    }

   
}