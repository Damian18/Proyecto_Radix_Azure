using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using Ionic.Zip;
using Proyecto_RadixWeb.Models;

namespace Proyecto_RadixWeb.Controllers
{
    public class SolicitudDetalleQrsController : Controller
    {
        private radixEntities db = new radixEntities();

        // GET: SolicitudDetalleQrs
        public ActionResult Index(string sol_id)
        {
            ViewBag.sol_id = sol_id;
            int id= Convert.ToInt32(sol_id);

            var solicitudDetalleQr = db.SolicitudDetalleQr.Include(s => s.contratos).Include(s => s.SolicitudesQr);
            return View(solicitudDetalleQr.Where(s=>s.sqr_id==id).ToList());
        }

        [HttpPost]
        public ActionResult DescargarQr(string sqr_id)
        {
            try
            {
                int id = Convert.ToInt32(sqr_id);

                var Solicitud = db.SolicitudesQr.FirstOrDefault(s=>s.sqr_id==id);

                string subempresa_nombre = Solicitud.subempresas.Sub_Nom;

                var listaSolicitudes = db.SolicitudDetalleQr.Where(s=>s.sqr_id==id);

                
                List<SolicitudDetalleQr> listadetalle = new List<SolicitudDetalleQr>();
                foreach (var item in listaSolicitudes)
                {
                    var con = GenerarQr(item.contratos.Per_Rut);
                    item.formato = BitmapToBytes(con);

                    listadetalle.Add(new SolicitudDetalleQr
                    {   formato =BitmapToBytes(con),
                        nombre =item.contratos.personas.Per_Nom+""+item.contratos.personas.Per_ApePat });

                }
  

                var outputStream = new MemoryStream();


                using (var zip = new ZipFile())
                {
                    foreach (var item in listadetalle)
                    {
                        zip.AddEntry(item.nombre + ".png", item.formato.ToArray());



                        //  zip.AddEntry("qr.png", conver1.ToArray());
                        //  zip.AddEntry("qr2.png", conver2.ToArray());

                    }
                    zip.Save(outputStream);
                }
                outputStream.Position = 0;
                return File(outputStream, "application/zip", subempresa_nombre+".zip");


            }
            catch (Exception e)
            {


            }


            return View();
        }

        private Bitmap GenerarQr(string texto)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(texto, out qrCode);
            GraphicsRenderer renderer = new GraphicsRenderer(new FixedCodeSize(400, QuietZoneModules.Zero), Brushes.Black, Brushes.White);
            MemoryStream ms = new MemoryStream();
            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            var imageTemporal = new Bitmap(ms);
            var image = new Bitmap(imageTemporal, new Size(new Point(200, 200)));

            return image;
        }


        private byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }

        // GET: SolicitudDetalleQrs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudDetalleQr solicitudDetalleQr = db.SolicitudDetalleQr.Find(id);
            if (solicitudDetalleQr == null)
            {
                return HttpNotFound();
            }
            return View(solicitudDetalleQr);
        }

        // GET: SolicitudDetalleQrs/Create
        public ActionResult Create()
        {
            ViewBag.Con_id = new SelectList(db.contratos, "Con_Id", "Per_Rut");
            ViewBag.sqr_id = new SelectList(db.SolicitudesQr, "sqr_id", "sqr_id");
            return View();
        }

        // POST: SolicitudDetalleQrs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sdqr_id,sqr_id,Con_id")] SolicitudDetalleQr solicitudDetalleQr)
        {
            if (ModelState.IsValid)
            {
                db.SolicitudDetalleQr.Add(solicitudDetalleQr);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Con_id = new SelectList(db.contratos, "Con_Id", "Per_Rut", solicitudDetalleQr.Con_id);
            ViewBag.sqr_id = new SelectList(db.SolicitudesQr, "sqr_id", "sqr_id", solicitudDetalleQr.sqr_id);
            return View(solicitudDetalleQr);
        }

        // GET: SolicitudDetalleQrs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudDetalleQr solicitudDetalleQr = db.SolicitudDetalleQr.Find(id);
            if (solicitudDetalleQr == null)
            {
                return HttpNotFound();
            }
            ViewBag.Con_id = new SelectList(db.contratos, "Con_Id", "Per_Rut", solicitudDetalleQr.Con_id);
            ViewBag.sqr_id = new SelectList(db.SolicitudesQr, "sqr_id", "sqr_id", solicitudDetalleQr.sqr_id);
            return View(solicitudDetalleQr);
        }

        // POST: SolicitudDetalleQrs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sdqr_id,sqr_id,Con_id")] SolicitudDetalleQr solicitudDetalleQr)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitudDetalleQr).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Con_id = new SelectList(db.contratos, "Con_Id", "Per_Rut", solicitudDetalleQr.Con_id);
            ViewBag.sqr_id = new SelectList(db.SolicitudesQr, "sqr_id", "sqr_id", solicitudDetalleQr.sqr_id);
            return View(solicitudDetalleQr);
        }

        // GET: SolicitudDetalleQrs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudDetalleQr solicitudDetalleQr = db.SolicitudDetalleQr.Find(id);
            if (solicitudDetalleQr == null)
            {
                return HttpNotFound();
            }
            return View(solicitudDetalleQr);
        }

        // POST: SolicitudDetalleQrs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SolicitudDetalleQr solicitudDetalleQr = db.SolicitudDetalleQr.Find(id);
            db.SolicitudDetalleQr.Remove(solicitudDetalleQr);
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
