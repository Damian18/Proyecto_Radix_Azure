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
        public ActionResult Index(string sect_id)
        {
         
            ViewBag.sect_id = sect_id;
            int sec = Convert.ToInt32(sect_id.ToString());
            var cuarteles = db.Cuarteles.Include(c => c.Sectores).Include(c => c.VariedadesFrutas);
            MultiplesClases multiples = new MultiplesClases
            {
                ObjECuarteles = cuarteles.Where(e => e.sect_id == sec).ToList()
            };
            return View(multiples);
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
        public ActionResult Create(MultiplesClases multiples,string sect_id)
        {
            if (ModelState.IsValid)
            {
                multiples.ObjCuarteles.sect_id = Convert.ToInt32(sect_id);
                db.Cuarteles.Add(multiples.ObjCuarteles);
                db.SaveChanges();
                return RedirectToAction("Index", new { sect_id });
            }
            ViewBag.sect_id = new SelectList(db.Sectores, "sect_id", "sect_nom", multiples.ObjCuarteles.sect_id);
            ViewBag.varfrut_id = new SelectList(db.VariedadesFrutas, "varfrut_id", "var_nom", multiples.ObjCuarteles.varfrut_id);
            return View(multiples);

        
          
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
