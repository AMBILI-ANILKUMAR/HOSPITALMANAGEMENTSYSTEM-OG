using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using HOSPITALMANAGEMENTSYSTEM.Models;
namespace HOSPITALMANAGEMENTSYSTEM.Controllers
{
    public class HomePageController : Controller
    {
        DoctorOperations dop = new DoctorOperations();
        PatientOperations pop = new PatientOperations();

        // GET: HomePage
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult HomePage()
        {
            return View();
        }
        public ActionResult AboutPage()
        {
            return View();
        }
        public ActionResult FacilityPage()
        {
            return View();
        }
        
        public ActionResult Login()
        {
            return View();
        }
        
        
        public ActionResult About()
        {
            return View();
        }
        
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(Login l)
        {
            if (l.Username == "admin" && l.Password == "admin")
            {
                return RedirectToAction("AdminHome", "Admin");
            }
            else
            {
                ViewBag.info = "Please Check the credentials";
            }
            return View();
        }
        public ActionResult DoctorLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoctorLogin(Login l)
        {
            DataSet ds = dop.logincheck(l.Username, l.Password);
            if ((ds.Tables["doc"].Rows.Count == 1))
            {

                Session["name"] = ds.Tables["doc"].Rows[0]["DoctName"].ToString();
                Session["Email"] = ds.Tables["doc"].Rows[0]["email"].ToString();
                Session["Password"] = ds.Tables["doc"].Rows[0]["password"].ToString();
                Session["id"] = ds.Tables["doc"].Rows[0]["DoctId"].ToString();
                return RedirectToAction("DoctorHome", "Doctor");
            }
            else
            {
                ViewBag.info = "Please Check the credentials";
            }
            return View();
        }
        public ActionResult PatientLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PatientLogin(Login l)
        {
            DataSet ds = pop.logincheck(l.Username, l.Password);
            if ((ds.Tables["doc"].Rows.Count == 1))
            {

                Session["name"] = ds.Tables["doc"].Rows[0]["PatName"].ToString();
                Session["Email"] = ds.Tables["doc"].Rows[0]["email"].ToString();
                Session["Password"] = ds.Tables["doc"].Rows[0]["password"].ToString();
                Session["id"] = ds.Tables["doc"].Rows[0]["PatId"].ToString();
                return RedirectToAction("PatientHome", "Patient");
            }
            else
            {
                ViewBag.info = "Please Check the credentials";
            }
            return View();
        }
    }
}