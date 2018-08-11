using Proyecto_RadixWeb.Models;
using System.Web.Mvc;

namespace IdentitySample.Controllers
{
    public class HomeController : Controller
    {
        private radixEntities db = new radixEntities();

        

        public ActionResult Index()
        {
            

            if (User.IsInRole("Radix"))
            {
                ViewBag.empresa = HttpContext.Session["Empresa"].ToString();

                return View("DashBoardRadix");

            }
            else if (User.IsInRole("Administrador"))
            {
                ViewBag.empresa = HttpContext.Session["Empresa"].ToString();
                //esto es temporal hasta que se logre hacer funcional el dashboard de administrador
                return View("DashBoardAdmin");
                //return RedirectToAction("Index", "subempresas", new { emp_nom = empresa, emp_id = emp_id });

            }


            return View();
        }

        

        [Authorize]
        public ActionResult About()
        {
           ViewBag.empresa = HttpContext.Session["Empresa"].ToString();

            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.empresa = HttpContext.Session["Empresa"].ToString();
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
