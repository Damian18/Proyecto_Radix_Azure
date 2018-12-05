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
    public class PrivilegiosCuentasController : Controller
    {
        private radixEntities db = new radixEntities();
     
            // GET: PrivilegiosCuentas
            public ActionResult Index()
        {

            ViewBag.empresa = HttpContext.Session["Empresa"].ToString();
            ViewBag.emp_id = HttpContext.Session["Emp_id"].ToString();
            var privilegios = db.PrivilegiosCuentas;
            MultiplesClases multiples = new MultiplesClases
            {
                ObjEPrivilegiosCuentas = privilegios.ToList()
            };

            return View(multiples);
        }
        public ActionResult All()
        {
            //List<PrivilegiosCuentas> pr = db.PrivilegiosCuentas.ToList();
            //return Json(pr, JsonRequestBehavior.AllowGet);
            var todos = db.PrivilegiosCuentas.OrderBy(a => a.pc_id).ToList();
            return Json(new { data = todos }, JsonRequestBehavior.AllowGet);
            
        }
        [HttpGet]
        public ActionResult Agregar(int id)
        {
            //List<PrivilegiosCuentas> pr = db.PrivilegiosCuentas.ToList();
            //return Json(pr, JsonRequestBehavior.AllowGet);
            var s = db.PrivilegiosCuentas.Where(a => a.pc_id == id).FirstOrDefault(); ;
            return View(s);

        }
        [HttpPost]
        public ActionResult Agregar(PrivilegiosCuentas pc)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                if (pc.pc_id > 0)
                {
                    //edit
                    var ss = db.PrivilegiosCuentas.Where(a => a.pc_id == pc.pc_id).FirstOrDefault();
                    if (ss != null)
                    {
                        ss.pc_nom = pc.pc_nom;
                    }
                }
                else
                {
                    //save
                    db.PrivilegiosCuentas.Add(pc);

                }
                db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status=status} };

        }
        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            var s = db.PrivilegiosCuentas.Where(a => a.pc_id == id).FirstOrDefault();

            if (s != null)
            {
                return View(s);
            }
            else {
                return HttpNotFound();
            }
        }
        [HttpPost]
        [ActionName("Eliminar")]
        public ActionResult EliminarConfirmar(int id)
        {

            bool status = false;
            var s = db.PrivilegiosCuentas.Where(a => a.pc_id == id).FirstOrDefault();

            if (s != null)
            {
                db.PrivilegiosCuentas.Remove(s);
                db.SaveChanges();
                status = true;            }
            else
            {
                return HttpNotFound();
            }
            return new JsonResult { Data = new { status = status } };
        }








        // GET: PrivilegiosCuentas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrivilegiosCuentas privilegiosCuentas = db.PrivilegiosCuentas.Find(id);
            if (privilegiosCuentas == null)
            {
                return HttpNotFound();
            }
            return View(privilegiosCuentas);
        }

        // GET: PrivilegiosCuentas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrivilegiosCuentas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MultiplesClases multiples)
        {
            if (ModelState.IsValid)
            {
              
                db.PrivilegiosCuentas.Add(multiples.ObjPrivilegiosCuentas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(multiples);
        }

        // GET: PrivilegiosCuentas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrivilegiosCuentas privilegiosCuentas = db.PrivilegiosCuentas.Find(id);
            if (privilegiosCuentas == null)
            {
                return HttpNotFound();
            }
            return View(privilegiosCuentas);
        }

        // POST: PrivilegiosCuentas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pc_id,pc_nom")] PrivilegiosCuentas privilegiosCuentas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(privilegiosCuentas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(privilegiosCuentas);
        }

        // GET: PrivilegiosCuentas/Delete/5
        public ActionResult Delete(MultiplesClases multiples, string id)
        {
            multiples.ObjPrivilegiosCuentas.pc_id = Convert.ToInt32(id);
            if (ModelState.IsValid)
            {
                multiples.ObjPrivilegiosCuentas = db.PrivilegiosCuentas.Find(id);
                db.PrivilegiosCuentas.Remove(multiples.ObjPrivilegiosCuentas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(multiples);
        }

        // POST: PrivilegiosCuentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(MultiplesClases multiples,string id)
        {
            multiples.ObjPrivilegiosCuentas.pc_id = Convert.ToInt32(id);
            if (id == null)
            {
                return View(multiples);
            }
            if (ModelState.IsValid)
            {
                multiples.ObjPrivilegiosCuentas = db.PrivilegiosCuentas.Find(id);
                db.PrivilegiosCuentas.Remove(multiples.ObjPrivilegiosCuentas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(multiples);
            //db.PrivilegiosCuentas.Remove(multiples.ObjPrivilegiosCuentas);
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
