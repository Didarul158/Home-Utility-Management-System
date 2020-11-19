using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeUtilityProject.Models;

namespace HomeUtilityProject.Controllers
{
    public class AdminController : Controller
    {
        HOMEUTILITYDATABASEEntities1 db = new HOMEUTILITYDATABASEEntities1();
        // GET: Admin
        public ActionResult Index()
        {
            int gass = db.ORDER_TABLE.Where(x => x.SERVICE_ID == 1).Count();
            int electric = db.ORDER_TABLE.Where(x => x.SERVICE_ID == 3).Count();
            int water = db.ORDER_TABLE.Where(x => x.SERVICE_ID == 2).Count();

            Session["GassServiceServed"] = 70;
            Session["ElectricalServiceServed"] = 35;
            Session["WaterServiceServed"] = 57;


            return View();
        }

        public ActionResult TechList(int? id)
        {
            var TechList = db.TECHNICIAN_TABLE.Where(x => x.SERVICE_ID == id);
            return View(TechList.ToList());
        }

        public ActionResult TechnicianDetails(int? id)
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
            var Orders = new List<ORDER_TABLE>();
            Orders = db.ORDER_TABLE.Where(x => x.TECHNICIAN_ID == id && x.WORKING_STATUS == "Paid").ToList();
            var ServedInJanuary = new List<ORDER_TABLE>();
            var ServedInFebruary = new List<ORDER_TABLE>();
            var ServedInMarch = new List<ORDER_TABLE>();
            var ServedInApril= new List<ORDER_TABLE>();
            var ServedInMay = new List<ORDER_TABLE>();
            var ServedInJune = new List<ORDER_TABLE>();
            var ServedInJuly = new List<ORDER_TABLE>();
            var ServedInAugust = new List<ORDER_TABLE>();
            var ServedInSeptember = new List<ORDER_TABLE>();
            var ServedInOctober = new List<ORDER_TABLE>();
            var ServedInNovember = new List<ORDER_TABLE>();

            foreach (var order in Orders)
            {
                if (DateTime.Parse(order.REQUESTED_TIME).Year == DateTime.Now.Year)
                {
                    if (DateTime.Parse(order.REQUESTED_TIME).Month == 1)
                    {
                        ServedInJanuary.Add(order);
                    }
                    if (DateTime.Parse(order.REQUESTED_TIME).Month == 2)
                    {
                        ServedInFebruary.Add(order);
                    }
                    if (DateTime.Parse(order.REQUESTED_TIME).Month == 3)
                    {
                        ServedInMarch.Add(order);
                    }
                    if (DateTime.Parse(order.REQUESTED_TIME).Month == 4)
                    {
                        ServedInApril.Add(order);
                    }
                    if (DateTime.Parse(order.REQUESTED_TIME).Month == 5)
                    {
                        ServedInMay.Add(order);
                    }
                    if (DateTime.Parse(order.REQUESTED_TIME).Month == 6)
                    {
                        ServedInJune.Add(order);
                    }
                    if (DateTime.Parse(order.REQUESTED_TIME).Month == 7)
                    {
                        ServedInJuly.Add(order);
                    }
                    if (DateTime.Parse(order.REQUESTED_TIME).Month == 8)
                    {
                        ServedInAugust.Add(order);
                    }
                    if (DateTime.Parse(order.REQUESTED_TIME).Month == 9)
                    {
                        ServedInSeptember.Add(order);
                    }
                    if (DateTime.Parse(order.REQUESTED_TIME).Month == 10)
                    {
                        ServedInOctober.Add(order);
                    }
                    if (DateTime.Parse(order.REQUESTED_TIME).Month == 11)
                    {
                        ServedInNovember.Add(order);
                    }                   
                }              
            }
            Session["ServedInJanuary"] = ServedInJanuary.Count();
            Session["ServedInFebruary"] = ServedInFebruary.Count();
            Session["ServedInMarch"] =ServedInMarch.Count();
            Session["ServedInApril"] = ServedInApril.Count();
            Session["ServedInMay"] = ServedInMay.Count();
            Session["ServedInJune"] = ServedInJune.Count();
            Session["ServedInJuly"] = ServedInJuly.Count();
            Session["ServedInAugust"] = ServedInAugust.Count();
            Session["ServedInSeptember"] = ServedInSeptember.Count();
            Session["ServedInOctober"] = ServedInOctober.Count();
            Session["ServedInNovember"] = ServedInNovember.Count();

            return View(tECHNICIAN_TABLE);
        }

        
        public ActionResult UserList()
        {
            var UserList = db.USER_TABLE.ToList();
            return View(UserList);
        }

        public ActionResult UserDetails(int? id)
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

            var Orders = new List<ORDER_TABLE>();
            Orders = db.ORDER_TABLE.Where(x => x.USER_ID == id && x.WORKING_STATUS == "Paid").ToList();
            var GassService = new List<ORDER_TABLE>();
            var ElectricService = new List<ORDER_TABLE>();
            var WaterService = new List<ORDER_TABLE>();

            foreach (var order in Orders)
            {
                if (DateTime.Parse(order.REQUESTED_TIME).Year == DateTime.Now.Year)
                {
                    if (order.SERVICE_ID==1)
                    {
                        GassService.Add(order);
                    }
                    if (order.SERVICE_ID == 3)
                    {
                        ElectricService.Add(order);
                    }
                    if (order.SERVICE_ID == 2)
                    {
                        WaterService.Add(order);
                    }
                    

                }


            }

            Session["GassService"] = GassService.Count();
            Session["ElectricService"] = ElectricService.Count();
            Session["WaterService"] = WaterService.Count();


            return View(uSER_TABLE);
        }


        public ActionResult YearlyReport()
        {

            return View();
        }

        public ActionResult ReceivedReport()
        {

            return View();
        }


        public ActionResult ServicesPie()
        {
            
            return View();
        }
        public ActionResult Logout()
        {
            Session["AdminSession"] = null;
            Session["LoginSession"] = "Home";
            return View();
        }

        



    }
}