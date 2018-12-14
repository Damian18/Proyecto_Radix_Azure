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
    public class GrupoCuartelesDetallesController : Controller
    {
        private radixEntities db = new radixEntities();

        // GET: GrupoCuartelesDetalles
        public ActionResult Index(string gc_id,string subemp_id)
        {
            ViewBag.subemp_id = subemp_id;
            ViewBag.gc_id = gc_id;

            int grupoCuartel = Convert.ToInt32(gc_id);
           

            var grupoCuartelesDetalle = db.GrupoCuartelesDetalle.Include(g => g.contratos).Include(g => g.GruposCuarteles);
            MultiplesClases multiples = new MultiplesClases {
                ObjEGruposCuartelesDetalle=grupoCuartelesDetalle.Where(g=>g.gc_id==grupoCuartel)
                
            };

            return View(multiples);
        }

        // GET: GrupoCuartelesDetalles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrupoCuartelesDetalle grupoCuartelesDetalle = db.GrupoCuartelesDetalle.Find(id);
            if (grupoCuartelesDetalle == null)
            {
                return HttpNotFound();
            }
            return View(grupoCuartelesDetalle);
        }

        // GET: GrupoCuartelesDetalles/Create
        public ActionResult Create()
        {
            ViewBag.Con_id = new SelectList(db.contratos, "Con_Id", "Per_Rut");
            ViewBag.gc_id = new SelectList(db.GruposCuarteles, "gc_id", "gc_nom");
            return View();
        }

        // POST: GrupoCuartelesDetalles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "gcd_id,gc_id,Con_id")] GrupoCuartelesDetalle grupoCuartelesDetalle)
        {
            if (ModelState.IsValid)
            {
                db.GrupoCuartelesDetalle.Add(grupoCuartelesDetalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Con_id = new SelectList(db.contratos, "Con_Id", "Per_Rut", grupoCuartelesDetalle.Con_id);
            ViewBag.gc_id = new SelectList(db.GruposCuarteles, "gc_id", "gc_nom", grupoCuartelesDetalle.gc_id);
            return View(grupoCuartelesDetalle);
        }

        // GET: GrupoCuartelesDetalles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrupoCuartelesDetalle grupoCuartelesDetalle = db.GrupoCuartelesDetalle.Find(id);
            if (grupoCuartelesDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.Con_id = new SelectList(db.contratos, "Con_Id", "Per_Rut", grupoCuartelesDetalle.Con_id);
            ViewBag.gc_id = new SelectList(db.GruposCuarteles, "gc_id", "gc_nom", grupoCuartelesDetalle.gc_id);
            return View(grupoCuartelesDetalle);
        }

        // POST: GrupoCuartelesDetalles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "gcd_id,gc_id,Con_id")] GrupoCuartelesDetalle grupoCuartelesDetalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grupoCuartelesDetalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Con_id = new SelectList(db.contratos, "Con_Id", "Per_Rut", grupoCuartelesDetalle.Con_id);
            ViewBag.gc_id = new SelectList(db.GruposCuarteles, "gc_id", "gc_nom", grupoCuartelesDetalle.gc_id);
            return View(grupoCuartelesDetalle);
        }

        // GET: GrupoCuartelesDetalles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GrupoCuartelesDetalle grupoCuartelesDetalle = db.GrupoCuartelesDetalle.Find(id);
            if (grupoCuartelesDetalle == null)
            {
                return HttpNotFound();
            }
            return View(grupoCuartelesDetalle);
        }

        // POST: GrupoCuartelesDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GrupoCuartelesDetalle grupoCuartelesDetalle = db.GrupoCuartelesDetalle.Find(id);
            db.GrupoCuartelesDetalle.Remove(grupoCuartelesDetalle);
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
