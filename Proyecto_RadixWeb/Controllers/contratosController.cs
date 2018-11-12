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
using Cloudmersive.APIClient.NET.DocumentAndDataConvert.Api;
using Cloudmersive.APIClient.NET.DocumentAndDataConvert.Client;
using Cloudmersive.APIClient.NET.DocumentAndDataConvert.Model;
using System.Diagnostics;
using System.Text;

namespace Proyecto_RadixWeb.Controllers
{
    public class ContratosController : Controller
    {
        private radixEntities db = new radixEntities();


        public ActionResult VistaParcial(int? subemp_id)
        {

            ViewBag.subemp_id = subemp_id;

            var model = db.contratos.Where(p=>p.Sub_Id==subemp_id).ToList();

            MultiplesClases multiples = new MultiplesClases
            {
                ObjEContrato=model

            };


            return PartialView(model);
        }

       
        public ActionResult verTodos()
        {
            string emp_nom = HttpContext.Session["Empresa"].ToString();

            ViewBag.emp_id = HttpContext.Session["Emp_id"].ToString();
            ViewBag.empresa = emp_nom;

            List<contratos> listarc = db.contratos.ToList();
            ViewBag.contratos = new SelectList(listarc, "Cod_Id", "Sub_Id", "Per_Rut");


            var contratos = db.contratos.Include(s => s.subempresas).Include(s => s.personas).Include(s => s.subempresas.empresas);
            MultiplesClases multiples = new MultiplesClases
            {
                ObjEContrato = contratos.Where(s => s.subempresas.empresas.Emp_Nom == emp_nom).ToList()
            };

            return View(multiples);
        }

        // GET: contratos
        public ActionResult Index(int? subemp_id)
        {
            ViewBag.empresa = HttpContext.Session["Empresa"].ToString();

            ViewBag.subemp_id = subemp_id;
            ViewBag.Car_Id = new SelectList(db.cargos, "Car_Id", "Car_Nom");
            ViewBag.EC_Id = new SelectList(db.estadosciviles, "EC_Id", "EC_Nom");
            ViewBag.Gen_Id = new SelectList(db.generos, "Gen_Id", "Gen_Nom");
            ViewBag.Nac_Id = new SelectList(db.nacionalidades, "Nac_Id", "Nac_Nom");



            var contratos = db.contratos.Include(c => c.personas).Include(c => c.subempresas).Include(c => c.tiposcontratos);
            MultiplesClases multiple = new MultiplesClases
            {
                ObjEContrato = contratos.Where(c => c.Sub_Id == subemp_id).ToList()
            };
            return View(multiple);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Doc_Id,Doc_Nom,Con_id")] documentos documentos, HttpPostedFileBase plantilla,int subemp_id)
        {
            if (plantilla != null && plantilla.ContentLength > 0)
            {
                var length = plantilla.InputStream.Length; 

                

                byte[] datoplantilla = null;
                using (var binarydoc = new BinaryReader(plantilla.InputStream))
                {
                    datoplantilla = binarydoc.ReadBytes(plantilla.ContentLength);
                }
                documentos.Doc_Binario = datoplantilla;
                documentos.Doc_Ext = Path.GetExtension(plantilla.FileName);
            }

            var contrato = db.contratos.Find(documentos.Con_Id);

            if (ModelState.IsValid)
            {
                //planillascontratos.PC_Ext =".docx";
                db.documentos.Add(documentos);

                contrato.Doc_Id = documentos.Doc_Id;
                db.Entry(contrato).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index",new { subemp_id });
            }


            return View(documentos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPersonas(MultiplesClases multiples,personas personas,contratos contratos, int? subemp_id)
        {
            if (ModelState.IsValid)
            {
                multiples.ObjPersonas.Car_Id = personas.Car_Id;
                multiples.ObjPersonas.EC_Id = personas.EC_Id;
                multiples.ObjPersonas.Nac_Id = personas.Nac_Id;
                multiples.ObjPersonas.Gen_Id = personas.Gen_Id;

                db.personas.Add(multiples.ObjPersonas);
                db.SaveChanges();


                contratos.Per_Rut = multiples.ObjPersonas.Per_Rut;

                contratos.Sub_Id =Convert.ToInt32( subemp_id);
                contratos.Con_FechaInicio = DateTime.Now.ToShortDateString();


                db.contratos.Add(contratos);
                db.SaveChanges();

                return RedirectToAction("Index", new { subemp_id });
            }

         
            return View(multiples);
        }


        public FileResult ViewPdf(int? id)
        {
            var archivo = db.documentos.Where(dp => dp.Doc_Id == id).FirstOrDefault();
            // Configure API key authorization: Apikey
            Configuration.Default.AddApiKey("Apikey", "768fa287-07a9-41f9-962d-d1b9356a6b04");
            ConvertDocumentApi apiInstance = new ConvertDocumentApi();
            //convertir un binario a system.io.stream
            MemoryStream stream = new MemoryStream(archivo.Doc_Binario);

            // convertir
            byte[] result = apiInstance.ConvertDocumentDocxToPdf(stream);

            //return File(result, "document/pdf", archivo.PC_Nom + ".pdf");

            ViewBag.nombre = archivo.Doc_Nom + "" + archivo.Doc_Ext;
            return File(result, "application/pdf");

        }

        public ActionResult DescargarDocx(int? id)
        {

            var archivo = db.documentos.Where(p => p.Doc_Id == id).FirstOrDefault();


            return File(archivo.Doc_Binario, "document/docx", archivo.Doc_Nom + ".docx");

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
            ViewBag.Doc_Id = new SelectList(db.planillascontratos, "Doc_Id", "Doc_Nom", contratos.Doc_Id);
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
            ViewBag.Doc_Id = new SelectList(db.planillascontratos, "Doc_Id", "Doc_Nom", contratos.Doc_Id);
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
            ViewBag.Doc_Id = new SelectList(db.planillascontratos, "Doc_Id", "Doc_Nom", contratos.Doc_Id);
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
