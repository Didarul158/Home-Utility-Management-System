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
    public class USER_TABLEController : Controller
    {
        private HOMEUTILITYDATABASEEntities1 db = new HOMEUTILITYDATABASEEntities1();

        // GET: USER_TABLE
        public ActionResult Index()
        {
            var uSER_TABLE = db.USER_TABLE.Include(u => u.LOCATION_TABLE);
            return View(uSER_TABLE.ToList());
        }

        // GET: USER_TABLE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_TABLE uSER_TABLE = db.USER_TABLE.Find(id);
            if (uSER_TABLE == null)
            {
                return HttpNotFound();
            }
            return View(uSER_TABLE);
        }

        // GET: USER_TABLE/Create
        public ActionResult Create()
        {
            ViewBag.LOCATION_ID = new SelectList(db.LOCATION_TABLE, "LOCATION_ID", "LOCATION_NAME");
            return View();
        }

        // POST: USER_TABLE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "USER_ID,NAME,EMAIL,PASSWORD,PHONE,LOCATION_ID,ADDRESS,RATINGS,STATUS,FLAG_1,FLAG_2")] USER_TABLE uSER_TABLE)
        {
            if (ModelState.IsValid)
            {
                db.USER_TABLE.Add(uSER_TABLE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LOCATION_ID = new SelectList(db.LOCATION_TABLE, "LOCATION_ID", "LOCATION_NAME", uSER_TABLE.LOCATION_ID);
            return View(uSER_TABLE);
        }

        // GET: USER_TABLE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_TABLE uSER_TABLE = db.USER_TABLE.Find(id);
            if (uSER_TABLE == null)
            {
                return HttpNotFound();
            }
            ViewBag.LOCATION_ID = new SelectList(db.LOCATION_TABLE, "LOCATION_ID", "LOCATION_NAME", uSER_TABLE.LOCATION_ID);
            return View(uSER_TABLE);
        }

        // POST: USER_TABLE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "USER_ID,NAME,EMAIL,PASSWORD,PHONE,LOCATION_ID,ADDRESS,RATINGS,STATUS,FLAG_1,FLAG_2")] USER_TABLE uSER_TABLE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSER_TABLE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LOCATION_ID = new SelectList(db.LOCATION_TABLE, "LOCATION_ID", "LOCATION_NAME", uSER_TABLE.LOCATION_ID);
            return View(uSER_TABLE);
        }

        // GET: USER_TABLE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_TABLE uSER_TABLE = db.USER_TABLE.Find(id);
            if (uSER_TABLE == null)
            {
                return HttpNotFound();
            }
            return View(uSER_TABLE);
        }

        // POST: USER_TABLE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            USER_TABLE uSER_TABLE = db.USER_TABLE.Find(id);
            db.USER_TABLE.Remove(uSER_TABLE);
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
