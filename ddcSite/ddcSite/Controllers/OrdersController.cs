using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ddcSite.db;
using ddcSite.Models;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using static System.String;

namespace ddcSite.Controllers
{
    [Authorize(Roles = "cli")]
    public class OrdersController : Controller
    {
        private ddcEntities db = new ddcEntities();

        // GET: Orders
        public async Task<ActionResult> Index()
        {
            try
            {
                var orders = db.Orders.Include(o => o.Client);
                return View(await orders.ToListAsync());
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<ActionResult> CreateCase()
        {
            try
            {

                return View();
            }
            catch (Exception)
            {

                throw;
            }

        }

        // GET: Orders/Details/5
        public async Task<ActionResult> Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.IdClient = new SelectList(db.Clients, "ID", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,Price,DateDelivery,Location,Score,DateCreation,IdClient,PatientName,PatientLastName,Coupon,Gender,Age")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdClient = new SelectList(db.Clients, "ID", "Name", order.IdClient);
            return View(order);
        }
        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Creates([Bind(Include = "ID,Name,Price,DateDelivery,Location,Score,DateCreation,IdClient,PatientName,PatientLastName,Coupon,Gender,Age,DetailOrders")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdClient = new SelectList(db.Clients, "ID", "Name", order.IdClient);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<ActionResult> Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdClient = new SelectList(db.Clients, "ID", "Name", order.IdClient);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Price,DateDelivery,Location,Score,DateCreation,IdClient,PatientName,PatientLastName,Coupon,Gender,Age")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdClient = new SelectList(db.Clients, "ID", "Name", order.IdClient);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<ActionResult> Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(decimal id)
        {
            Order order = await db.Orders.FindAsync(id);
            db.Orders.Remove(order);
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

        [HttpPost]
        public async Task<ActionResult> CreateOrderComplete(CompleteOrder orderComplete)
        {
            try
            {


                //if (ModelState.IsValid)
                //{
                Order master = new Order
                {
                    PatientLastName = Empty,
                    Age = orderComplete.OrderMaster.Age > 0 ? orderComplete.OrderMaster.Age : -1,
                    Coupon =
                        !IsNullOrWhiteSpace(orderComplete.OrderMaster.Coupon) ? orderComplete.OrderMaster.Coupon : Empty,
                    DateCreation =
                        !IsNullOrWhiteSpace(orderComplete.OrderMaster.DateCreation)
                            ? orderComplete.OrderMaster.DateCreation
                            : DateTime.Now.Date.ToShortDateString(),
                    DateDelivery = orderComplete.OrderMaster.DateDelivery,
                    Gender =
                        !IsNullOrWhiteSpace(orderComplete.OrderMaster.Gender)
                            ? orderComplete.OrderMaster.Gender
                            : string.Empty,
                    IdClient = orderComplete.OrderMaster.IdClient,
                    Location =
                        !IsNullOrWhiteSpace(orderComplete.OrderMaster.Location)
                            ? orderComplete.OrderMaster.Location
                            : string.Empty,
                    Name =
                        Format("{0}-{1}", orderComplete.OrderMaster.PatientName,
                            orderComplete.OrderMaster.DateCreation),
                    PatientName = orderComplete.OrderMaster.PatientName,
                    Price =
                        IsNullOrWhiteSpace(orderComplete.OrderMaster.Price.ToString()) ? 0 : orderComplete.OrderMaster.Price,
                    PriceTax =
                        IsNullOrWhiteSpace(orderComplete.OrderMaster.PriceTax.ToString())
                            ? 0
                            : orderComplete.OrderMaster.PriceTax,
                    PriceTotal =
                        IsNullOrWhiteSpace(orderComplete.OrderMaster.PriceTotal.ToString())
                            ? 0
                            : orderComplete.OrderMaster.PriceTotal,
                    Score =
                        IsNullOrWhiteSpace(orderComplete.OrderMaster.Score.ToString())
                            ? -1
                            : orderComplete.OrderMaster.Score
                };

                //db.Orders.Add(master);
                //db.SaveChanges();
                foreach (DetailOrder item in orderComplete.OrderDetail)
                {
                    item.IdOrder = master.ID;
                    //item.AbutmentsParallel = IsNullOrWhiteSpace(item.AbutmentsParallel) ? Empty : item.AbutmentsParallel;
                    //item.Amount = IsNullOrWhiteSpace(item.Amount.ToString()) ? 0 : item.Amount;
                    //item.ArticulatorType = IsNullOrWhiteSpace(item.ArticulatorType) ? Empty : item.ArticulatorType;
                    //item.BuccalMargin = IsNullOrWhiteSpace(item.BuccalMargin) ? Empty : item.BuccalMargin;
                    //item.BuccalTissueCompression = IsNullOrWhiteSpace(item.BuccalTissueCompression) ? Empty : item.BuccalTissueCompression;
                    //item.Contact = IsNullOrWhiteSpace(item.Contact) ? Empty : item.Contact;
                    //item.DesignRequirements = IsNullOrWhiteSpace(item.DesignRequirements) ? Empty : item.DesignRequirements;
                    //item.AbutmentsParallel = IsNullOrWhiteSpace(item.AbutmentsParallel) ? Empty : item.AbutmentsParallel;
                    //item.Teeth = IsNullOrWhiteSpace(item.Teeth) ? Empty : item.Teeth;
                    //item.Shade = IsNullOrWhiteSpace(item.Shade) ? Empty : item.Shade;
                    //item.Note = IsNullOrWhiteSpace(item.Note) ? Empty : item.Note;
                    //item.LingualMargin = IsNullOrWhiteSpace(item.LingualMargin) ? Empty : item.LingualMargin;
                    //item.LingualTissueCompression = IsNullOrWhiteSpace(item.LingualTissueCompression) ? Empty : item.LingualTissueCompression;
                    //item.Oclussion = IsNullOrWhiteSpace(item.Oclussion) ? Empty : item.Oclussion;
                    //item.ModelServices = IsNullOrWhiteSpace(item.ModelServices) ? Empty : item.ModelServices;
                    //item.Material = IsNullOrWhiteSpace(item.Material) ? Empty : item.Material;
                    //item.IdName = IsNullOrWhiteSpace(item.IdName) ? Empty : item.IdName;
                    master.DetailOrders.Add(item);
                }
                db.Orders.AddOrUpdate(master);
                db.SaveChanges();

                //foreach (Attachment item in order.ListAtaAttachments)
                //{
                //    Attachment objtAttachment = new Attachment();
                //    objtAttachment.Format = item.Format;
                //    objtAttachment.Name = item.Name;
                //    objtAttachment.NameUpdated = item.NameUpdated;
                //    objtAttachment.SizeFile = item.SizeFile;
                //    objtAttachment.kindFile = item.kindFile;
                //    objtAttachment.IdOrder = Master.ID;
                //    db.Attachments.Add(objtAttachment);
                //}
                //await db.SaveChangesAsync();
                //return RedirectToAction("Index");
                //}

                //ViewBag.IdClient = new SelectList(db.Clients, "ID", "Name", order.OrderMaster.IdClient);
                return View(orderComplete);
            }
            catch (Exception ex)
            {
                var x = ex.Message;
                throw;
            }
        }

    }
}
