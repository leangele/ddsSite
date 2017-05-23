using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ddcSite.db;
using ddcSite.Models;

namespace ddcSite.Controllers
{
    public class DetailOrdersController : Controller
    {
        private ddcEntities db = new ddcEntities();

        // GET: DetailOrders
        public async Task<ActionResult> Index()
        {
            var detailOrders = db.DetailOrders.Include(d => d.Order).Include(d => d.ProductName);
            return View(await detailOrders.ToListAsync());
        }

        // GET: DetailOrders/Details/5
        public async Task<ActionResult> Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailOrder detailOrder = await db.DetailOrders.FindAsync(id);
            if (detailOrder == null)
            {
                return HttpNotFound();
            }
            return View(detailOrder);
        }

        // GET: DetailOrders/Create
        public ActionResult Create()
        {
            ViewBag.IdOrder = new SelectList(db.Orders, "ID", "Name");
            ViewBag.IdProduct = new SelectList(db.Products, "ID", "Name");
            return View();
        }

        // POST: DetailOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,IdOrder,Teeth,BuccalMargin,LingualMargin,BuccalTissue,Compression,AbutmentsParallel,Note,IdProduct,Contact,Oclussion,Material,Shade,DesignRequirements,ModelServices,ArticulatorType,ModelImplantConnection")] DetailOrder detailOrder)
        {
            if (ModelState.IsValid)
            {
                db.DetailOrders.Add(detailOrder);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdOrder = new SelectList(db.Orders, "ID", "Name", detailOrder.IdOrder);
            return View(detailOrder);
        }

        // GET: DetailOrders/Edit/5
        public async Task<ActionResult> Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailOrder detailOrder = await db.DetailOrders.FindAsync(id);
            if (detailOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdOrder = new SelectList(db.Orders, "ID", "Name", detailOrder.IdOrder);
            return View(detailOrder);
        }

        // POST: DetailOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,IdOrder,Teeth,BuccalMargin,LingualMargin,BuccalTissue,Compression,AbutmentsParallel,Note,IdProduct,Contact,Oclussion,Material,Shade,DesignRequirements,ModelServices,ArticulatorType,ModelImplantConnection")] DetailOrder detailOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detailOrder).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdOrder = new SelectList(db.Orders, "ID", "Name", detailOrder.IdOrder);
            return View(detailOrder);
        }

        // GET: DetailOrders/Delete/5
        public async Task<ActionResult> Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailOrder detailOrder = await db.DetailOrders.FindAsync(id);
            if (detailOrder == null)
            {
                return HttpNotFound();
            }
            return View(detailOrder);
        }

        // POST: DetailOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(decimal id)
        {
            DetailOrder detailOrder = await db.DetailOrders.FindAsync(id);
            db.DetailOrders.Remove(detailOrder);
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
