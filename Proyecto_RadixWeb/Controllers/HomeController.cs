using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto_RadixWeb.Models;

namespace IdentitySample.Controllers
{
    public class HomeController : Controller
    {
        private radixEntities db = new radixEntities();

        public ActionResult Estadisticas_cantidad()
        {
                    return View("Estadisticas_cantidad");          
   }
        public ActionResult Estadisticas_genero()
        {
            return View("Estadisticas_genero");
        }
        public ActionResult Estadisticas_forma()
        {
            return View("Estadisticas_forma");
        }



        public ActionResult Index()
        {

          if (User.IsInRole("Radix"))
            {
                string emp_nom = HttpContext.Session["Empresa"].ToString();
                ViewBag.empresa = emp_nom;

                return View("DashBoardRadix");

            }
            else if (User.IsInRole("Administrador"))
            {
                try
                {
                    string emp_nom = HttpContext.Session["Empresa"].ToString();
                    ViewBag.empresa = emp_nom;
                    string id2 = HttpContext.Session["Emp_id"].ToString();
                    int id = Convert.ToInt32(id2);
               
                    int contar = db.subempresas.Count(s => s.Emp_Id == id);
                    ViewBag.contar = contar;
                    ViewBag.ressub = ViewBag.contar + 1;
                    int contar2 = db.contratos.Count(s => s.subempresas.empresas.Emp_Id == id);
                    ViewBag.contarcontratos = contar2;
                    int contar3 = db.empresa_cargo.Count(s => s.empresas.Emp_Id == id);
                    ViewBag.contarcargos = contar3;
                    int contar4 = db.Sectores.Count(s => s.subempresas.empresas.Emp_Id == id);
                    ViewBag.contarsectores = contar4;
               
                    int contar5 = db.Cuarteles.Count(s => s.Sectores.subempresas.empresas.Emp_Id == id);
                    ViewBag.contarcuad = contar5;
                    //var lista_empresa = db.subempresas.ToList();
                    //foreach (var item in lista_empresa)
                    //{
                    //    int conta2r = db.subempresas.Count(s => s.Emp_Id == id);

                    //}

                    string prob = "1";
                    ViewBag.prob = prob;
                    if (Request.Cookies["Cookie"] != null)
                    {
                        ViewBag.idcookie = Request.Cookies["Cookie"].Value;
                    }
                    else
                    {
                        ViewBag.idcookie = "Usuario no activo";
             
                        HttpCookie cook = new HttpCookie("Cookie");
             
                        cook.Value = "Usuario Activo";
                  
                        cook.Expires = DateTime.Now.AddSeconds(1);
                
                        Response.Cookies.Add(cook);
             
                    }
                    //esto es temporal hasta que se logre hacer funcional el dashboard de administrador
                    return View("DashBoardAdmin");
                    //return RedirectToAction("Index", "subempresas", new { emp_nom = empresa, emp_id = emp_id });

                }
                catch (Exception e)
                {
                   
                    ViewBag.prob = "2";
                    return (ViewBag.prob);
                }

            }else if (User.IsInRole("Secretaria"))
            {
                return View("DashBoardSucursal");
            }


                return View();
        }

        public JsonResult ListarPlanes()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<PlanesEmpresas> listaplanes = db.PlanesEmpresas.ToList();

            return Json(listaplanes, JsonRequestBehavior.AllowGet);

        }



        public JsonResult ObtenerSubEmpresa()
        {
            int emp_id = Convert.ToInt32(HttpContext.Session["Emp_id"].ToString());

            //db.Configuration.ProxyCreationEnabled = false;
            //List<subempresas> Listasubempresas = db.subempresas.Where(p => p.Emp_Id == emp_id).ToList();

            var lista = db.subempresas.Select(x => new
            {
                Id = x.Emp_Id,
                SubEmp= x.Sub_Id,
                Nombre = x.Sub_Nom

            }).Where(s=>s.Id==emp_id).ToList();

            return Json(lista, JsonRequestBehavior.AllowGet);

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
