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
    public class SectoresController : Controller
    {
        private radixEntities db = new radixEntities();

        // GET: Sectores
        public ActionResult Index(string subemp_id)
        {

            ViewBag.subemp_id = subemp_id;
            var sectores = db.Sectores.Include(s => s.subempresas);

            MultiplesClases multiples = new MultiplesClases
            {
                ObjESectores= sectores.ToList()
            };

            return View(multiples);
        }

        // GET: Sectores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sectores sectores = db.Sectores.Find(id);
            if (sectores == null)
            {
                return HttpNotFound();
            }
            return View(sectores);
        }

        // GET: Sectores/Create
        public ActionResult Create()
        {
            ViewBag.Sub_Id = new SelectList(db.subempresas, "Sub_Id", "Sub_Nom");
            return View();
        }

        // POST: Sectores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MultiplesClases multiples, string subemp_id)
        {
            if (ModelState.IsValid)
            {
                multiples.ObjSectores.Sub_Id = Convert.ToInt32(subemp_id);
                db.Sectores.Add(multiples.ObjSectores);
                db.SaveChanges();
                return RedirectToAction("Index",new { subemp_id });
            }

            ViewBag.Sub_Id = new SelectList(db.subempresas, "Sub_Id", "Sub_Nom", multiples.ObjSectores.Sub_Id);
            return View(multiples);
        }

        // GET: Sectores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sectores sectores = db.Sectores.Find(id);
            if (sectores == null)
            {
                return HttpNotFound();
            }
            ViewBag.Sub_Id = new SelectList(db.subempresas, "Sub_Id", "Sub_Nom", sectores.Sub_Id);
            return View(sectores);
        }

        // POST: Sectores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sect_id,sect_nom,Sub_Id")] Sectores sectores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sectores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Sub_Id = new SelectList(db.subempresas, "Sub_Id", "Sub_Nom", sectores.Sub_Id);
            return View(sectores);
        }

        // GET: Sectores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sectores sectores = db.Sectores.Find(id);
            if (sectores == null)
            {
                return HttpNotFound();
            }
            return View(sectores);
        }
       [HttpPost]
        public ActionResult borrar(string id, string subemp_id) {

            int ide = Convert.ToInt32(id);
            Sectores sectores = db.Sectores.Find(ide);
            db.Sectores.Remove(sectores);
            db.SaveChanges();

            return View("Index");
        }
        // POST: Sectores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sectores sectores = db.Sectores.Find(id);
            db.Sectores.Remove(sectores);
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
