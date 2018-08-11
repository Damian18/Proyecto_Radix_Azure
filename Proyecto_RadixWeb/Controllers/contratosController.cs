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
    public class ContratosController : Controller
    {
        private radixEntities db = new radixEntities();

        // GET: contratos
        public ActionResult Index(int? subemp_id)
        {
            ViewBag.empresa = HttpContext.Session["Empresa"].ToString();

            ViewBag.subemp_id = subemp_id;
            

            var contratos = db.contratos.Include(c => c.personas).Include(c => c.planillascontratos).Include(c => c.subempresas).Include(c => c.tiposcontratos);
            return View(contratos.Where(c => c.Sub_Id == subemp_id).ToList());
        }

        public ActionResult RedirecionarPersonas(int? subemp_id)
        {
            return RedirectToAction("Create", "Personas", new { subemp_id });
        }

        public ActionResult RedirecionarCuenta(string subemp_id, string per_rut,string car_nom)
        {
            return RedirectToAction("CuentaPersonas", "Account", new { subemp_id, per_rut, car_nom });
        }

        // GET: contratos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contratos contratos = db.contratos.Find(id);
            if (contratos == null)
            {
                return HttpNotFound();
            }
            return View(contratos);
        }

        // GET: contratos/Create
        public ActionResult Create()
        {
            ViewBag.Per_Rut = new SelectList(db.personas, "Per_Rut", "Per_Nom");
            ViewBag.PC_Id = new SelectList(db.planillascontratos, "PC_Id", "PC_Nom");
            ViewBag.Sub_Id = new SelectList(db.subempresas, "Sub_Id", "Sub_Nom");
            ViewBag.TCon_Id = new SelectList(db.tiposcontratos, "TCon_Id", "TCon_Nom");
            return View();
        }

        // POST: contratos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Con_Id,Sub_Id,PC_Id,TCon_Id,Per_Rut,Con_FechaInicio,Con_FechaFin,Con_Estado")] contratos contratos)
        {
            if (ModelState.IsValid)
            {
                db.contratos.Add(contratos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Per_Rut = new SelectList(db.personas, "Per_Rut", "Per_Nom", contratos.Per_Rut);
            ViewBag.PC_Id = new SelectList(db.planillascontratos, "PC_Id", "PC_Nom", contratos.PC_Id);
            ViewBag.Sub_Id = new SelectList(db.subempresas, "Sub_Id", "Sub_Nom", contratos.Sub_Id);
            ViewBag.TCon_Id = new SelectList(db.tiposcontratos, "TCon_Id", "TCon_Nom", contratos.TCon_Id);
            return View(contratos);
        }

        // GET: contratos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contratos contratos = db.contratos.Find(id);
            if (contratos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Per_Rut = new SelectList(db.personas, "Per_Rut", "Per_Nom", contratos.Per_Rut);
            ViewBag.PC_Id = new SelectList(db.planillascontratos, "PC_Id", "PC_Nom", contratos.PC_Id);
            ViewBag.Sub_Id = new SelectList(db.subempresas, "Sub_Id", "Sub_Nom", contratos.Sub_Id);
            ViewBag.TCon_Id = new SelectList(db.tiposcontratos, "TCon_Id", "TCon_Nom", contratos.TCon_Id);
            return View(contratos);
        }

        // POST: contratos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Con_Id,Sub_Id,PC_Id,TCon_Id,Per_Rut,Con_FechaInicio,Con_FechaFin,Con_Estado")] contratos contratos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contratos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Per_Rut = new SelectList(db.personas, "Per_Rut", "Per_Nom", contratos.Per_Rut);
            ViewBag.PC_Id = new SelectList(db.planillascontratos, "PC_Id", "PC_Nom", contratos.PC_Id);
            ViewBag.Sub_Id = new SelectList(db.subempresas, "Sub_Id", "Sub_Nom", contratos.Sub_Id);
            ViewBag.TCon_Id = new SelectList(db.tiposcontratos, "TCon_Id", "TCon_Nom", contratos.TCon_Id);
            return View(contratos);
        }

        // GET: contratos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contratos contratos = db.contratos.Find(id);
            if (contratos == null)
            {
                return HttpNotFound();
            }
            return View(contratos);
        }

        // POST: contratos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            contratos contratos = db.contratos.Find(id);
            db.contratos.Remove(contratos);
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
