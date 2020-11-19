using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeUtilityProject.Models;

namespace HomeUtilityProject.Controllers
{
    public class TechnicianController : Controller
    {
        HOMEUTILITYDATABASEEntities1 db = new HOMEUTILITYDATABASEEntities1();
        // GET: Technician
        public ActionResult Index(int? id)
        {
            if (id == 1)
            {
                ViewBag.ses = "Payment Done !!!";
            }

            int tempId = Convert.ToInt32(Session["TechnicianSessionId"].ToString());

            List<ORDER_TABLE> Order = db.ORDER_TABLE.Where(x => x.TECHNICIAN_ID == tempId).ToList();
            ORDER_TABLE LastOrder = null;
            if (Order != null)
            {
                try { LastOrder = Order.LastOrDefault(); }
                catch (Exception ex)
                { }
            }
            if (LastOrder != null)
            {
                
                if (LastOrder.WORKING_STATUS == "Accepted")
                {
                    if (DateTime.Parse(LastOrder.REQUESTED_TIME).AddMinutes(10) > DateTime.Now)
                    {
                        return RedirectToAction("OnMyWayTechnician", new { Id = LastOrder.ID });
                    }
                }

                else if (LastOrder.WORKING_STATUS == "Reached")
                {
                    return RedirectToAction("DoneWork", new { Id = LastOrder.ID });
                }



            }




            var Orders = new List<ORDER_TABLE>();
            Orders = db.ORDER_TABLE.Where(x => x.TECHNICIAN_ID == tempId && x.WORKING_STATUS == "Requested").ToList();
            var FilteredOrder = new List<ORDER_TABLE>();
            foreach (var order in Orders)
            {
                if (DateTime.Parse(order.REQUESTED_TIME).AddMinutes(8) > DateTime.Now)
                {
                    FilteredOrder.Add(order);
                }
            }
            return View(FilteredOrder);
        }

        public ActionResult Accept(int id)
        {
            db.ORDER_TABLE.Find(id).WORKING_STATUS = "Accepted";
            Session["OnService"] = "OnMyWay";
            Session["ServedUserid"] = db.ORDER_TABLE.Find(id).USER_ID.ToString();
            Session["ServedTechnicianid"] = db.ORDER_TABLE.Find(id).TECHNICIAN_ID.ToString();
            db.SaveChanges();
            return RedirectToAction("OnMyWayTechnician",new { Id=id});         
        }
        public ActionResult Decline(int id)
        {
            db.ORDER_TABLE.Find(id).WORKING_STATUS = "Declined";
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult OnMyWayTechnician(int? id)
        {
            ViewBag.OnMyWay = "Taratrai Jau !!!";
            Session["Tesst"] = Convert.ToInt32(id);
            return View();
        }

        public ActionResult Reached(int? id)
        {
            ViewBag.ReachedHome = "Kaj Kortesi!!";
            db.ORDER_TABLE.Find(id).WORKING_STATUS = "Reached";
            db.SaveChanges();
            Session["Tesst1"] = Convert.ToInt32(id);

            return View();
        }
        public ActionResult DoneWork(int? id)
        {
            ViewBag.Welcome = "Congratulation!!!\n Waiting for payment !!";
            if (db.ORDER_TABLE.Find(id).WORKING_STATUS == "Paid")
            {
                return RedirectToAction("Index", new { id = 1 });
            }

            return View();
        }
        

        public ActionResult Logout()
        {
            Session["TechnicianSession"] = null;
            Session["TechnicianSessionId"] = null;
            Session["LoginSession"] = "Home";
            return RedirectToActionPermanent("Login", "Home");
        }


    }
}