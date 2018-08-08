﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IdentitySample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Proyecto_RadixWeb.Models;

namespace Proyecto_RadixWeb.Controllers
{
    public class PersonasController : Controller
    {
        private radixEntities db = new radixEntities();

        // GET: personas
        public ActionResult Index()
        {
            var personas = db.personas.Include(p => p.cargos).Include(p => p.estadosciviles).Include(p => p.fichadescuentos).Include(p => p.generos).Include(p => p.nacionalidades).Include(p => p.tiposhorasextras).Include(p => p.tipoimpuestos);
            return View(personas.ToList());
        }


        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult CuentaPersonas(int? subemp_id, string subemp_nom, string per_rut)
        {
            ViewBag.empresa = subemp_nom;
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CuentaPersonas(MultiplesClases model, string subemp_nom, string per_rut)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.objRegistrar.Email, Email = model.objRegistrar.Email };
                var result = await UserManager.CreateAsync(user, model.objRegistrar.Password);
                
                if (result.Succeeded)
                {
                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                  
                }
                AddErrors(result);

                string idcuenta = user.Id;
                personas personas = db.personas.FirstOrDefault(p=>p.Per_Rut==per_rut);
                cargos cargos = db.cargos.FirstOrDefault(c=>c.Car_Id==personas.Car_Id);

                aspnetroles asprol = db.aspnetroles.FirstOrDefault(r => r.Name == cargos.Car_Nom);

                var rol = new aspnetuserroles
                {
                    UserId = idcuenta,
                    RoleId = asprol.Id
                };

                db.aspnetuserroles.Add(rol);
                db.SaveChanges();

                subempresas subempresas  = db.subempresas.FirstOrDefault(e => e.Sub_Nom == subemp_nom);

                int empresa_id = subempresas.Emp_Id;

                var log = new login
                {
                    Emp_Id = empresa_id,
                    Id = idcuenta,
                    Per_Rut=per_rut

                };

                db.login.Add(log);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        // GET: personas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            personas personas = db.personas.Find(id);
            if (personas == null)
            {
                return HttpNotFound();
            }
            return View(personas);
        }

        // GET: personas/Create
        [Authorize]
        public ActionResult Create(int? id, string nombre)
        {
            ViewBag.empresa = nombre;

            ViewBag.Car_Id = new SelectList(db.cargos, "Car_Id", "Car_Nom");
            ViewBag.EC_Id = new SelectList(db.estadosciviles, "EC_Id", "EC_Nom");
            ViewBag.Desc_Id = new SelectList(db.fichadescuentos, "Desc_Id", "Desc_Nom");
            ViewBag.Gen_Id = new SelectList(db.generos, "Gen_Id", "Gen_Nom");
            ViewBag.Nac_Id = new SelectList(db.nacionalidades, "Nac_Id", "Nac_Nom");
            ViewBag.THor_Id = new SelectList(db.tiposhorasextras, "THor_Id", "THor_Nom");
            ViewBag.TImp_Id = new SelectList(db.tipoimpuestos, "TImp_Id", "TImp_nom");
            return View();
        }

        // POST: personas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Per_Rut,Per_Nom,Per_ApePat,Per_ApeMat,Per_Dir,Per_Tel,Nac_Id,Gen_Id,EC_Id,Car_Id,Per_Suel,TImp_Id,Per_Des,Desc_Id,THor_Id")] personas personas, contratos contratos,int id)
        {
            if (ModelState.IsValid)
            {
                db.personas.Add(personas);
                db.SaveChanges();
               

                contratos.Per_Rut = personas.Per_Rut;
                contratos.Sub_Id = id;
                contratos.Con_FechaInicio = DateTime.Now.ToShortDateString();
                contratos.Con_Estado = "Espera";

                db.contratos.Add(contratos);
                db.SaveChanges();

                return RedirectToAction("Index","Contratos",new { id});
            }

            ViewBag.Car_Id = new SelectList(db.cargos, "Car_Id", "Car_Nom", personas.Car_Id);
            ViewBag.EC_Id = new SelectList(db.estadosciviles, "EC_Id", "EC_Nom", personas.EC_Id);
            ViewBag.Desc_Id = new SelectList(db.fichadescuentos, "Desc_Id", "Desc_Nom", personas.Desc_Id);
            ViewBag.Gen_Id = new SelectList(db.generos, "Gen_Id", "Gen_Nom", personas.Gen_Id);
            ViewBag.Nac_Id = new SelectList(db.nacionalidades, "Nac_Id", "Nac_Nom", personas.Nac_Id);
            ViewBag.THor_Id = new SelectList(db.tiposhorasextras, "THor_Id", "THor_Nom", personas.THor_Id);
            ViewBag.TImp_Id = new SelectList(db.tipoimpuestos, "TImp_Id", "TImp_nom", personas.TImp_Id);
            return View(personas);
        }

        // GET: personas/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            personas personas = db.personas.Find(id);
            if (personas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Car_Id = new SelectList(db.cargos, "Car_Id", "Car_Nom", personas.Car_Id);
            ViewBag.EC_Id = new SelectList(db.estadosciviles, "EC_Id", "EC_Nom", personas.EC_Id);
            ViewBag.Desc_Id = new SelectList(db.fichadescuentos, "Desc_Id", "Desc_Nom", personas.Desc_Id);
            ViewBag.Gen_Id = new SelectList(db.generos, "Gen_Id", "Gen_Nom", personas.Gen_Id);
            ViewBag.Nac_Id = new SelectList(db.nacionalidades, "Nac_Id", "Nac_Nom", personas.Nac_Id);
            ViewBag.THor_Id = new SelectList(db.tiposhorasextras, "THor_Id", "THor_Nom", personas.THor_Id);
            ViewBag.TImp_Id = new SelectList(db.tipoimpuestos, "TImp_Id", "TImp_nom", personas.TImp_Id);
            return View(personas);
        }

        // POST: personas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Per_Rut,Per_Nom,Per_ApePat,Per_ApeMat,Per_Dir,Per_Tel,Nac_Id,Gen_Id,EC_Id,Car_Id,Per_Suel,TImp_Id,Per_Des,Desc_Id,THor_Id")] personas personas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Car_Id = new SelectList(db.cargos, "Car_Id", "Car_Nom", personas.Car_Id);
            ViewBag.EC_Id = new SelectList(db.estadosciviles, "EC_Id", "EC_Nom", personas.EC_Id);
            ViewBag.Desc_Id = new SelectList(db.fichadescuentos, "Desc_Id", "Desc_Nom", personas.Desc_Id);
            ViewBag.Gen_Id = new SelectList(db.generos, "Gen_Id", "Gen_Nom", personas.Gen_Id);
            ViewBag.Nac_Id = new SelectList(db.nacionalidades, "Nac_Id", "Nac_Nom", personas.Nac_Id);
            ViewBag.THor_Id = new SelectList(db.tiposhorasextras, "THor_Id", "THor_Nom", personas.THor_Id);
            ViewBag.TImp_Id = new SelectList(db.tipoimpuestos, "TImp_Id", "TImp_nom", personas.TImp_Id);
            return View(personas);
        }

        // GET: personas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            personas personas = db.personas.Find(id);
            if (personas == null)
            {
                return HttpNotFound();
            }
            return View(personas);
        }

        // POST: personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            personas personas = db.personas.Find(id);
            db.personas.Remove(personas);
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
