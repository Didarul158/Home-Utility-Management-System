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
    public class TECHNICIAN_TABLEController : Controller
    {
        private HOMEUTILITYDATABASEEntities1 db = new HOMEUTILITYDATABASEEntities1();

        // GET: TECHNICIAN_TABLE
        public ActionResult Index()
        {
            var tECHNICIAN_TABLE = db.TECHNICIAN_TABLE.Include(t => t.LOCATION_TABLE).Include(t => t.SERVICE_TABLE);
            return View(tECHNICIAN_TABLE.ToList());
        }

        // GET: TECHNICIAN_TABLE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TECHNICIAN_TABLE tECHNICIAN_TABLE = db.TECHNICIAN_TABLE.Find(id);
            if (tECHNICIAN_TABLE == null)
            {
                

                return HttpNotFound();
            }
            return View(tECHNICIAN_TABLE);
        }

        // GET: TECHNICIAN_TABLE/Create
        public ActionResult Create()
        {
            ViewBag.LOCATION_ID = new SelectList(db.LOCATION_TABLE, "LOCATION_ID", "LOCATION_NAME");
            ViewBag.SERVICE_ID = new SelectList(db.SERVICE_TABLE, "SERVICE_ID", "SERVICE_NAME");
            return View();
        }

        // POST: TECHNICIAN_TABLE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TECHNICIAN_ID,NAME,EMAIL,PASSWORD,SERVICE_ID,PHONE,RATINGS,WORKING_STATUS,STATUS,LOCATION_ID,FLAG_1,FLAG_2")] TECHNICIAN_TABLE tECHNICIAN_TABLE)
        {
            if (ModelState.IsValid)
            {
                db.TECHNICIAN_TABLE.Add(tECHNICIAN_TABLE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LOCATION_ID = new SelectList(db.LOCATION_TABLE, "LOCATION_ID", "LOCATION_NAME", tECHNICIAN_TABLE.LOCATION_ID);
            ViewBag.SERVICE_ID = new SelectList(db.SERVICE_TABLE, "SERVICE_ID", "SERVICE_NAME", tECHNICIAN_TABLE.SERVICE_ID);
            return View(tECHNICIAN_TABLE);
        }

        // GET: TECHNICIAN_TABLE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TECHNICIAN_TABLE tECHNICIAN_TABLE = db.TECHNICIAN_TABLE.Find(id);
            if (tECHNICIAN_TABLE == null)
            {
                return HttpNotFound();
            }
            ViewBag.LOCATION_ID = new SelectList(db.LOCATION_TABLE, "LOCATION_ID", "LOCATION_NAME", tECHNICIAN_TABLE.LOCATION_ID);
            ViewBag.SERVICE_ID = new SelectList(db.SERVICE_TABLE, "SERVICE_ID", "SERVICE_NAME", tECHNICIAN_TABLE.SERVICE_ID);
            return View(tECHNICIAN_TABLE);
        }

        // POST: TECHNICIAN_TABLE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TECHNICIAN_ID,NAME,EMAIL,PASSWORD,SERVICE_ID,PHONE,RATINGS,WORKING_STATUS,STATUS,LOCATION_ID,FLAG_1,FLAG_2")] TECHNICIAN_TABLE tECHNICIAN_TABLE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tECHNICIAN_TABLE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LOCATION_ID = new SelectList(db.LOCATION_TABLE, "LOCATION_ID", "LOCATION_NAME", tECHNICIAN_TABLE.LOCATION_ID);
            ViewBag.SERVICE_ID = new SelectList(db.SERVICE_TABLE, "SERVICE_ID", "SERVICE_NAME", tECHNICIAN_TABLE.SERVICE_ID);
            return View(tECHNICIAN_TABLE);
        }

        // GET: TECHNICIAN_TABLE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TECHNICIAN_TABLE tECHNICIAN_TABLE = db.TECHNICIAN_TABLE.Find(id);
            if (tECHNICIAN_TABLE == null)
            {
                return HttpNotFound();
            }
            return View(tECHNICIAN_TABLE);
        }

        // POST: TECHNICIAN_TABLE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TECHNICIAN_TABLE tECHNICIAN_TABLE = db.TECHNICIAN_TABLE.Find(id);
            db.TECHNICIAN_TABLE.Remove(tECHNICIAN_TABLE);
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
