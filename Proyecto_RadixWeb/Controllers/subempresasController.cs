using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto_RadixWeb.Models;

namespace Proyecto_RadixWeb.Controllers
{
    public class SubEmpresasController : Controller
    {
        private radixEntities db = new radixEntities();

        public ActionResult MenuSubempresa(string subemp_id)
        {
            ViewBag.subemp_id = subemp_id;
          
            return View();
        }

        public JsonResult GetDatos()
        {

            db.Configuration.ProxyCreationEnabled = false;
            List<empresas> events = db.empresas.ToList();
            
            //var events = db.horario_laboral.Where(h => h.Subempcar_id == Subempcar_id).ToList();

            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public JsonResult GetSubEmpresaDatos()
        {

            db.Configuration.ProxyCreationEnabled = false;

            var lista_empresa = db.empresas.ToList();
            List<CanvasjsSubEmpresa> events=new List<CanvasjsSubEmpresa>();
            foreach (var item in lista_empresa)
            {
                int contar = db.subempresas.Count(s=>s.Emp_Id==item.Emp_Id);

                CanvasjsSubEmpresa canvasjs = new CanvasjsSubEmpresa
                {
                    Emp_nom=item.Emp_Nom,
                    Contar_SubEmpresa=contar

                };

                events.Add(canvasjs);
            }

            //List<empresas> events = db.empresas.ToList();

            //var events = db.horario_laboral.Where(h => h.Subempcar_id == Subempcar_id).ToList();

            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public JsonResult GetEmpresaCargosDatos()
        {

            db.Configuration.ProxyCreationEnabled = false;

            var lista_empresa = db.empresas.ToList();
            List<CanvasjsSubEmpresa> events = new List<CanvasjsSubEmpresa>();
            foreach (var item in lista_empresa)
            {
                int contar = db.empresa_cargo.Count(s => s.Emp_Id == item.Emp_Id);

                CanvasjsSubEmpresa canvasjs = new CanvasjsSubEmpresa
                {
                    Emp_nom = item.Emp_Nom,
                    Contar_SubEmpresa = contar

                };

                events.Add(canvasjs);
            }

            //List<empresas> events = db.empresas.ToList();

            //var events = db.horario_laboral.Where(h => h.Subempcar_id == Subempcar_id).ToList();

            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }




        public ActionResult ListaPlantilla()
        {
            string emp_nom = HttpContext.Session["Empresa"].ToString();
            int empresa_id= Convert.ToInt32(HttpContext.Session["Emp_id"].ToString());
            ViewBag.emp_id = empresa_id;
            ViewBag.empresa = emp_nom;

            var listaPlantilla = db.empresa_cargo.Include(e=>e.empresas).Include(e=>e.cargos).Include(e=>e.planillascontratos);

            listaPlantilla.Where(l=>l.Emp_Id==empresa_id).ToList();

            return View(listaPlantilla);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListaPlantilla([Bind(Include = "PC_Id,PC_Nom")] planillascontratos planillascontratos, HttpPostedFileBase plantilla)
        {
            if (plantilla != null && plantilla.ContentLength > 0)
            {
                var length = plantilla.InputStream.Length;



                byte[] datoplantilla = null;

                using (var binarydoc = new BinaryReader(plantilla.InputStream))
                {
                    datoplantilla = binarydoc.ReadBytes(plantilla.ContentLength);
                }
                planillascontratos.PC_Binario = datoplantilla;
                planillascontratos.PC_Ext = Path.GetExtension(plantilla.FileName);
            }

            

            if (ModelState.IsValid)
            {
                //planillascontratos.PC_Ext =".docx";
                db.planillascontratos.Add(planillascontratos);

               
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(planillascontratos);
        }

        // GET: subempresas
        public ActionResult Index()
        {

        
            string emp_nom = HttpContext.Session["Empresa"].ToString();
            ViewBag.empresa = emp_nom;
            string id2 = HttpContext.Session["Emp_id"].ToString();
            int id = Convert.ToInt32(id2);

            int contar = db.subempresas.Count(s => s.Emp_Id == id);
            ViewBag.contar = contar;

            List<regiones> listaregiones = db.regiones.ToList();
            ViewBag.regiones = new SelectList(listaregiones, "Reg_Id", "Reg_Nom");

            
            var subempresas = db.subempresas.Include(s => s.comunas).Include(s => s.empresas);
            MultiplesClases multiples = new MultiplesClases
            {
                ObjESubEmpresas= subempresas.Where(s => s.empresas.Emp_Nom == emp_nom).ToList()
            };

            return View(multiples);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Sub_Id,Sub_Nom,Sub_Dir,Com_Id")]  subempresas subempresas)
        {
            if (ModelState.IsValid)
            {
                string emp_id = HttpContext.Session["Emp_id"].ToString();
                string empresa = HttpContext.Session["Empresa"].ToString();
                ViewBag.emp_id = emp_id;

                var lista_empresa = db.subempresas.ToList();
                foreach (var item in lista_empresa)
                {
                    int contar = db.subempresas.Count(s => s.Emp_Id == item.Emp_Id);
                    ViewBag.contar = contar;
                }

                subempresas.Emp_Id = Convert.ToInt32(emp_id);
                db.subempresas.Add(subempresas);
                db.SaveChanges();
                return RedirectToAction("Index");
                //return RedirectToAction("Index", "SubEmpresas", new { emp_nom = empresa, emp_id });
            }

            //ViewBag.Com_Id = new SelectList(db.comunas, "Com_Id", "Com_Nom", subempresas.Com_Id);
            //ViewBag.Emp_Id = new SelectList(db.empresas, "Emp_Id", "Emp_Nom", subempresas.Emp_Id);
            return View(subempresas);
        }
        public ActionResult VerSuc()
        {

            string emp_nom = HttpContext.Session["Empresa"].ToString();

            ViewBag.emp_id = HttpContext.Session["Emp_id"].ToString();
            ViewBag.empresa = emp_nom;

            List<regiones> listaregiones = db.regiones.ToList();
            ViewBag.regiones = new SelectList(listaregiones, "Reg_Id", "Reg_Nom");


            var subempresas = db.subempresas.Include(s => s.comunas).Include(s => s.empresas);
            MultiplesClases multiples = new MultiplesClases
            {
                ObjESubEmpresas = subempresas.Where(s => s.empresas.Emp_Nom == emp_nom).ToList()
            };

            return View(multiples);
        }

        // GET: subempresas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subempresas subempresas = db.subempresas.Find(id);
            if (subempresas == null)
            {
                return HttpNotFound();
            }
            return View(subempresas);
        }

        public ActionResult Agregar()
        {

            List<regiones> listaregiones = db.regiones.ToList();
            ViewBag.regiones = new SelectList(listaregiones, "Reg_Id", "Reg_Nom");



            return PartialView();
        }

        


        // GET: subempresas/Create
        public ActionResult Create()
        {

            List<regiones> listaregiones = db.regiones.ToList();
            ViewBag.regiones = new SelectList(listaregiones, "Reg_Id", "Reg_Nom");


            //ViewBag.Com_Id = new SelectList(db.comunas, "Com_Id", "Com_Nom");

        
            ViewBag.empresa = HttpContext.Session["Empresa"].ToString();

            //ViewBag.Emp_Id = new SelectList(db.empresas, "Emp_Id", "Emp_Nom");
            return View();
        }

        // POST: subempresas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sub_Id,Sub_Nom,Sub_Cant,Sub_Estado,Sub_Dir,Com_Id")] subempresas subempresas)
        {
            if (ModelState.IsValid)
            {
                string emp_id = HttpContext.Session["Emp_id"].ToString();
                string empresa = HttpContext.Session["Empresa"].ToString();

                subempresas.Emp_Id = Convert.ToInt32(emp_id);
                db.subempresas.Add(subempresas);
                db.SaveChanges();
                return RedirectToAction("Index", "SubEmpresas", new { emp_nom = empresa, emp_id });
            }

            //ViewBag.Com_Id = new SelectList(db.comunas, "Com_Id", "Com_Nom", subempresas.Com_Id);
            //ViewBag.Emp_Id = new SelectList(db.empresas, "Emp_Id", "Emp_Nom", subempresas.Emp_Id);
            return View(subempresas);
        }



        public JsonResult ObtenerProvincia(int Reg_Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<provincias> listaprovincia = db.provincias.Where(p => p.Reg_Id == Reg_Id).OrderBy(pp => pp.Prov_Nom).ToList();

            return Json(listaprovincia, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ObtenerComuna(int Prov_Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<comunas> listacomuna = db.comunas.Where(p => p.Prov_Id == Prov_Id).OrderBy(pp => pp.Com_Nom).ToList();

            return Json(listacomuna, JsonRequestBehavior.AllowGet);

        }


        // GET: subempresas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subempresas subempresas = db.subempresas.Find(id);
            if (subempresas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Com_Id = new SelectList(db.comunas, "Com_Id", "Com_Nom", subempresas.Com_Id);
            ViewBag.Emp_Id = new SelectList(db.empresas, "Emp_Id", "Emp_Nom", subempresas.Emp_Id);
            return View(subempresas);
        }

        // POST: subempresas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sub_Id,Sub_Nom,Sub_Cant,Emp_Id,Sub_Estado,Sub_Dir,Com_Id")] subempresas subempresas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subempresas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Com_Id = new SelectList(db.comunas, "Com_Id", "Com_Nom", subempresas.Com_Id);
            ViewBag.Emp_Id = new SelectList(db.empresas, "Emp_Id", "Emp_Nom", subempresas.Emp_Id);
            return View(subempresas);
        }

        // GET: subempresas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subempresas subempresas = db.subempresas.Find(id);
            if (subempresas == null)
            {
                return HttpNotFound();
            }
            return View(subempresas);
        }

        // POST: subempresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            subempresas subempresas = db.subempresas.Find(id);
            db.subempresas.Remove(subempresas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Eliminar(subempresas sub)
        {
            int? id2 = sub.Sub_Id;
            int id = Convert.ToInt32(id2);
            //elminar sectores
            var sectores = db.Sectores.Where(s=>s.Sub_Id==id);

            foreach (var item3 in sectores)
            {
                //elminar cuarteles

                var cuarteles = db.Cuarteles.Where(c => c.sect_id == item3.sect_id);

                foreach (var item in cuarteles)
                {
                    //elminar grupos
                    var grupos = db.GruposCuarteles.Where(gc => gc.cuar_id == item.cuar_id);


                    if (grupos != null)
                    {

                        foreach (var item2 in grupos)
                        {
                            GruposCuarteles gc = db.GruposCuarteles.Find(item2.gc_id);
                            db.GruposCuarteles.Remove(gc);
                        }

                    }

                    Cuarteles ca = db.Cuarteles.Find(item.cuar_id);
                    db.Cuarteles.Remove(ca);
                }
                Sectores se = db.Sectores.Find(item3.sect_id);
                db.Sectores.Remove(se);
            }
            db.SaveChanges();
            //elminar cargos
            var car = db.subempresa_cargo.Where(s => s.Sub_Id == id);

            foreach (var item4 in car)
            {
                subempresa_cargo sc = db.subempresa_cargo.Find(item4.Subempcar_id);
                db.subempresa_cargo.Remove(sc);
            }
            db.SaveChanges();
            //elminar personas
            var per = db.contratos.Where(s => s.Sub_Id == id);

            foreach (var item5 in per)
            {
                contratos con = db.contratos.Find(item5.Con_Id);
                db.contratos.Remove(con);
            }
            db.SaveChanges();

            subempresas sube = db.subempresas.Find(id);
            db.subempresas.Remove(sube);
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
