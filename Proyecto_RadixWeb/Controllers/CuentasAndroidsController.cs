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
    public class CuentasAndroidsController : Controller
    {
        private radixEntities db = new radixEntities();

        // GET: CuentasAndroids
        public async Task<ActionResult> Index()
        {
            return View(await db.CuentasAndroid.ToListAsync());
        }

        // GET: CuentasAndroids/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentasAndroid cuentasAndroid = await db.CuentasAndroid.FindAsync(id);
            if (cuentasAndroid == null)
            {
                return HttpNotFound();
            }
            return View(cuentasAndroid);
        }

        // GET: CuentasAndroids/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CuentasAndroids/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ca_id,ca_empresa,ca_usuario,ca_password,ca_subempresa")] CuentasAndroid cuentasAndroid)
        {
            if (ModelState.IsValid)
            {
                db.CuentasAndroid.Add(cuentasAndroid);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cuentasAndroid);
        }

        // GET: CuentasAndroids/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentasAndroid cuentasAndroid = await db.CuentasAndroid.FindAsync(id);
            if (cuentasAndroid == null)
            {
                return HttpNotFound();
            }
            return View(cuentasAndroid);
        }

        // POST: CuentasAndroids/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ca_id,ca_empresa,ca_usuario,ca_password,ca_subempresa")] CuentasAndroid cuentasAndroid)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuentasAndroid).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cuentasAndroid);
        }

        // GET: CuentasAndroids/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentasAndroid cuentasAndroid = await db.CuentasAndroid.FindAsync(id);
            if (cuentasAndroid == null)
            {
                return HttpNotFound();
            }
            return View(cuentasAndroid);
        }

        // POST: CuentasAndroids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CuentasAndroid cuentasAndroid = await db.CuentasAndroid.FindAsync(id);
            db.CuentasAndroid.Remove(cuentasAndroid);
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
