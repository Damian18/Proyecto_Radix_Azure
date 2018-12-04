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
    public class EmpresasController : Controller
    {
        private radixEntities db = new radixEntities();
        

        // GET: empresas
        public ActionResult Index(string id)
        {
            var empresaslista = db.empresas.Include(e => e.subempresas);


            return View(empresaslista.ToList());
            //var empresas = db.empresas.Include(e => e.PlanesEmpresas);
            //return View(empresas.ToList());
        }

        // GET: empresas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empresas empresas = db.empresas.Find(id);
            if (empresas == null)
            {
                return HttpNotFound();
            }
            return View(empresas);
        }

        // GET: empresas/Create
        public ActionResult Create()
        {
            ViewBag.plan_id = new SelectList(db.PlanesEmpresas, "plan_id", "plan_nom");
            return View();
        }

        // POST: empresas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Emp_Id,Emp_Nom,Emp_Cant,Emp_Estado,Emp_Dir,plan_id,plan_estado,plan_FechaInicio,plan_FechaFin")] empresas empresas)
        {

            if (ModelState.IsValid)
            {
                db.empresas.Add(empresas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.plan_id = new SelectList(db.PlanesEmpresas, "plan_id", "plan_nom", empresas.plan_id);
            return View(empresas);
        }
        // GET: empresas2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empresas empresas = db.empresas.Find(id);
            if (empresas == null)
            {
                return HttpNotFound();
            }
            ViewBag.plan_id = new SelectList(db.PlanesEmpresas, "plan_id", "plan_nom", empresas.plan_id);
            return View(empresas);
        }

        // POST: empresas2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Emp_Id,Emp_Nom,Emp_Cant,Emp_Estado,Emp_Dir,plan_id,plan_estado,plan_FechaInicio,plan_FechaFin")] empresas empresas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empresas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.plan_id = new SelectList(db.PlanesEmpresas, "plan_id", "plan_nom", empresas.plan_id);
            return View(empresas);
        }

        // GET: empresas2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empresas empresas = db.empresas.Find(id);
            if (empresas == null)
            {
                return HttpNotFound();
            }
            return View(empresas);
        }

        // POST: empresas2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            empresas empresas = db.empresas.Find(id);
            db.empresas.Remove(empresas);
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
