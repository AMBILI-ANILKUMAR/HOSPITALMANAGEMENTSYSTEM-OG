using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using HOSPITALMANAGEMENTSYSTEM.Models;
namespace HOSPITALMANAGEMENTSYSTEM.Controllers
{
    public class DoctorController : Controller
    {
        DoctorOperations dop = new DoctorOperations();
        List<Appointments> alst = new List<Appointments>();
        List<Doctors> prolst = new List<Doctors>();
        List<Appointments> plst = new List<Appointments>();
        List<Appointments> aplst = new List<Appointments>();

        // GET: Doctor
        public ActionResult DoctorHome()
        {
            string id = (string)Session["id"];
            DataSet ds = dop.ViewProfile(id);
            Doctors d = new Doctors();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                d.DoctId = ds.Tables[0].Rows[i]["DoctId"].ToString();
                d.DoctName = ds.Tables[0].Rows[i]["DoctName"].ToString();
                d.Gender = ds.Tables[0].Rows[i]["Gender"].ToString();
                d.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                d.phonenumber = ds.Tables[0].Rows[i]["phonenumber"].ToString();
                d.age = int.Parse(ds.Tables[0].Rows[i]["age"].ToString());
                d.specializationName = ds.Tables[0].Rows[i]["specializationName"].ToString();
                d.email = ds.Tables[0].Rows[i]["email"].ToString();
                d.password = ds.Tables[0].Rows[i]["password"].ToString();
            }
            Session["Sname"] = d.DoctName;
            ViewBag.name = Session["Sname"];
            ViewBag.id = "Employee ID:" + Session["id"];
            return View();

            
        }
        public ActionResult ProfileView()
        {
            string id = (string)Session["id"];
            DataSet ds = dop.ViewProfile(id);
            Doctors d = new Doctors();
            if ((ds.Tables["apt"].Rows.Count > 0))
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    d.DoctId = ds.Tables[0].Rows[i]["DoctId"].ToString();
                    d.DoctName = ds.Tables[0].Rows[i]["DoctName"].ToString();
                    d.Gender = ds.Tables[0].Rows[i]["Gender"].ToString();
                    d.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                    d.phonenumber = ds.Tables[0].Rows[i]["phonenumber"].ToString();
                    d.age = int.Parse(ds.Tables[0].Rows[i]["age"].ToString());
                    d.specializationName = ds.Tables[0].Rows[i]["specializationName"].ToString();
                    d.email = ds.Tables[0].Rows[i]["email"].ToString();
                    d.password = ds.Tables[0].Rows[i]["password"].ToString();
                }
                Session["Sname"] = d.DoctName;
                ViewBag.name = Session["Sname"];
                ViewBag.id = "Employee ID:" + Session["id"];
                return View(d);

            }
            else
            {
                ViewBag.info = "Not Yet!";
                ViewBag.name = Session["name"];
                ViewBag.id = "Employee ID:" + Session["id"];
                return View(d);
            }
        }
        public ActionResult EditProfile(string id)
        {
            DoctorDemo d = new DoctorDemo();
            DataSet ds = dop.ViewProfile(id);
            if ((ds.Tables["apt"].Rows.Count > 0))
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    d.spclId = int.Parse(ds.Tables[0].Rows[i]["spclId"].ToString());
                    d.DoctId = ds.Tables[0].Rows[i]["DoctId"].ToString();
                    d.DoctName = ds.Tables[0].Rows[i]["DoctName"].ToString();
                    d.Gender = ds.Tables[0].Rows[i]["Gender"].ToString();
                    d.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                    d.phonenumber = ds.Tables[0].Rows[i]["phonenumber"].ToString();
                    d.age = int.Parse(ds.Tables[0].Rows[i]["age"].ToString());
                    d.specializationName = ds.Tables[0].Rows[i]["specializationName"].ToString();
                    d.email = ds.Tables[0].Rows[i]["email"].ToString();
                    d.password = ds.Tables[0].Rows[i]["password"].ToString();
                }
                d.SpclDropdown = new SelectList(dop.GetSpclData(), "spclId", "specializationName",d.spclId);
                ViewBag.name = Session["Sname"];
                ViewBag.id = "Employee ID:" + Session["id"];
                return View(d);

            }
            else
            {
                ViewBag.info = "Not Yet!";
                d.SpclDropdown = new SelectList(dop.GetSpclData(), "spclId", "specializationName");
                ViewBag.name = Session["Sname"];
                ViewBag.id = "Employee ID:" + Session["id"];
                return View(d);
            }
        }
        [HttpPost]
        public ActionResult EditProfile(string id,DoctorDemo d)
        {
            bool b = dop.EditProfile(id, d);
            if (b == true)
                return RedirectToAction("ProfileView");
            d.SpclDropdown = new SelectList(dop.GetSpclData(), "spclId", "specializationName");
            ViewBag.name = Session["Sname"];
            ViewBag.id = "Employee ID:" + Session["id"];
            return View();
        }
        public ActionResult Appointments()
        {
            string id = (string)Session["id"];
            DataSet ds = dop.ViewPatient(id);
            if ((ds.Tables["apt"].Rows.Count > 0))
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Appointments d = new Appointments();
                    d.AppointmentId = ds.Tables[0].Rows[i]["AppointmentId"].ToString();
                    d.PatName = ds.Tables[0].Rows[i]["PatName"].ToString();
                    d.DoctName = ds.Tables[0].Rows[i]["DoctName"].ToString();
                    d.disease = ds.Tables[0].Rows[i]["disease"].ToString();
                    d.Gender = ds.Tables[0].Rows[i]["Gender"].ToString();
                    d.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                    d.phonenumber = ds.Tables[0].Rows[i]["phonenumber"].ToString();
                    d.age = int.Parse(ds.Tables[0].Rows[i]["age"].ToString());
                    d.bloodgrp = ds.Tables[0].Rows[i]["bloodgrp"].ToString();
                    d.Date = DateTime.Parse(ds.Tables[0].Rows[i]["AppDate"].ToString());
                    d.AppTime = (ds.Tables[0].Rows[i]["AppTime"].ToString());
                    d.diagnosis= (ds.Tables[0].Rows[i]["diagnosis"].ToString());
                    d.medicine = (ds.Tables[0].Rows[i]["medicine"].ToString());
                    alst.Add(d);
                }
                ViewBag.name = Session["Sname"];
                ViewBag.id = "Employee ID:" + Session["id"];
                return View(alst);

            }
            else
            {
                ViewBag.info = "No Patients consulted Yet!";
                ViewBag.name = Session["Sname"];
                ViewBag.id = "Employee ID:" + Session["id"];
                return View(alst);
            }
        }
        public ActionResult Prescription(string id)
        {
            string did = (string)Session["id"];
            DataSet ds = dop.PatientPrescriptions(did,id);
            if ((ds.Tables["apt"].Rows.Count == 0))
            {

                ViewBag.info = "No Prescription Yet!";
                ViewBag.name = Session["Sname"];
                ViewBag.id = "Employee ID:" + Session["id"];
                return View(plst);


            }
            else
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Appointments d = new Appointments();
                    d.AppointmentId = ds.Tables[0].Rows[i]["AppointmentId"].ToString();
                    d.PatName = ds.Tables[0].Rows[i]["PatName"].ToString();
                    d.disease = ds.Tables[0].Rows[i]["disease"].ToString();
                    d.Date = DateTime.Parse(ds.Tables[0].Rows[i]["AppDate"].ToString());
                    d.AppTime = (ds.Tables[0].Rows[i]["AppTime"].ToString());
                    d.diagnosis = ds.Tables[0].Rows[i]["diagnosis"].ToString();
                    d.medicine = ds.Tables[0].Rows[i]["medicine"].ToString();
                    plst.Add(d);
                }
                ViewBag.name = Session["Sname"];
                ViewBag.id = "Employee ID:" + Session["id"];
                return View(plst);
            }
        }
        public ActionResult AddPrescription(string id)
        {
            ViewBag.name = Session["Sname"];
            ViewBag.id = "Employee ID:" + Session["id"];
            string did = (string)Session["id"];
            DataSet ds = dop.ViewPrescription(did, id);
            Prescription d = new Prescription();
            if ((ds.Tables["apt"].Rows.Count > 0))
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    d.AppointmentId = ds.Tables[0].Rows[i]["AppointmentId"].ToString();
                    d.PatName = ds.Tables[0].Rows[i]["PatName"].ToString();
                    d.Gender = ds.Tables[0].Rows[i]["Gender"].ToString();
                    d.phonenumber = ds.Tables[0].Rows[i]["phonenumber"].ToString();
                    d.age = int.Parse(ds.Tables[0].Rows[i]["age"].ToString());
                    d.bloodgrp = ds.Tables[0].Rows[i]["bloodgrp"].ToString();
                    d.disease = ds.Tables[0].Rows[i]["disease"].ToString();
                    d.Date = DateTime.Parse(ds.Tables[0].Rows[i]["AppDate"].ToString());
                    d.AppTime = (ds.Tables[0].Rows[i]["AppTime"].ToString());
                    d.diagnosis = ds.Tables[0].Rows[i]["diagnosis"].ToString();
                    d.medicine = ds.Tables[0].Rows[i]["medicine"].ToString();

                }
                return View(d);
            }
            else
            {
                ViewBag.info = "No Prescription Added Yet!";
                ViewBag.name = Session["Sname"];
                ViewBag.id = "Employee ID:" + Session["id"];
                return View(d);
            }
        }
        [HttpPost]
        public ActionResult AddPrescription(string id,Prescription p)
        {
            bool b = dop.Edit(id, p);
            if (b == true)
                return RedirectToAction("Patient");
            return View();
        }
        public ActionResult EditPrescription(string id)
        {
            ViewBag.name = Session["Sname"];
            ViewBag.id = "Employee ID:" + Session["id"];
            string did = (string)Session["id"];
            DataSet ds = dop.EditPrescription(did, id);
            Prescription d = new Prescription();
            if ((ds.Tables["apt"].Rows.Count > 0))
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    d.AppointmentId = ds.Tables[0].Rows[i]["AppointmentId"].ToString();
                    d.PatName = ds.Tables[0].Rows[i]["PatName"].ToString();
                    d.Gender = ds.Tables[0].Rows[i]["Gender"].ToString();
                    d.phonenumber = ds.Tables[0].Rows[i]["phonenumber"].ToString();
                    d.age = int.Parse(ds.Tables[0].Rows[i]["age"].ToString());
                    d.bloodgrp = ds.Tables[0].Rows[i]["bloodgrp"].ToString();
                    d.disease = ds.Tables[0].Rows[i]["disease"].ToString();
                    d.Date = DateTime.Parse(ds.Tables[0].Rows[i]["AppDate"].ToString());
                    d.AppTime = (ds.Tables[0].Rows[i]["AppTime"].ToString());
                    d.diagnosis = ds.Tables[0].Rows[i]["diagnosis"].ToString();
                    d.medicine = ds.Tables[0].Rows[i]["medicine"].ToString();

                }
                return View(d);
            }
            else
            {
                ViewBag.info = "No Prescription Added Yet!";
                ViewBag.name = Session["Sname"];
                ViewBag.id = "Employee ID:" + Session["id"];
                return View(d);
            }
        }
        [HttpPost]
        public ActionResult EditPrescription(string id, Prescription p)
        {
            bool b = dop.Edit(id, p);
            if (b == true)
                return RedirectToAction("Appointments");
            return View();
        }
        public ActionResult Patient()
        {
            string did = (string)Session["id"];
            DataSet ds = dop.ViewAppointment(did);
            if ((ds.Tables["apt"].Rows.Count > 0))
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Appointments d = new Appointments();
                    d.AppointmentId = ds.Tables[0].Rows[i]["AppointmentId"].ToString();
                    d.PatName = ds.Tables[0].Rows[i]["PatName"].ToString();
                    d.disease = ds.Tables[0].Rows[i]["disease"].ToString();
                    d.Date = DateTime.Parse(ds.Tables[0].Rows[i]["AppDate"].ToString());
                    d.AppTime = (ds.Tables[0].Rows[i]["AppTime"].ToString());
                    d.diagnosis = ds.Tables[0].Rows[i]["diagnosis"].ToString();
                    d.medicine = ds.Tables[0].Rows[i]["medicine"].ToString();
                    aplst.Add(d);
                }
                ViewBag.name = Session["Sname"];
                ViewBag.id = "Employee ID:" + Session["id"];
                return View(aplst);
            }
            else
            {
                ViewBag.info = "No Appointments Currently assigned!";
                ViewBag.name = Session["Sname"];
                ViewBag.id = "Employee ID:" + Session["id"];
                return View(aplst);
            }
        }

    }
}