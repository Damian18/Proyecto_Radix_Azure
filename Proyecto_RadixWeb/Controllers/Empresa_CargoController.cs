using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto_RadixWeb.Models;

namespace Proyecto_RadixWeb.Controllers
{
    public class Empresa_CargoController : Controller
    {
        private radixEntities db = new radixEntities();

        // GET: Empresa_Cargo
        public ActionResult Index()
        {

            string emp_nom = HttpContext.Session["Empresa"].ToString();

            int emp_id =Convert.ToInt32( HttpContext.Session["Emp_id"].ToString());
            ViewBag.empresa = emp_nom;
            var empresa_cargo = db.empresa_cargo.Include(e => e.cargos).Include(e => e.empresas);
            return View(empresa_cargo.Where(e=>e.Emp_Id==emp_id).ToList());
        }

        // GET: Empresa_Cargo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empresa_cargo empresa_cargo = db.empresa_cargo.Find(id);
            if (empresa_cargo == null)
            {
                return HttpNotFound();
            }
            return View(empresa_cargo);
        }

        // GET: Empresa_Cargo/Create
        public ActionResult Create()
        {

            string emp_nom = HttpContext.Session["Empresa"].ToString();

            int emp_id = Convert.ToInt32(HttpContext.Session["Emp_id"].ToString());
            ViewBag.empresa = emp_nom;
            ViewBag.Car_Id = new SelectList(db.cargos, "Car_Id", "Car_Nom");
        
            return View();
        }

        // POST: Empresa_Cargo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Empcar_id,Car_Id")] empresa_cargo empresa_cargo)
        {
            if (ModelState.IsValid)
            {
                string emp_nom = HttpContext.Session["Empresa"].ToString();

                int emp_id = Convert.ToInt32(HttpContext.Session["Emp_id"].ToString());
                empresa_cargo.Emp_Id = emp_id;
                db.empresa_cargo.Add(empresa_cargo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Car_Id = new SelectList(db.cargos, "Car_Id", "Car_Nom", empresa_cargo.Car_Id);
            ViewBag.Emp_Id = new SelectList(db.empresas, "Emp_Id", "Emp_Nom", empresa_cargo.Emp_Id);
            return View(empresa_cargo);
        }

        // GET: Empresa_Cargo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empresa_cargo empresa_cargo = db.empresa_cargo.Find(id);
            if (empresa_cargo == null)
            {
                return HttpNotFound();
            }
            ViewBag.Car_Id = new SelectList(db.cargos, "Car_Id", "Car_Nom", empresa_cargo.Car_Id);
            ViewBag.Emp_Id = new SelectList(db.empresas, "Emp_Id", "Emp_Nom", empresa_cargo.Emp_Id);
            return View(empresa_cargo);
        }

        // POST: Empresa_Cargo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Empcar_id,Emp_Id,Car_Id")] empresa_cargo empresa_cargo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empresa_cargo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Car_Id = new SelectList(db.cargos, "Car_Id", "Car_Nom", empresa_cargo.Car_Id);
            ViewBag.Emp_Id = new SelectList(db.empresas, "Emp_Id", "Emp_Nom", empresa_cargo.Emp_Id);
            return View(empresa_cargo);
        }

        // GET: Empresa_Cargo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empresa_cargo empresa_cargo = db.empresa_cargo.Find(id);
            if (empresa_cargo == null)
            {
                return HttpNotFound();
            }
            return View(empresa_cargo);
        }

        // POST: Empresa_Cargo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            empresa_cargo empresa_cargo = db.empresa_cargo.Find(id);
            db.empresa_cargo.Remove(empresa_cargo);
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
