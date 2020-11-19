using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeUtilityProject.Models;

namespace HomeUtilityProject.Controllers
{
    public class UserController : Controller
    {
        HOMEUTILITYDATABASEEntities1 db = new HOMEUTILITYDATABASEEntities1();

        public ActionResult Index()
        {
            int tempId = Convert.ToInt32(Session["UserSessionId"]?.ToString());
            List<ORDER_TABLE> Order=db.ORDER_TABLE.Where(x => x.USER_ID == tempId).ToList();
            ORDER_TABLE LastOrder=null;
            if (Order != null)
            {
                try { LastOrder = Order.Last(); }
                catch (Exception ex)
                { }
                
            }
 
            if (LastOrder != null)
            {
                if (LastOrder.WORKING_STATUS == "Requested")
                {
                    if (DateTime.Parse(LastOrder.REQUESTED_TIME).AddMinutes(2) > DateTime.Now)
                    {
                        return RedirectToAction("OrderWaiting", new { id = LastOrder.ID });
                    }

                }
                else if (LastOrder.WORKING_STATUS == "Accepted")
                {
                    if (DateTime.Parse(LastOrder.REQUESTED_TIME).AddMinutes(10) > DateTime.Now)
                    {
                        return RedirectToAction("AcceptedRequested", new { id = LastOrder.ID });
                    }
                }

                else if (LastOrder.WORKING_STATUS == "Reached")
                {
                    return RedirectToAction("WorkingTech", new { Id = LastOrder.ID });
                }




            }


            return View();
        }

    
        
        public ActionResult Services(int id)
        {
            var Promos = db.PROMO_TABLE.ToList();
            ViewBag.PROMO_ID = new SelectList(Promos, "PROMO_ID", "PROMO_NAME");
            int tempId= Convert.ToInt32(Session["UserSessionId"]);
            USER_TABLE user = db.USER_TABLE.Where(x => x.USER_ID.Equals(tempId)).FirstOrDefault();
            List<TECHNICIAN_TABLE> tech = db.TECHNICIAN_TABLE.Where(x => x.SERVICE_ID == id && x.WORKING_STATUS == "Free" && x.LOCATION_ID==user.LOCATION_ID).ToList();
            Random rand = new Random();
            if (tech.Count != 0)
            {
                Session["Shortage"] = "";
                ViewBag.serviceId = id;
                TECHNICIAN_TABLE model = tech[rand.Next(0, tech.Count())];
                SERVICE_TABLE price = db.SERVICE_TABLE.Where(x => x.SERVICE_ID == id).FirstOrDefault();
                Session["PickedTechnician"] = model.TECHNICIAN_ID;
                Session["Wage"] = price.SERVICE_COST - (price.SERVICE_COST * price.DISCOUNT);
                ViewBag.MasterTechId = model.TECHNICIAN_ID;
                return View(model);
            }
            else
            {
                Session["Shortage"] = "No Technician is currently Available At this Moment!!";
                return RedirectToAction("Index");
            }

            
        }
        [HttpGet]
        public ActionResult Order(int id)
        {
            var Promos = db.PROMO_TABLE.ToList();
            ViewBag.PROMO_ID = new SelectList(Promos, "PROMO_ID", "DISCOUNT");
            
            return View();
        }
        [HttpPost]
        public ActionResult Order(ORDER_TABLE model,int id)
        {
            Random rand = new Random();
            var Promos = db.PROMO_TABLE.ToList();
            ViewBag.PROMO_ID = new SelectList(Promos, "PROMO_ID", "DISCOUNT");

            ORDER_TABLE order = new ORDER_TABLE();
            order.TECHNICIAN_ID = Convert.ToInt32(Session["PickedTechnician"]);
            int tempId=order.USER_ID = Convert.ToInt32(Session["UserSessionId"]);
            order.SERVICE_ID = id;
            order.REQUESTED_TIME = DateTime.Now.ToString();
            order.FINISHED_TIME = "Not Finished Yet";
            order.WORKING_STATUS = "Requested";
            order.PROMO_ID = model.PROMO_ID;
            order.CALCULATED_WAGE = Convert.ToInt32(Session["Wage"].ToString()); //800
            order.NET_WAGE = Convert.ToInt32(Session["Wage"].ToString()) - (Convert.ToInt32(Session["Wage"].ToString()) * (db.PROMO_TABLE.Find(model.PROMO_ID).DISCOUNT))/100;
            int myId=order.FLAG_1 = rand.Next(100000, 999999);
            order.FLAG_2 = 0;
            order.FLAG_3 = "0";
            order.FLAG_4 = "0";
            db.ORDER_TABLE.Add(order);
            db.SaveChanges();
            ORDER_TABLE xOrder = db.ORDER_TABLE.Where(x => x.FLAG_1 == myId).FirstOrDefault();
            return RedirectToAction("OrderWaiting",new {id=xOrder.ID });
        }

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
        public ActionResult Edit([Bind(Include = "USER_ID,NAME,EMAIL,PASSWORD,PHONE,LOCATION_ID,ADDRESS,RATINGS,STATUS,FLAG_1,FLAG_2")] USER_TABLE uSER_TABLE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSER_TABLE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details",new { id=uSER_TABLE.USER_ID});
            }
            ViewBag.LOCATION_ID = new SelectList(db.LOCATION_TABLE, "LOCATION_ID", "LOCATION_NAME", uSER_TABLE.LOCATION_ID);
            return View(uSER_TABLE);
        }



        public ActionResult OrderWaiting(int? id)
        {
            int tempId = Convert.ToInt32(Session["UserSessionId"].ToString());
            if (db.ORDER_TABLE.Find(id).WORKING_STATUS == "Accepted")
            {
                return RedirectToAction("AcceptedRequested", new { Id = id });
            }

            ViewBag.WaitingMessage = "Please wait for Sometime!!";
            return View();
        }
        public ActionResult AcceptedRequested(int? id)
        {
            if (db.ORDER_TABLE.Find(id).WORKING_STATUS == "Reached")
            {
                return RedirectToAction("WorkingTech", new { Id = id });
            }

            ViewBag.WaitingMessage = "Coming To Your Home!!!";
            return View();
        }
        public ActionResult WorkingTech(int? id)
        {
            ViewBag.WoreDone = "Is He Done ?";
            ViewBag.serviceId = Convert.ToInt32(id);
            return View();
        }
        public ActionResult ServicePaid(int? id)
        {
            db.ORDER_TABLE.Find(id).WORKING_STATUS = "Paid";
            db.SaveChanges();
            return View();
        }

        public ActionResult Logout()
        {
            Session["UserSession"] = null;
            Session["UserSessionId"] = null;
            Session["LoginSession"] = "Home";
            return RedirectToActionPermanent("Login", "Home");
        }



    }
}