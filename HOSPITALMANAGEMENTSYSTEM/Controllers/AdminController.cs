using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using PagedList;
using System.Data;
using HOSPITALMANAGEMENTSYSTEM.Models;
namespace HOSPITALMANAGEMENTSYSTEM.Controllers
{
    public class AdminController : Controller
    {
        AdminOperations aop = new AdminOperations();
        List<Specializations> slst = new List<Specializations>();
        List<Doctors> dlst = new List<Doctors>();
        List<Patients> plst = new List<Patients>();
        List<Appointments> alst = new List<Appointments>();


        // GET: Admin
        public ActionResult AdminLogin()
        {
            return View();
        }
        public ActionResult Appointment()
        {
            return View();
        }
        public ActionResult Specialization()
        {
            return View();
        }
        public ActionResult Doctor()
        {
            return View();
        }
        public ActionResult Patient()
        {
            return View();
        }

        public ActionResult AdminHome()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult AddSpecialization()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSpecialization(Specializations s)
        {
            bool b = aop.AddSpecialization(s);
            if (b == true)
            {
                ViewBag.info = "Added Successfully";
                return RedirectToAction("ViewSpecialization");
            }
            else
            {
                ViewBag.info = "Something Went Wrong!";
            }
            return View();
        }
        public ActionResult ViewSpecialization(int? page )
        {
            int pageSize = 5;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Specializations> sli = null;
            DataSet ds = aop.ViewSpecialization();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Specializations c = new Specializations();
                c.spclId = int.Parse(ds.Tables[0].Rows[i]["spclId"].ToString());
                c.specializationName = ds.Tables[0].Rows[i]["specializationName"].ToString();
                slst.Add(c);
            }
            sli = slst.ToPagedList(pageIndex, pageSize);
            return View(sli);

        }
        public ActionResult AddDoctor()
        {
            Doctors d = new Doctors();
            d.SpclDropdown = new SelectList(aop.GetSpclData(), "spclId", "specializationName");
            return View(d);
        }
        [HttpPost]
        public ActionResult AddDoctor(Doctors d)
        {
            bool b = aop.AddDoctors(d);
            if (b == true)
            {
                d.SpclDropdown = new SelectList(aop.GetSpclData(), "spclId", "specializationName");
                ViewBag.info = "Added Successfully";
                return RedirectToAction("ViewDoctor");
            }
            else
            {
                d.SpclDropdown = new SelectList(aop.GetSpclData(), "spclId", "specializationName");
            }
            return View(d);
        }
        public ActionResult ViewDoctor(int? page)
        {
            int pageSize = 5;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Doctors> dli = null;
            DataSet ds = aop.ViewDoctor();
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
                    d.age = int.Parse(ds.Tables[0].Rows[i]["age"].ToString());
                    d.specializationName = ds.Tables[0].Rows[i]["specializationName"].ToString();
                    d.email = ds.Tables[0].Rows[i]["email"].ToString();
                    d.password = ds.Tables[0].Rows[i]["password"].ToString();
                    dlst.Add(d);
                }
                dli = dlst.ToPagedList(pageIndex, pageSize);
                return View(dli);
            }
            else
            {
                ViewBag.info = "No Doctors Added Yet!";
                return View(dli);
            }
        }
        public ActionResult EditDoctor(string id)
        {
            
                DoctorDemo d = new DoctorDemo();
                DataSet ds = aop.ViewDoctor(id);
                if ((ds.Tables["doc"].Rows.Count > 0))
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
                    d.SpclDropdown = new SelectList(aop.GetSpclData(), "spclId", "specializationName", d.spclId);                    
                    return View(d);

                }
                else
                {
                    ViewBag.info = "Not Yet!";
                    d.SpclDropdown = new SelectList(aop.GetSpclData(), "spclId", "specializationName");
                    
                    return View(d);
                }
            
        }
        public ActionResult EditAppointment(string id)
        {
            AppointmentDemo a = new AppointmentDemo();
            DataSet ds = aop.ViewAppointment(id);
            if ((ds.Tables["apt"].Rows.Count > 0))
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    a.AppointmentId = ds.Tables[0].Rows[i]["AppointmentId"].ToString();
                    a.PatName = ds.Tables[0].Rows[i]["PatName"].ToString();
                    a.DoctId= ds.Tables[0].Rows[i]["DoctId"].ToString();
                    a.PatId= ds.Tables[0].Rows[i]["PatId"].ToString();
                    a.DoctName = ds.Tables[0].Rows[i]["DoctName"].ToString();
                    a.SplName = ds.Tables[0].Rows[i]["specializationName"].ToString();
                    a.disease = ds.Tables[0].Rows[i]["disease"].ToString();
                    a.Date = DateTime.Parse(ds.Tables[0].Rows[i]["AppDate"].ToString());
                    a.AppTime = (ds.Tables[0].Rows[i]["AppTime"].ToString());
                }
                a.DocDropdown = new SelectList(aop.GetDocData(), "DoctId", "fullname",a.DoctId);
                a.PatDropdown = new SelectList(aop.GetPatData(), "PatId", "PatName",a.PatId);
                if (a.AppTime == "9AM - 12PM")
                    a.AppTime = "0";
                else if (a.AppTime == "2PM - 4PM")
                    a.AppTime = "1";
                else if (a.AppTime == "5PM - 6PM")
                    a.AppTime = "2";
                return View(a);

            }
            else
            {
                a.DocDropdown = new SelectList(aop.GetDocData(), "DoctId", "fullname");
                a.PatDropdown = new SelectList(aop.GetPatData(), "PatId", "PatName");
                if (a.AppTime == "0")
                    a.AppTime = "9AM - 12PM";
                else if (a.AppTime == "1")
                    a.AppTime = "2PM - 4PM";
                else if (a.AppTime == "2")
                    a.AppTime = "5PM - 6PM";
                return View(a);
            }
        }
        [HttpPost]
        public ActionResult EditAppointment(string id, AppointmentDemo a)
        {
            if (a.AppTime == "0")
                a.AppTime = "9AM - 12PM";
            else if (a.AppTime == "1")
                a.AppTime = "2PM - 4PM";
            else if (a.AppTime == "2")
                a.AppTime = "5PM - 6PM";
            bool b = aop.EditAppointment(id, a);
            if(b==true)
                return RedirectToAction("SAppointment");
            a.DocDropdown = new SelectList(aop.GetDocData(), "DoctId", "fullname");
            a.PatDropdown = new SelectList(aop.GetPatData(), "PatId", "PatName");
            return View(a);

        }
        public ActionResult DeleteAppointment(string id)
        {

            bool b = aop.DeleteAppointment(id);
            if (b == true)
            {
                return RedirectToAction("SAppointment");
            }
            return View();
        }
        [HttpPost]
        public ActionResult EditDoctor(string id, DoctorDemo d)
        {
            bool b = aop.EditDoctor(id, d);
            if (b == true)
                return RedirectToAction("ViewDoctor");
            d.SpclDropdown = new SelectList(aop.GetSpclData(), "spclId", "specializationName");
            return View(d);
        }
        public ActionResult DeleteDoctor(string id)
        {

            bool b = aop.DeleteDoctor(id);
            if (b==true)
            {
                return RedirectToAction("ViewDoctor");
            }
            return View();
        }

        public ActionResult AddPatient()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPatient(Patients p)
        {
            if (p.bloodgrp == "0")
                p.bloodgrp = "A+";
            else if (p.bloodgrp == "1")
                p.bloodgrp = "A-";
            else if (p.bloodgrp == "2")
                p.bloodgrp = "B+";
            else if (p.bloodgrp == "3")
                p.bloodgrp = "B-";
            else if (p.bloodgrp == "4")
                p.bloodgrp = "O+";
            else if (p.bloodgrp == "5")
                p.bloodgrp = "0-";
            else if (p.bloodgrp == "6")
                p.bloodgrp = "AB+";
            else if (p.bloodgrp == "7")
                p.bloodgrp = "AB-";
            bool b = aop.AddPatients(p);
            if (b == true)
            {
                ViewBag.info = "Added Successfully";
                return RedirectToAction("ShowPatient");
            }
            return View(p);
        }
        public ActionResult ShowPatient(int? page)
        {
            int pageSize = 5;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Patients> pli = null; 
            DataSet ds = aop.ShowPatient();
            if ((ds.Tables["pat"].Rows.Count > 0))
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Patients d = new Patients();
                    d.PatId = ds.Tables[0].Rows[i]["PatId"].ToString();
                    d.PatName = ds.Tables[0].Rows[i]["PatName"].ToString();
                    d.Gender = ds.Tables[0].Rows[i]["Gender"].ToString();
                    d.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                    d.phonenumber = ds.Tables[0].Rows[i]["phonenumber"].ToString();
                    d.age = int.Parse(ds.Tables[0].Rows[i]["age"].ToString());
                    d.bloodgrp = ds.Tables[0].Rows[i]["bloodgrp"].ToString();
                    d.email = ds.Tables[0].Rows[i]["email"].ToString();
                    d.password = ds.Tables[0].Rows[i]["password"].ToString();
                    plst.Add(d);
                }
                pli = plst.ToPagedList(pageIndex, pageSize);
                return View(pli);
            }
            else
            {
                ViewBag.info = "No Patients Added Yet!";
                return View(pli);
            }
        }
        public ActionResult EditPatient(string id)
        {
            PatientDemo d = new PatientDemo();
            DataSet ds = aop.ShowPatient(id);
            if ((ds.Tables["pat"].Rows.Count > 0))
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
                if (d.bloodgrp == "A+ ")
                    d.bloodgrp = "0";
                else if (d.bloodgrp == "A- ")
                    d.bloodgrp = "1";
                else if (d.bloodgrp == "B+ ")
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
                
                return View(d);

            }
            else
            {
                ViewBag.info = "Not Yet!";
                
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
            bool b = aop.EditPatient(id, d);
            if (b == true)
                return RedirectToAction("ShowPatient");
            
            return View();
        }
        public ActionResult DeletePatient(string id)
        {

            bool b = aop.DeletePatient(id);
            if (b == true)
            {
                return RedirectToAction("ShowPatient");
            }
            return View();
        }
        public ActionResult AddAppointments()
        {
            Appointments a = new Appointments();
            a.DocDropdown = new SelectList(aop.GetDocData(), "DoctId", "fullname");
            a.PatDropdown = new SelectList(aop.GetPatData(), "PatId", "PatName");
            return View(a);
        }
        [HttpPost]
        public ActionResult AddAppointments(Appointments a)
        {
            if (a.AppTime == "0")
                a.AppTime = "9AM - 12PM";
            else if(a.AppTime == "1")
                a.AppTime = "2PM - 4PM";
            else if(a.AppTime == "2")
                a.AppTime = "5PM - 6PM";

            bool b = aop.AddAppointment(a);
            if(b==true)
            {
                a.DocDropdown = new SelectList(aop.GetDocData(), "DoctId", "fullname");
                a.PatDropdown = new SelectList(aop.GetPatData(), "PatId", "PatName");
                ViewBag.info = "Added Successfully";
                return RedirectToAction("SAppointment");

            }
            else
            {
                a.DocDropdown = new SelectList(aop.GetDocData(), "DoctId", "fullname");
                a.PatDropdown = new SelectList(aop.GetPatData(), "PatId", "PatName");
                ViewBag.info = "Not Added";

            }
            return View(a);
        }
        public ActionResult ViewAppointment(int? page)
        {
            int pageSize = 5;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Appointments> ali = null;
            DataSet ds = aop.ViewAppointment();
            if ((ds.Tables["apt"].Rows.Count > 0))
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Appointments d = new Appointments();
                    d.AppointmentId= ds.Tables[0].Rows[i]["AppointmentId"].ToString();
                    d.PatName = ds.Tables[0].Rows[i]["PatName"].ToString();
                    d.DoctName = ds.Tables[0].Rows[i]["DoctName"].ToString();
                    d.SplName = ds.Tables[0].Rows[i]["specializationName"].ToString();
                    d.disease = ds.Tables[0].Rows[i]["disease"].ToString();
                    d.Date =DateTime.Parse( ds.Tables[0].Rows[i]["AppDate"].ToString());
                    d.AppTime = (ds.Tables[0].Rows[i]["AppTime"].ToString());
                    alst.Add(d);
                }
                ali = alst.ToPagedList(pageIndex, pageSize);
                return View(ali);
            }
            else
            {
                ViewBag.info = "No Appointments Added Yet!";
                return View(ali);
            }
        }
        public ActionResult SAppointment(int? page)
        {
            int pageSize = 5;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Appointments> ali = null;
            DataSet ds = aop.SAppointment();
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
                    d.Date = DateTime.Parse(ds.Tables[0].Rows[i]["AppDate"].ToString());
                    d.AppTime = (ds.Tables[0].Rows[i]["AppTime"].ToString());
                    alst.Add(d);
                }
                ali = alst.ToPagedList(pageIndex, pageSize);
                return View(ali);
            }
            else
            {
                ViewBag.info = "No Appointments Added Yet!";
                return View(ali);
            }
        }
        public ActionResult CAppointment(int? page)
        {
            int pageSize = 5;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Appointments> ali = null;
            DataSet ds = aop.CAppointment();
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
                    d.Date = DateTime.Parse(ds.Tables[0].Rows[i]["AppDate"].ToString());
                    d.AppTime = (ds.Tables[0].Rows[i]["AppTime"].ToString());
                    alst.Add(d);
                }
                ali = alst.ToPagedList(pageIndex, pageSize);
                return View(ali);
            }
            else
            {
                ViewBag.info = "No Appointments Added Yet!";
                return View(ali);
            }
        }
        public ActionResult DoctorSpl(int? page)
        {
            int pageSize = 5;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Doctors> dli = null;
            DataSet ds = aop.ViewDoctor();
            if ((ds.Tables["doc"].Rows.Count > 0))
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Doctors d = new Doctors();
                    d.DoctId = ds.Tables[0].Rows[i]["DoctId"].ToString();
                    d.DoctName = ds.Tables[0].Rows[i]["DoctName"].ToString();
                    
                    d.phonenumber = ds.Tables[0].Rows[i]["phonenumber"].ToString();
                    
                    d.specializationName = ds.Tables[0].Rows[i]["specializationName"].ToString();
                    
                    dlst.Add(d);
                }
                dli = dlst.ToPagedList(pageIndex, pageSize);
                return View(dli);
            }
            else
            {
                ViewBag.info = "No Doctors Added Yet!";
                return View(dli);
            }
        }
    }
}