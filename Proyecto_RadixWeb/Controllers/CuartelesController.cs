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
    public class CuartelesController : Controller
    {
        private radixEntities db = new radixEntities();

        // GET: Cuarteles
        public ActionResult Index(string subemp_id)
        {
            int sub_id = Convert.ToInt32(subemp_id);

            ViewBag.subemp_id = subemp_id;

            var cuarteles = db.Cuarteles.Include(c => c.Sectores).Include(c => c.VariedadesFrutas);
            return View(cuarteles.Where(c=>c.Sectores.Sub_Id==sub_id).ToList());
        }

        // GET: Cuarteles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuarteles cuarteles = db.Cuarteles.Find(id);
            if (cuarteles == null)
            {
                return HttpNotFound();
            }
            return View(cuarteles);
        }

        // GET: Cuarteles/Create
        public ActionResult Create()
        {
            ViewBag.sect_id = new SelectList(db.Sectores, "sect_id", "sect_nom");
            ViewBag.varfrut_id = new SelectList(db.VariedadesFrutas, "varfrut_id", "var_nom");
            return View();
        }

        // POST: Cuarteles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cuar_id,cuar_nom,varfrut_id,sect_id")] Cuarteles cuarteles)
        {
            if (ModelState.IsValid)
            {
                db.Cuarteles.Add(cuarteles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.sect_id = new SelectList(db.Sectores, "sect_id", "sect_nom", cuarteles.sect_id);
            ViewBag.varfrut_id = new SelectList(db.VariedadesFrutas, "varfrut_id", "var_nom", cuarteles.varfrut_id);
            return View(cuarteles);
        }

        // GET: Cuarteles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuarteles cuarteles = db.Cuarteles.Find(id);
            if (cuarteles == null)
            {
                return HttpNotFound();
            }
            ViewBag.sect_id = new SelectList(db.Sectores, "sect_id", "sect_nom", cuarteles.sect_id);
            ViewBag.varfrut_id = new SelectList(db.VariedadesFrutas, "varfrut_id", "var_nom", cuarteles.varfrut_id);
            return View(cuarteles);
        }

        // POST: Cuarteles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cuar_id,cuar_nom,varfrut_id,sect_id")] Cuarteles cuarteles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuarteles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.sect_id = new SelectList(db.Sectores, "sect_id", "sect_nom", cuarteles.sect_id);
            ViewBag.varfrut_id = new SelectList(db.VariedadesFrutas, "varfrut_id", "var_nom", cuarteles.varfrut_id);
            return View(cuarteles);
        }

        // GET: Cuarteles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuarteles cuarteles = db.Cuarteles.Find(id);
            if (cuarteles == null)
            {
                return HttpNotFound();
            }
            return View(cuarteles);
        }

        // POST: Cuarteles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cuarteles cuarteles = db.Cuarteles.Find(id);
            db.Cuarteles.Remove(cuarteles);
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
