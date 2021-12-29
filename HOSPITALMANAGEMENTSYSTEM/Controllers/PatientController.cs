using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using HOSPITALMANAGEMENTSYSTEM.Models;
namespace HOSPITALMANAGEMENTSYSTEM.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Index()
        {
            return View();
        }
        PatientOperations pop = new PatientOperations();
        List<Patients> prolst = new List<Patients>();
        List<Doctors> dlst = new List<Doctors>();
        List<Appointments> aplst = new List<Appointments>();
        List<Appointments> alst = new List<Appointments>();
        List<Appointments> plst = new List<Appointments>();
        // GET: Patient
        public ActionResult PatientHome()
        {
            string id = (string)Session["id"];
            DataSet ds = pop.ViewProfile(id);
            Patients d = new Patients();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    d.PatId = ds.Tables[0].Rows[i]["PatId"].ToString();
                    d.PatName = ds.Tables[0].Rows[i]["PatName"].ToString();
                    d.Gender = ds.Tables[0].Rows[i]["Gender"].ToString();
                    d.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                    d.phonenumber = ds.Tables[0].Rows[i]["phonenumber"].ToString();
                    d.age = int.Parse(ds.Tables[0].Rows[i]["age"].ToString());
                    d.bloodgrp = ds.Tables[0].Rows[i]["bloodgrp"].ToString();

                    d.email = ds.Tables[0].Rows[i]["email"].ToString();
                    d.password = ds.Tables[0].Rows[i]["password"].ToString();
                    prolst.Add(d);
                }
            Session["Pname"] =d.PatName ;
            ViewBag.name = Session["Pname"];
                ViewBag.id = "ID:" + Session["id"];
                return View();

            
           
        }
        public ActionResult ProfileView()
        {
            string id = (string)Session["id"];
            DataSet ds = pop.ViewProfile(id);
            
                Patients d = new Patients();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    d.PatId = ds.Tables[0].Rows[i]["PatId"].ToString();
                    d.PatName = ds.Tables[0].Rows[i]["PatName"].ToString();
                    d.Gender = ds.Tables[0].Rows[i]["Gender"].ToString();
                    d.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                    d.phonenumber = ds.Tables[0].Rows[i]["phonenumber"].ToString();
                    d.age = int.Parse(ds.Tables[0].Rows[i]["age"].ToString());
                    d.bloodgrp = ds.Tables[0].Rows[i]["bloodgrp"].ToString();
                    
                    d.email = ds.Tables[0].Rows[i]["email"].ToString();
                    d.password = ds.Tables[0].Rows[i]["password"].ToString();
                }

                Session["Pname"] = d.PatName;
                ViewBag.name = Session["Pname"];
                ViewBag.id = "ID:" + Session["id"];
                return View(d);

            
           

        }
        public ActionResult EditPatient(string id)
        {
            //string id = (string)Session["id"];
            PatientDemo d = new PatientDemo();
            DataSet ds = pop.ViewProfile(id);
            if ((ds.Tables["apt"].Rows.Count > 0))
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    d.PatId = ds.Tables[0].Rows[i]["PatId"].ToString();
                    d.PatName = ds.Tables[0].Rows[i]["PatName"].ToString();
                    d.Gender = ds.Tables[0].Rows[i]["Gender"].ToString();
                    d.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                    d.phonenumber = ds.Tables[0].Rows[i]["phonenumber"].ToString();
                    d.age = int.Parse(ds.Tables[0].Rows[i]["age"].ToString());

                    d.bloodgrp = ds.Tables[0].Rows[i]["bloodgrp"].ToString();
                    d.email = ds.Tables[0].Rows[i]["email"].ToString();
                    d.password = ds.Tables[0].Rows[i]["password"].ToString();
                }
                if (d.bloodgrp =="A+ ")
                    d.bloodgrp = "0";
                else if (d.bloodgrp== "A- ")
                    d.bloodgrp = "1";
                else if (d.bloodgrp== "B+ ")
                    d.bloodgrp = "2";
                else if (d.bloodgrp.ToString() == "B- ")
                    d.bloodgrp = "3";
                else if (d.bloodgrp.ToString() == "O+ ")
                    d.bloodgrp = "4";
                else if (d.bloodgrp.ToString() == "O- ")
                    d.bloodgrp = "5";
                else if (d.bloodgrp.ToString() == "AB+ ")
                    d.bloodgrp = "6";
                else if (d.bloodgrp.ToString() == "AB- ")
                    d.bloodgrp = "7";
                else
                    d.bloodgrp = "1";
                ViewBag.name = Session["Pname"];
                ViewBag.id = "ID:" + Session["id"];
                return View(d);

            }
            else
            {
                ViewBag.info = "Not Yet!";
                ViewBag.name = Session["name"];
                ViewBag.id = "ID:" + Session["id"];
                return View(d);
            }

        }
        [HttpPost]
        public ActionResult EditPatient(string id, PatientDemo d)
        {
            if (d.bloodgrp == "0")
                d.bloodgrp = "A+";
            else if (d.bloodgrp == "1")
                d.bloodgrp = "A-";
            else if (d.bloodgrp == "2")
                d.bloodgrp = "B+";
            else if (d.bloodgrp == "3")
                d.bloodgrp = "B-";
            else if (d.bloodgrp == "4")
                d.bloodgrp = "O+";
            else if (d.bloodgrp == "5")
                d.bloodgrp = "0-";
            else if (d.bloodgrp == "6")
                d.bloodgrp = "AB+";
            else if (d.bloodgrp == "7")
                d.bloodgrp = "AB-";
            bool b = pop.EditProfile(id, d);
            if (b == true)
                return RedirectToAction("ProfileView");
            ViewBag.name = Session["Pname"];
            ViewBag.id = "Employee ID:" + Session["id"];
            return View();
        }
        public ActionResult Doctors()
        {

            DataSet ds = pop.ViewAllDoctors();
            if ((ds.Tables["doc"].Rows.Count > 0))
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Doctors d = new Doctors();
                    d.DoctId = ds.Tables[0].Rows[i]["DoctId"].ToString();
                    d.DoctName = ds.Tables[0].Rows[i]["DoctName"].ToString();
                    d.Gender = ds.Tables[0].Rows[i]["Gender"].ToString();
                    d.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                    d.phonenumber = ds.Tables[0].Rows[i]["phonenumber"].ToString();

                    d.specializationName = ds.Tables[0].Rows[i]["specializationName"].ToString();
                    d.email = ds.Tables[0].Rows[i]["email"].ToString();


                    dlst.Add(d);
                }
                ViewBag.name = Session["Pname"];
                ViewBag.id = "ID:" + Session["id"];
                return View(dlst);
            }
            else
            {
                ViewBag.name = Session["Pname"];
                ViewBag.id = "ID:" + Session["id"];
                ViewBag.info = "No Doctor Added Yet!";
                return View(dlst);
            }
        }
        public ActionResult Appointments()
        {
            ViewBag.name = Session["Pname"];
            ViewBag.id = "ID:" + Session["id"];
            return View();
        }
        public ActionResult UpcomingAppointment()
        {
            string did = (string)Session["id"];
            DataSet ds = pop.ViewAppointment(did);
            if ((ds.Tables["apt"].Rows.Count > 0))
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Appointments d = new Appointments();
                    d.AppointmentId = ds.Tables[0].Rows[i]["AppointmentId"].ToString();
                    d.DoctName = ds.Tables[0].Rows[i]["DoctName"].ToString();
                    d.SplName = ds.Tables[0].Rows[i]["specializationName"].ToString();
                    d.disease = ds.Tables[0].Rows[i]["disease"].ToString();
                    d.Date = DateTime.Parse(ds.Tables[0].Rows[i]["AppDate"].ToString());
                    d.AppTime = (ds.Tables[0].Rows[i]["AppTime"].ToString());
                    d.diagnosis = ds.Tables[0].Rows[i]["diagnosis"].ToString();
                    d.medicine = ds.Tables[0].Rows[i]["medicine"].ToString();
                    aplst.Add(d);
                }
                ViewBag.name = Session["Pname"];
                ViewBag.id = "ID:" + Session["id"];
                return View(aplst);
            }
            else
            {
                ViewBag.info = "No Appointments Currently Assigned!";
                ViewBag.name = Session["Pname"];
                ViewBag.id = "ID:" + Session["id"];
                return View(aplst);
            }
        }
            public ActionResult OldAppointment()
            {
                string id = (string)Session["id"];
                DataSet ds = pop.ViewPatient(id);
                if ((ds.Tables["apt"].Rows.Count > 0))
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Appointments d = new Appointments();
                        d.AppointmentId = ds.Tables[0].Rows[i]["AppointmentId"].ToString();
                        d.PatName = ds.Tables[0].Rows[i]["PatName"].ToString();
                        d.DoctName = ds.Tables[0].Rows[i]["DoctName"].ToString();
                        d.SplName = ds.Tables[0].Rows[i]["specializationName"].ToString();
                        d.disease = ds.Tables[0].Rows[i]["disease"].ToString();
                        d.Gender = ds.Tables[0].Rows[i]["Gender"].ToString();
                        d.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                        d.phonenumber = ds.Tables[0].Rows[i]["phonenumber"].ToString();
                        d.age = int.Parse(ds.Tables[0].Rows[i]["age"].ToString());
                        d.bloodgrp = ds.Tables[0].Rows[i]["bloodgrp"].ToString();
                        d.Date = DateTime.Parse(ds.Tables[0].Rows[i]["AppDate"].ToString());
                        d.AppTime = (ds.Tables[0].Rows[i]["AppTime"].ToString());
                        d.diagnosis = (ds.Tables[0].Rows[i]["diagnosis"].ToString());
                        d.medicine = (ds.Tables[0].Rows[i]["medicine"].ToString());
                        alst.Add(d);
                    }
                    ViewBag.name = Session["Pname"];
                    ViewBag.id = "ID:" + Session["id"];
                    return View(alst);

                }
                else
                {
                    ViewBag.info = "No Prescription Added Yet!!";
                    ViewBag.name = Session["Pname"];
                    ViewBag.id = "ID:" + Session["id"];
                    return View(alst);
                }
            }
        public ActionResult Prescription(string id)
        {
            string did = (string)Session["id"];
            DataSet ds = pop.PatientPrescriptions(did, id);
            if ((ds.Tables["apt"].Rows.Count == 0))
            {

                ViewBag.info = "No Prescription Yet!";
                ViewBag.name = Session["Pname"];
                ViewBag.id = "ID:" + Session["id"];
                return View(plst);


            }
            else
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Appointments d = new Appointments();
                    d.AppointmentId = ds.Tables[0].Rows[i]["AppointmentId"].ToString();
                    d.DoctName = ds.Tables[0].Rows[i]["DoctName"].ToString();
                    d.SplName = ds.Tables[0].Rows[i]["specializationName"].ToString();
                    d.disease = ds.Tables[0].Rows[i]["disease"].ToString();
                    d.Date = DateTime.Parse(ds.Tables[0].Rows[i]["AppDate"].ToString());
                    d.AppTime = (ds.Tables[0].Rows[i]["AppTime"].ToString());
                    d.diagnosis = ds.Tables[0].Rows[i]["diagnosis"].ToString();
                    d.medicine = ds.Tables[0].Rows[i]["medicine"].ToString();
                    plst.Add(d);
                }
                ViewBag.name = Session["Pname"];
                ViewBag.id = "ID:" + Session["id"];
                return View(plst);
            }
        }
    }
}