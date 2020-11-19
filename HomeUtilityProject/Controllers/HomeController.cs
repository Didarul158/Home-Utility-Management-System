using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeUtilityProject.Models;

namespace HomeUtilityProject.Controllers
{
    public class HomeController : Controller
    {
        HOMEUTILITYDATABASEEntities1 db = new HOMEUTILITYDATABASEEntities1();
        public ActionResult Index()
        {
            Session["LoginSession"] = "Home";
            ViewBag.GassCount = db.TECHNICIAN_TABLE.Where(x => x.SERVICE_ID == 1).Count();
            ViewBag.ElectricCount = db.TECHNICIAN_TABLE.Where(x => x.SERVICE_ID == 3).Count();
            ViewBag.WaterCount = db.TECHNICIAN_TABLE.Where(x => x.SERVICE_ID == 2).Count();
            return View();
        }


        [HttpGet]
        public ActionResult UserSignup()
        {
            var Areas = db.LOCATION_TABLE.ToList();
            ViewBag.LOCATION_ID = new SelectList(Areas,"LOCATION_ID","LOCATION_NAME");

            return View();
        }
        [HttpPost]
        public ActionResult UserSignup(USER_TABLE model)
        {
            var Areas = db.LOCATION_TABLE.ToList();
            ViewBag.LOCATION_ID = new SelectList(Areas, "LOCATION_ID", "LOCATION_NAME");

            var test = db.USED_EMAIL_TABLE.Where(x => x.EMAIL.Equals(model.EMAIL)).SingleOrDefault();
            if (test == null)
            {
                USER_TABLE user = new USER_TABLE();
                user.NAME = model.NAME;
                user.EMAIL = model.EMAIL;
                user.PASSWORD = model.PASSWORD;
                user.PHONE = model.PHONE;
                user.LOCATION_ID = model.LOCATION_ID;
                user.ADDRESS = model.ADDRESS;
                user.RATINGS = "0";
                user.STATUS = "Active";
                user.FLAG_1 = 0;
                user.FLAG_2 = "0";
                db.USER_TABLE.Add(user);
                db.SaveChanges();

                LOGIN_TABLE login = new LOGIN_TABLE();
                login.EMAIL = model.EMAIL;
                login.PASSWORD = model.PASSWORD;
                login.TYPE = "USER";
                login.FLAG_1 = 0;
                login.FLAG_2 = "0";
                db.LOGIN_TABLE.Add(login);
                db.SaveChanges();

                USED_EMAIL_TABLE email = new USED_EMAIL_TABLE();
                email.EMAIL = model.EMAIL;
                email.FLAG_1 = "0";
                email.FLAG_2 = "0";
                db.USED_EMAIL_TABLE.Add(email);
                db.SaveChanges();
                USER_TABLE sessionObject = db.USER_TABLE.Where(x => x.EMAIL.Equals(model.EMAIL)).FirstOrDefault();
                Session["USER_ID"] = sessionObject.USER_ID;
            }

            else
            {
                ViewBag.UsedMailAlert = "Saaala!";
            }
            


            return View();
        }

        [HttpGet]
        public ActionResult TechnicianSignup()
        {
            var Areas = db.LOCATION_TABLE.ToList();
            ViewBag.LOCATION_ID = new SelectList(Areas, "LOCATION_ID", "LOCATION_NAME");

            var Services = db.SERVICE_TABLE.ToList();
            ViewBag.SERVICE_ID = new SelectList(Services, "SERVICE_ID","SERVICE_NAME");

            return View();
        }
        [HttpPost]
        public ActionResult TechnicianSignup(TECHNICIAN_TABLE model)
        {

            var Areas = db.LOCATION_TABLE.ToList();
            ViewBag.LOCATION_ID = new SelectList(Areas, "LOCATION_ID", "LOCATION_NAME");

            var Services = db.SERVICE_TABLE.ToList();
            ViewBag.SERVICE_ID = new SelectList(Services, "SERVICE_ID", "SERVICE_NAME");


            var test = db.USED_EMAIL_TABLE.Where(x => x.EMAIL.Equals(model.EMAIL)).SingleOrDefault();
            if (test == null)
            {
                TECHNICIAN_TABLE tech = new TECHNICIAN_TABLE();
            tech.NAME = model.NAME;
            tech.EMAIL = model.EMAIL;
            tech.PASSWORD = model.PASSWORD;
            tech.SERVICE_ID = model.SERVICE_ID;
            tech.PHONE = model.PHONE;
            tech.RATINGS = "0";
            tech.WORKING_STATUS = "Free";
            tech.STATUS = "Active";
            tech.LOCATION_ID = model.LOCATION_ID;
            tech.FLAG_1 = 0;
            tech.FLAG_2 = "0";
            db.TECHNICIAN_TABLE.Add(tech);
            db.SaveChanges();

            LOGIN_TABLE login = new LOGIN_TABLE();
            login.EMAIL = model.EMAIL;
            login.PASSWORD = model.PASSWORD;
            login.TYPE = "TECHNICIAN";
            login.FLAG_1 = 0;
            login.FLAG_2 = "0";
            db.LOGIN_TABLE.Add(login);
            db.SaveChanges();

            USED_EMAIL_TABLE email = new USED_EMAIL_TABLE();
            email.EMAIL = model.EMAIL;
            email.FLAG_1 = "0";
            email.FLAG_2 = "0";
            db.USED_EMAIL_TABLE.Add(email);
            db.SaveChanges();

            }

            else
            {
                ViewBag.UsedMailAlert = "Saaala!";
            }


            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            Session["LoginSession"] = "Home";
            return View();
        }
        [HttpPost]
        public ActionResult Login(LOGIN_TABLE model)
        {
            LOGIN_TABLE login = db.LOGIN_TABLE.Where(x => x.EMAIL.Equals(model.EMAIL) && x.PASSWORD.Equals(model.PASSWORD)).FirstOrDefault();
            if (login != null)
            {
                if (login.TYPE == "USER")
                {
                    USER_TABLE LoginUser = db.USER_TABLE.Where(x => x.EMAIL.Equals(model.EMAIL)).FirstOrDefault();
                    Session["UserSession"] = login.EMAIL.ToString();
                    Session["UserSessionId"] = LoginUser.USER_ID.ToString();
                    Session["LoginSession"] = "User";
                    return RedirectToAction("Index", "User");
                }
                else if (login.TYPE == "TECHNICIAN")
                {
                    TECHNICIAN_TABLE LoginTech = db.TECHNICIAN_TABLE.Where(x => x.EMAIL.Equals(model.EMAIL)).FirstOrDefault();
                    Session["TechnicianSession"] = login.EMAIL.ToString();
                    Session["TechnicianSessionId"] = LoginTech.TECHNICIAN_ID;
                    Session["LoginSession"] = "Technician";
                    return RedirectToAction("Index", "Technician");
                }
                else if (login.TYPE == "ADMIN")
                {
                    Session["AdminSession"] = login.EMAIL.ToString();
                    Session["LoginSession"] = "Admin";
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.typeWarning = "User Not Found!";
                }
               
            }

            else
            {
                ViewBag.typeWarning = "User Not Found!";
            }

            return View();
        }


    }
}