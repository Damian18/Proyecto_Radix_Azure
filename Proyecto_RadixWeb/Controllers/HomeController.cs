﻿using System;
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
                    if (Request.Cookies["Cookie"] != null)
                    {
                        ViewBag.idcookie = Request.Cookies["Cookie"].Value;
                        
                    }
                    else
                    {
                        ViewBag.idcookie = "Usuario no activo";
                        HttpCookie cook = new HttpCookie("Cookie");
                        cook.Value = "Usuario Activo";
                        cook.Expires = DateTime.Now.AddMonths(1);
                        Response.Cookies.Add(cook);
                    }
                    //esto es temporal hasta que se logre hacer funcional el dashboard de administrador
                    return View("DashBoardAdmin");
                    //return RedirectToAction("Index", "subempresas", new { emp_nom = empresa, emp_id = emp_id });

                }
                catch (Exception e)
                {

                    throw new Exception("Tiempo limite de sesion superada " + e.Message);
                }
               
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
