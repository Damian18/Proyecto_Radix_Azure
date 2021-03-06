﻿using System;
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
            int sub = Convert.ToInt32(subemp_id.ToString());
            var sectores = db.Sectores.Include(s => s.subempresas);

            MultiplesClases multiples = new MultiplesClases
            {
                ObjESectores = sectores.Where(e => e.Sub_Id == sub).OrderBy(s=>s.sect_nom).ToList()
            };

            return View(multiples);
        }
        public ActionResult All()
        {

            string emp_nom = HttpContext.Session["Empresa"].ToString();
            int emp_id = Convert.ToInt32(HttpContext.Session["Emp_id"].ToString());
            ViewBag.empresa = emp_nom;
          
            int contar4 = db.Sectores.Count(s => s.subempresas.empresas.Emp_Id == emp_id);
            ViewBag.contarsectores = contar4;
            var sec = db.Sectores.Include(c => c.subempresas);
            MultiplesClases multiples = new MultiplesClases
            {
                ObjESectores = sec.Where(s => s.subempresas.empresas.Emp_Nom == emp_nom).OrderBy(c=>c.sect_nom).ToList()
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
        public ActionResult borrar(int id) {
       
            Sectores sectores = db.Sectores.Find(id);
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

        [HttpPost]
        public ActionResult Eliminar(Sectores sect, string subemp_id)
        {
            int? id2 = sect.sect_id;
            int id = Convert.ToInt32(id2);

            var cuarteles = db.Cuarteles.Where(c => c.sect_id == id);

            foreach (var item in cuarteles)
            {
                var grupos = db.GruposCuarteles.Where(gc => gc.cuar_id == item.cuar_id);


                if (grupos != null)
                {

                    foreach (var item2 in grupos)
                    {
                        var grupodetalle = db.GrupoCuartelesDetalle.Where(gd=>gd.gc_id==item2.gc_id);

                        if (grupodetalle!=null)
                        {
                            foreach (var item3 in grupodetalle)
                            {
                                GrupoCuartelesDetalle detalle = db.GrupoCuartelesDetalle.Find(item3.gc_id);
                                db.GrupoCuartelesDetalle.Remove(detalle);
                            }
                           
                        }

                        GruposCuarteles gc = db.GruposCuarteles.Find(item2.gc_id);
                        db.GruposCuarteles.Remove(gc);
                    }
                  
                }

                Cuarteles ca = db.Cuarteles.Find(item.cuar_id);
                db.Cuarteles.Remove(ca);
            }
            db.SaveChanges();

            Sectores sectores = db.Sectores.Find(id);
            db.Sectores.Remove(sectores);
            db.SaveChanges();
            return RedirectToAction("Index",new { subemp_id });
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
