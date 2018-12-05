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
    public class PrivilegiosCuentasController : Controller
    {
        private radixEntities db = new radixEntities();
     
            // GET: PrivilegiosCuentas
            public ActionResult Index()
        {

            ViewBag.empresa = HttpContext.Session["Empresa"].ToString();
            ViewBag.emp_id = HttpContext.Session["Emp_id"].ToString();
            var privilegios = db.PrivilegiosCuentas;
            MultiplesClases multiples = new MultiplesClases
            {
                ObjEPrivilegiosCuentas = privilegios.ToList()
            };

            return View(multiples);
        }
     
        // GET: PrivilegiosCuentas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrivilegiosCuentas privilegiosCuentas = db.PrivilegiosCuentas.Find(id);
            if (privilegiosCuentas == null)
            {
                return HttpNotFound();
            }
            return View(privilegiosCuentas);
        }

        // GET: PrivilegiosCuentas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrivilegiosCuentas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MultiplesClases multiples)
        {
            if (ModelState.IsValid)
            {
              
                db.PrivilegiosCuentas.Add(multiples.ObjPrivilegiosCuentas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(multiples);
        }

        // GET: PrivilegiosCuentas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrivilegiosCuentas privilegiosCuentas = db.PrivilegiosCuentas.Find(id);
            if (privilegiosCuentas == null)
            {
                return HttpNotFound();
            }
            return View(privilegiosCuentas);
        }

        // POST: PrivilegiosCuentas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pc_id,pc_nom")] PrivilegiosCuentas privilegiosCuentas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(privilegiosCuentas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(privilegiosCuentas);
        }

        // GET: PrivilegiosCuentas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrivilegiosCuentas priv = db.PrivilegiosCuentas.Find(id);
            if (priv == null)
            {
                return HttpNotFound();
            }
            return View(priv);
        }

        // POST: PrivilegiosCuentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PrivilegiosCuentas priv = db.PrivilegiosCuentas.Find(id);
            db.PrivilegiosCuentas.Remove(priv);
            db.SaveChanges();
                return RedirectToAction("Index");
        
            //db.PrivilegiosCuentas.Remove(multiples.ObjPrivilegiosCuentas);
        }
        public ActionResult Eliminar(PrivilegiosCuentas pc)
        {
            int? id2 = pc.pc_id;
            int id = Convert.ToInt32(id2);
            PrivilegiosCuentas privilegios = db.PrivilegiosCuentas.Find(id);
            db.PrivilegiosCuentas.Remove(privilegios);
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
