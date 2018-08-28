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
    public class AsistenciasController : Controller
    {
        private radixEntities db = new radixEntities();

        // GET: Asistencias
        public ActionResult Index(int? subemp_id ,int? car_id)
        {
            var subempcar= db.subempresa_cargo.FirstOrDefault(s=>s.Sub_Id==subemp_id&&s.Car_Id==car_id);

            var asistencias = db.asistencias.Include(a => a.contratos).Include(a => a.horario_laboral);
            return View(asistencias.Where(a=>a.horario_laboral.Subempcar_id==subempcar.Subempcar_id).ToList());
        }

        // GET: Asistencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asistencias asistencias = db.asistencias.Find(id);
            if (asistencias == null)
            {
                return HttpNotFound();
            }
            return View(asistencias);
        }

        // GET: Asistencias/Create
        public ActionResult Create()
        {
            ViewBag.Con_Id = new SelectList(db.contratos, "Con_Id", "Per_Rut");
            ViewBag.Hl_Id = new SelectList(db.horario_laboral, "Hl_Id", "Hl_Titulo");
            return View();
        }

        // POST: Asistencias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "asis_id,Con_Id,Hl_Id,asis_estado")] asistencias asistencias)
        {
            if (ModelState.IsValid)
            {
                db.asistencias.Add(asistencias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Con_Id = new SelectList(db.contratos, "Con_Id", "Per_Rut", asistencias.Con_Id);
            ViewBag.Hl_Id = new SelectList(db.horario_laboral, "Hl_Id", "Hl_Titulo", asistencias.Hl_Id);
            return View(asistencias);
        }

        // GET: Asistencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asistencias asistencias = db.asistencias.Find(id);
            if (asistencias == null)
            {
                return HttpNotFound();
            }
            ViewBag.Con_Id = new SelectList(db.contratos, "Con_Id", "Per_Rut", asistencias.Con_Id);
            ViewBag.Hl_Id = new SelectList(db.horario_laboral, "Hl_Id", "Hl_Titulo", asistencias.Hl_Id);
            return View(asistencias);
        }

        // POST: Asistencias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "asis_id,Con_Id,Hl_Id,asis_estado")] asistencias asistencias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asistencias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Con_Id = new SelectList(db.contratos, "Con_Id", "Per_Rut", asistencias.Con_Id);
            ViewBag.Hl_Id = new SelectList(db.horario_laboral, "Hl_Id", "Hl_Titulo", asistencias.Hl_Id);
            return View(asistencias);
        }

        // GET: Asistencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asistencias asistencias = db.asistencias.Find(id);
            if (asistencias == null)
            {
                return HttpNotFound();
            }
            return View(asistencias);
        }

        // POST: Asistencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            asistencias asistencias = db.asistencias.Find(id);
            db.asistencias.Remove(asistencias);
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
