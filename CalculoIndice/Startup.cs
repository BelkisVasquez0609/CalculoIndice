using CalculoIndice.Controllers;
using CalculoIndice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Owin;
using Owin;
using System.Linq;
using System.Web;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(CalculoIndice.Startup))]
namespace CalculoIndice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }

    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        CalculoIndiceEntities4 context = new CalculoIndiceEntities4(); // my entity  
        private readonly int[] allowedroles;

        public CustomAuthorizeAttribute(params int[] roles)
        {
            this.allowedroles = roles;

        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            foreach (var role in allowedroles)
            {
                var user = context.Usuario.Where(m => m.Username == HomeController.CurrentUser/* getting user form current context */ && m.RolId == role); // checking active users with allowed roles.  
                if (user.Count() > 0)
                {
                    authorize = true; /* return true if Entity has current user(active) with specific role */
                }
            }
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}
