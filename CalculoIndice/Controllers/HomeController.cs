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

        [AllowAnonymous]
        public ActionResult Index()
        {

            return View();
        }
       

        public ActionResult About(int EstudianteID )
        {
            var indice = (from s in db.Estudiantes
                          where s.EstudiantesId == EstudianteID
                          select s.Indice.Value).Single();

            ViewBag.Indice = 4.0;
            ViewBag.Message = "Su Indice es: "+ indice;

            return View();
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

                    return RedirectToAction("About", new { EstudianteID = userId});
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
            
            }

   
}