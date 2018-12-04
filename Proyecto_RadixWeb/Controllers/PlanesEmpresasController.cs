using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto_RadixWeb.Models;

namespace Proyecto_RadixWeb.Controllers
{
    public class PlanesEmpresasController : Controller
    {
        private radixEntities db = new radixEntities();

        // GET: PlanesEmpresas
        public async Task<ActionResult> Index()
        {
            return View(await db.PlanesEmpresas.ToListAsync());
        }

        // GET: PlanesEmpresas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanesEmpresas planesEmpresas = await db.PlanesEmpresas.FindAsync(id);
            if (planesEmpresas == null)
            {
                return HttpNotFound();
            }
            return View(planesEmpresas);
        }

        // GET: PlanesEmpresas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlanesEmpresas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "plan_id,plan_nom")] PlanesEmpresas planesEmpresas)
        {
            if (ModelState.IsValid)
            {
                db.PlanesEmpresas.Add(planesEmpresas);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(planesEmpresas);
        }

        // GET: PlanesEmpresas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanesEmpresas planesEmpresas = await db.PlanesEmpresas.FindAsync(id);
            if (planesEmpresas == null)
            {
                return HttpNotFound();
            }
            return View(planesEmpresas);
        }

        // POST: PlanesEmpresas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "plan_id,plan_nom")] PlanesEmpresas planesEmpresas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planesEmpresas).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(planesEmpresas);
        }

        // GET: PlanesEmpresas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanesEmpresas planesEmpresas = await db.PlanesEmpresas.FindAsync(id);
            if (planesEmpresas == null)
            {
                return HttpNotFound();
            }
            return View(planesEmpresas);
        }

        // POST: PlanesEmpresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PlanesEmpresas planesEmpresas = await db.PlanesEmpresas.FindAsync(id);
            db.PlanesEmpresas.Remove(planesEmpresas);
            await db.SaveChangesAsync();
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
