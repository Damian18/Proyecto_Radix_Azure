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
    public class SolicitudDetalleQrsController : Controller
    {
        private radixEntities db = new radixEntities();

        // GET: SolicitudDetalleQrs
        public ActionResult Index(string sol_id)
        {
            int id= Convert.ToInt32(sol_id);

            var solicitudDetalleQr = db.SolicitudDetalleQr.Include(s => s.contratos).Include(s => s.SolicitudesQr);
            return View(solicitudDetalleQr.Where(s=>s.sqr_id==id).ToList());
        }

        // GET: SolicitudDetalleQrs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudDetalleQr solicitudDetalleQr = db.SolicitudDetalleQr.Find(id);
            if (solicitudDetalleQr == null)
            {
                return HttpNotFound();
            }
            return View(solicitudDetalleQr);
        }

        // GET: SolicitudDetalleQrs/Create
        public ActionResult Create()
        {
            ViewBag.Con_id = new SelectList(db.contratos, "Con_Id", "Per_Rut");
            ViewBag.sqr_id = new SelectList(db.SolicitudesQr, "sqr_id", "sqr_id");
            return View();
        }

        // POST: SolicitudDetalleQrs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sdqr_id,sqr_id,Con_id")] SolicitudDetalleQr solicitudDetalleQr)
        {
            if (ModelState.IsValid)
            {
                db.SolicitudDetalleQr.Add(solicitudDetalleQr);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Con_id = new SelectList(db.contratos, "Con_Id", "Per_Rut", solicitudDetalleQr.Con_id);
            ViewBag.sqr_id = new SelectList(db.SolicitudesQr, "sqr_id", "sqr_id", solicitudDetalleQr.sqr_id);
            return View(solicitudDetalleQr);
        }

        // GET: SolicitudDetalleQrs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudDetalleQr solicitudDetalleQr = db.SolicitudDetalleQr.Find(id);
            if (solicitudDetalleQr == null)
            {
                return HttpNotFound();
            }
            ViewBag.Con_id = new SelectList(db.contratos, "Con_Id", "Per_Rut", solicitudDetalleQr.Con_id);
            ViewBag.sqr_id = new SelectList(db.SolicitudesQr, "sqr_id", "sqr_id", solicitudDetalleQr.sqr_id);
            return View(solicitudDetalleQr);
        }

        // POST: SolicitudDetalleQrs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sdqr_id,sqr_id,Con_id")] SolicitudDetalleQr solicitudDetalleQr)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitudDetalleQr).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Con_id = new SelectList(db.contratos, "Con_Id", "Per_Rut", solicitudDetalleQr.Con_id);
            ViewBag.sqr_id = new SelectList(db.SolicitudesQr, "sqr_id", "sqr_id", solicitudDetalleQr.sqr_id);
            return View(solicitudDetalleQr);
        }

        // GET: SolicitudDetalleQrs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudDetalleQr solicitudDetalleQr = db.SolicitudDetalleQr.Find(id);
            if (solicitudDetalleQr == null)
            {
                return HttpNotFound();
            }
            return View(solicitudDetalleQr);
        }

        // POST: SolicitudDetalleQrs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SolicitudDetalleQr solicitudDetalleQr = db.SolicitudDetalleQr.Find(id);
            db.SolicitudDetalleQr.Remove(solicitudDetalleQr);
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
