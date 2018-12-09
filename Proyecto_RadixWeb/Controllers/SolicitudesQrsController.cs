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
    public class SolicitudesQrsController : Controller
    {
        private radixEntities db = new radixEntities();

        // GET: SolicitudesQrs
        public ActionResult Index()
        {
            var solicitudesQr = db.SolicitudesQr.Include(s => s.subempresas);
            return View(solicitudesQr.ToList());
        }

        // GET: SolicitudesQrs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudesQr solicitudesQr = db.SolicitudesQr.Find(id);
            if (solicitudesQr == null)
            {
                return HttpNotFound();
            }
            return View(solicitudesQr);
        }

        // GET: SolicitudesQrs/Create
        public ActionResult Create()
        {
            ViewBag.Sub_id = new SelectList(db.subempresas, "Sub_Id", "Sub_Nom");
            return View();
        }

        // POST: SolicitudesQrs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sqr_id,Sub_id,sqr_estado")] SolicitudesQr solicitudesQr)
        {
            if (ModelState.IsValid)
            {
                db.SolicitudesQr.Add(solicitudesQr);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Sub_id = new SelectList(db.subempresas, "Sub_Id", "Sub_Nom", solicitudesQr.Sub_id);
            return View(solicitudesQr);
        }

        // GET: SolicitudesQrs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudesQr solicitudesQr = db.SolicitudesQr.Find(id);
            if (solicitudesQr == null)
            {
                return HttpNotFound();
            }
            ViewBag.Sub_id = new SelectList(db.subempresas, "Sub_Id", "Sub_Nom", solicitudesQr.Sub_id);
            return View(solicitudesQr);
        }

        // POST: SolicitudesQrs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sqr_id,Sub_id,sqr_estado")] SolicitudesQr solicitudesQr)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitudesQr).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Sub_id = new SelectList(db.subempresas, "Sub_Id", "Sub_Nom", solicitudesQr.Sub_id);
            return View(solicitudesQr);
        }

        // GET: SolicitudesQrs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudesQr solicitudesQr = db.SolicitudesQr.Find(id);
            if (solicitudesQr == null)
            {
                return HttpNotFound();
            }
            return View(solicitudesQr);
        }

        // POST: SolicitudesQrs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SolicitudesQr solicitudesQr = db.SolicitudesQr.Find(id);
            db.SolicitudesQr.Remove(solicitudesQr);
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
