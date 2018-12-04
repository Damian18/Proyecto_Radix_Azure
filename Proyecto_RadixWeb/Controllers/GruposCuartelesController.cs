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
    public class GruposCuartelesController : Controller
    {
        private radixEntities db = new radixEntities();

        // GET: GruposCuarteles
        public ActionResult Index(string cuar_id)
        {
            ViewBag.cuar_id = cuar_id;
            int cuar = Convert.ToInt32(cuar_id.ToString());
            var grupos = db.GruposCuarteles.Include(c => c.Cuarteles).Include(p=>p.contratos);
            MultiplesClases multiples = new MultiplesClases
            {
                ObjEGruposCuarteles = grupos.Where(e => e.cuar_id == cuar).ToList()
            };
            return View(multiples);
        }

        // GET: GruposCuarteles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GruposCuarteles gruposCuarteles = db.GruposCuarteles.Find(id);
            if (gruposCuarteles == null)
            {
                return HttpNotFound();
            }
            return View(gruposCuarteles);
        }

        // GET: GruposCuarteles/Create
        public ActionResult Create()
        {
            ViewBag.Con_id = new SelectList(db.contratos, "Con_Id", "Per_Rut");
            ViewBag.ConJefe_id = new SelectList(db.contratos, "Con_Id", "Per_Rut");
            ViewBag.cuar_id = new SelectList(db.Cuarteles, "cuar_id", "cuar_nom");
            return View();
        }

        // POST: GruposCuarteles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "gc_id,cuar_id,Con_id,ConJefe_id")] GruposCuarteles gruposCuarteles)
        {
            if (ModelState.IsValid)
            {
                db.GruposCuarteles.Add(gruposCuarteles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Con_id = new SelectList(db.contratos, "Con_Id", "Per_Rut", gruposCuarteles.Con_id);
            ViewBag.ConJefe_id = new SelectList(db.contratos, "Con_Id", "Per_Rut", gruposCuarteles.ConJefe_id);
            ViewBag.cuar_id = new SelectList(db.Cuarteles, "cuar_id", "cuar_nom", gruposCuarteles.cuar_id);
            return View(gruposCuarteles);
        }

        // GET: GruposCuarteles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GruposCuarteles gruposCuarteles = db.GruposCuarteles.Find(id);
            if (gruposCuarteles == null)
            {
                return HttpNotFound();
            }
            ViewBag.Con_id = new SelectList(db.contratos, "Con_Id", "Per_Rut", gruposCuarteles.Con_id);
            ViewBag.ConJefe_id = new SelectList(db.contratos, "Con_Id", "Per_Rut", gruposCuarteles.ConJefe_id);
            ViewBag.cuar_id = new SelectList(db.Cuarteles, "cuar_id", "cuar_nom", gruposCuarteles.cuar_id);
            return View(gruposCuarteles);
        }

        // POST: GruposCuarteles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "gc_id,cuar_id,Con_id,ConJefe_id")] GruposCuarteles gruposCuarteles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gruposCuarteles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Con_id = new SelectList(db.contratos, "Con_Id", "Per_Rut", gruposCuarteles.Con_id);
            ViewBag.ConJefe_id = new SelectList(db.contratos, "Con_Id", "Per_Rut", gruposCuarteles.ConJefe_id);
            ViewBag.cuar_id = new SelectList(db.Cuarteles, "cuar_id", "cuar_nom", gruposCuarteles.cuar_id);
            return View(gruposCuarteles);
        }

        // GET: GruposCuarteles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GruposCuarteles gruposCuarteles = db.GruposCuarteles.Find(id);
            if (gruposCuarteles == null)
            {
                return HttpNotFound();
            }
            return View(gruposCuarteles);
        }

        // POST: GruposCuarteles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GruposCuarteles gruposCuarteles = db.GruposCuarteles.Find(id);
            db.GruposCuarteles.Remove(gruposCuarteles);
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
