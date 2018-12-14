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
        public ActionResult Index(string cuar_id,string subemp_id)
        {
            ViewBag.subemp_id = subemp_id;
            ViewBag.cuar_id = cuar_id;
            int cuar = Convert.ToInt32(cuar_id.ToString());

            var listacuar = db.Cuarteles.FirstOrDefault(c=>c.cuar_id== cuar);

            int? subemp = listacuar.Sectores.Sub_Id;

            int sub_id =Convert.ToInt32(subemp);


            var listajefes = db.contratos.Where(c=>c.personas.cargos.Car_Nom== "Jefe Cuadrilla").Select(v => new {
                Con_id = v.Con_Id,
                NombreJefe = v.personas.Per_Nom + " " + v.personas.Per_ApePat
            });

            ViewBag.ObjGruposCuarteles_ConJefe_id = new SelectList(listajefes, "Con_id", "NombreJefe");


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
        public ActionResult Create(MultiplesClases multiples,string cuar_id)
        {
            if (ModelState.IsValid)
            {
                multiples.ObjGruposCuarteles.cuar_id = Convert.ToInt32(cuar_id);


                db.GruposCuarteles.Add(multiples.ObjGruposCuarteles);
                db.SaveChanges();
                return RedirectToAction("Index",new { cuar_id});
            }

        
            return View(multiples);
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
        [HttpPost]
        public ActionResult Eliminar(GruposCuarteles grup, string cuar_id)
        {
            int? id2 = grup.gc_id;
            int id = Convert.ToInt32(id2);

            GruposCuarteles gc = db.GruposCuarteles.Find(id);
            db.GruposCuarteles.Remove(gc);
            db.SaveChanges();
            return RedirectToAction("Index", new { cuar_id });
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
