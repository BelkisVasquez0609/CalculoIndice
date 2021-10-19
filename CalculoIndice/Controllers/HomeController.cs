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
    public class HomeController : Controller
    {
        private CalculoIndiceEntities4 db = new CalculoIndiceEntities4();
        public ActionResult Index()
        {//cmbiar estudiantesId por UsuarioId
            var calificacion = db.Calificacion.Include(c => c.Asignatura).Include(c => c.Estudiantes).Where(x => x.Estudiantes.EstudiantesId == 1);
            var Puntos = 0;
            var SumCreditos = 0;
            var Creditos = 0;
            var Mul = 0;
            float Indice = 0;

            foreach (var item in calificacion)
            {
                if (item.Calificación == "A" || item.Calificación == "a")
                {
                    Mul = 4;
                }
                else if (item.Calificación == "B" || item.Calificación == "B")
                {
                    Mul = 3;
                }
                else if (item.Calificación == "C" || item.Calificación == "c")
                {
                    Mul = 2;
                }
                else
                {
                    Mul = 0;

                }
                Creditos = (Convert.ToInt32(item.Asignatura.Credito));
                SumCreditos += Creditos;
                Puntos += (Creditos * Mul);
            }
            if (SumCreditos != 0)
            {
                Indice = (Puntos / SumCreditos);
            }
            else {
                Indice = 0;
            }
            ViewBag.Indice = Indice;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}