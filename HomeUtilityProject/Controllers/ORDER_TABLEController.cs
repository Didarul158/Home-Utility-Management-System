using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeUtilityProject.Models;

namespace HomeUtilityProject.Controllers
{
    public class ORDER_TABLEController : Controller
    {
        private HOMEUTILITYDATABASEEntities1 db = new HOMEUTILITYDATABASEEntities1();

        // GET: ORDER_TABLE
        public ActionResult Index()
        {
            var oRDER_TABLE = db.ORDER_TABLE.Include(o => o.PROMO_TABLE).Include(o => o.SERVICE_TABLE).Include(o => o.TECHNICIAN_TABLE).Include(o => o.USER_TABLE);
            return View(oRDER_TABLE.ToList());
        }

        // GET: ORDER_TABLE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDER_TABLE oRDER_TABLE = db.ORDER_TABLE.Find(id);
            if (oRDER_TABLE == null)
            {
                return HttpNotFound();
            }
            return View(oRDER_TABLE);
        }

        // GET: ORDER_TABLE/Create
        public ActionResult Create()
        {
            ViewBag.PROMO_ID = new SelectList(db.PROMO_TABLE, "PROMO_ID", "PROMO_NAME");
            ViewBag.SERVICE_ID = new SelectList(db.SERVICE_TABLE, "SERVICE_ID", "SERVICE_NAME");
            ViewBag.TECHNICIAN_ID = new SelectList(db.TECHNICIAN_TABLE, "TECHNICIAN_ID", "NAME");
            ViewBag.USER_ID = new SelectList(db.USER_TABLE, "USER_ID", "NAME");
            return View();
        }

        // POST: ORDER_TABLE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TECHNICIAN_ID,USER_ID,SERVICE_ID,REQUESTED_TIME,FINISHED_TIME,WORKING_STATUS,PROMO_ID,CALCULATED_WAGE,NET_WAGE,FLAG_1,FLAG_2,FLAG_3,FLAG_4")] ORDER_TABLE oRDER_TABLE)
        {
            if (ModelState.IsValid)
            {
                db.ORDER_TABLE.Add(oRDER_TABLE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PROMO_ID = new SelectList(db.PROMO_TABLE, "PROMO_ID", "PROMO_NAME", oRDER_TABLE.PROMO_ID);
            ViewBag.SERVICE_ID = new SelectList(db.SERVICE_TABLE, "SERVICE_ID", "SERVICE_NAME", oRDER_TABLE.SERVICE_ID);
            ViewBag.TECHNICIAN_ID = new SelectList(db.TECHNICIAN_TABLE, "TECHNICIAN_ID", "NAME", oRDER_TABLE.TECHNICIAN_ID);
            ViewBag.USER_ID = new SelectList(db.USER_TABLE, "USER_ID", "NAME", oRDER_TABLE.USER_ID);
            return View(oRDER_TABLE);
        }

        // GET: ORDER_TABLE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDER_TABLE oRDER_TABLE = db.ORDER_TABLE.Find(id);
            if (oRDER_TABLE == null)
            {
                return HttpNotFound();
            }
            ViewBag.PROMO_ID = new SelectList(db.PROMO_TABLE, "PROMO_ID", "PROMO_NAME", oRDER_TABLE.PROMO_ID);
            ViewBag.SERVICE_ID = new SelectList(db.SERVICE_TABLE, "SERVICE_ID", "SERVICE_NAME", oRDER_TABLE.SERVICE_ID);
            ViewBag.TECHNICIAN_ID = new SelectList(db.TECHNICIAN_TABLE, "TECHNICIAN_ID", "NAME", oRDER_TABLE.TECHNICIAN_ID);
            ViewBag.USER_ID = new SelectList(db.USER_TABLE, "USER_ID", "NAME", oRDER_TABLE.USER_ID);
            return View(oRDER_TABLE);
        }

        // POST: ORDER_TABLE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TECHNICIAN_ID,USER_ID,SERVICE_ID,REQUESTED_TIME,FINISHED_TIME,WORKING_STATUS,PROMO_ID,CALCULATED_WAGE,NET_WAGE,FLAG_1,FLAG_2,FLAG_3,FLAG_4")] ORDER_TABLE oRDER_TABLE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oRDER_TABLE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PROMO_ID = new SelectList(db.PROMO_TABLE, "PROMO_ID", "PROMO_NAME", oRDER_TABLE.PROMO_ID);
            ViewBag.SERVICE_ID = new SelectList(db.SERVICE_TABLE, "SERVICE_ID", "SERVICE_NAME", oRDER_TABLE.SERVICE_ID);
            ViewBag.TECHNICIAN_ID = new SelectList(db.TECHNICIAN_TABLE, "TECHNICIAN_ID", "NAME", oRDER_TABLE.TECHNICIAN_ID);
            ViewBag.USER_ID = new SelectList(db.USER_TABLE, "USER_ID", "NAME", oRDER_TABLE.USER_ID);
            return View(oRDER_TABLE);
        }

        // GET: ORDER_TABLE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDER_TABLE oRDER_TABLE = db.ORDER_TABLE.Find(id);
            if (oRDER_TABLE == null)
            {
                return HttpNotFound();
            }
            return View(oRDER_TABLE);
        }

        // POST: ORDER_TABLE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ORDER_TABLE oRDER_TABLE = db.ORDER_TABLE.Find(id);
            db.ORDER_TABLE.Remove(oRDER_TABLE);
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
