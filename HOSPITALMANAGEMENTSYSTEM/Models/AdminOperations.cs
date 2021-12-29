using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
namespace HOSPITALMANAGEMENTSYSTEM.Models
{
    public class AdminOperations
    {
        List<Specializations> dlst = new List<Specializations>();
        List<Doctors> slst = new List<Doctors>();
        List<Patients> plst = new List<Patients>();


        SqlConnection con = null;
        public AdminOperations()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);
        }
        public bool AddSpecialization(Specializations s)
        {
            bool b = false;
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into Specialization(specializationName)values(@sname)", con);
                cmd.Parameters.AddWithValue("@sname", s.specializationName);
                cmd.ExecuteNonQuery();
                b = true;
            }
            catch (Exception e)
            {
                b = false;
            }
            return b;
        }
        public DataSet ViewSpecialization()
        {
            SqlDataAdapter adpt = new SqlDataAdapter("select * from Specialization order by spclId asc", con);
            DataSet ds = new DataSet();
            adpt.Fill(ds, "spcl");
            return ds;
        }
        public IEnumerable<Specializations> GetSpclData()
        {
            SqlDataAdapter data = new SqlDataAdapter("Select * from Specialization order by spclId", con);
            DataSet ds = new DataSet();
            data.Fill(ds, "spcl");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Specializations s = new Specializations();
                s.spclId = int.Parse(ds.Tables[0].Rows[i]["spclId"].ToString());
                s.specializationName = ds.Tables[0].Rows[i]["specializationName"].ToString();
                dlst.Add(s);
            }
            return dlst;
        }
        public bool AddDoctors(Doctors d)
        {
            bool b = false;
            try
            {            
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into Doctors(DoctId,DoctName,Gender,Address,phonenumber,age,spclId,email,password)values(@did ,@dname, @gen, @add, @pno, @age ,@sid ,@em, @pwd)", con);
                cmd.Parameters.AddWithValue("@did", d.DoctId);
                cmd.Parameters.AddWithValue("@dname", d.DoctName);
                cmd.Parameters.AddWithValue("@gen", d.Gender);
                cmd.Parameters.AddWithValue("@add", d.Address);
                cmd.Parameters.AddWithValue("@pno", d.phonenumber);
                cmd.Parameters.AddWithValue("@age", d.age);
                cmd.Parameters.AddWithValue("@sid", d.spclId);
                cmd.Parameters.AddWithValue("@em", d.email);
                cmd.Parameters.AddWithValue("@pwd", d.password);
                cmd.ExecuteNonQuery();
                b = true;
            }
            catch (Exception e)
            {
                b = false;
            }
            return b;
        }
        public DataSet ViewDoctor()
        {
            SqlDataAdapter adpt = new SqlDataAdapter("select * from Doctors as d inner join Specialization as s on d.spclId=s.spclId", con);
            DataSet ds = new DataSet();
            adpt.Fill(ds, "doc");
            return ds;
        }
        public DataSet ViewDoctor(string id)
        {
            SqlDataAdapter adpt = new SqlDataAdapter("select * from Doctors as d inner join Specialization as s on d.spclId=s.spclId where d.DoctId='"+id+"'", con);
            DataSet ds = new DataSet();
            adpt.Fill(ds, "doc");
            return ds;
        }
        public bool UpdateDoctor(Doctors d)
        {
            bool b = false;
            try
            {
                SqlDataAdapter adpt = new SqlDataAdapter("update Doctors set DoctName='" + d.DoctName + "',Address='" + d.Address + "',phonenumber='" + d.phonenumber + "',age='" + d.age + "',email='" + d.email + "',password='" + d.password + "' where DoctId='" + d.DoctId + "'", con);
                DataSet ds = new DataSet();
                adpt.Fill(ds, "apt");
                b = true;
            }
            catch (Exception ex)
            {
                b = false;
            }
            return b;
        }
        public bool EditDoctor(string id, DoctorDemo p)
        {
            Doctors a = new Doctors();
            a.DoctId = id;
            a.DoctName = p.DoctName;
            a.Address = p.Address;
            a.phonenumber = p.phonenumber;
            a.age = p.age;
            a.email = p.email;
            a.password = p.password;
            bool b = UpdateDoctor(a);
            return b;
        }
        public bool DeleteDoctor(string id)
        {

            bool b = false;
            SqlDataAdapter adpt = new SqlDataAdapter("delete from Doctors where DoctId='" + id + "'", con);
            DataSet ds = new DataSet();
            adpt.Fill(ds, "Doc");
            b = true;
            return b;
            //return RedirectToAction("ViewDoctor");
        }

        public bool AddPatients(Patients p)
        {
            bool b = false;
            try
            {
                //create table Doctors(DoctId varchar(5) primary key,DoctName varchar(20),Gender varchar(10),Address varchar(50),phonenumber char(10),
                //age int, spclId int , foreign key(spclId) references Specialization, role varchar(10),email varchar(30),password varchar(10))
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into Patients(PatId,PatName,Gender,Address,phonenumber,age,bloodgrp,email,password)values(@pid ,@pname, @gen, @add, @pno, @age ,@bgrp ,@em, @pwd)", con);
                cmd.Parameters.AddWithValue("@pid", p.PatId);
                cmd.Parameters.AddWithValue("@pname", p.PatName);
                cmd.Parameters.AddWithValue("@gen", p.Gender);
                cmd.Parameters.AddWithValue("@add", p.Address);
                cmd.Parameters.AddWithValue("@pno", p.phonenumber);
                cmd.Parameters.AddWithValue("@age", p.age);
                cmd.Parameters.AddWithValue("@bgrp", p.bloodgrp);
                cmd.Parameters.AddWithValue("@em", p.email);
                cmd.Parameters.AddWithValue("@pwd", p.password);
                cmd.ExecuteNonQuery();
                b = true;
            }
            catch
            {
                b = false;
            }
            return b;

        }
        public DataSet ShowPatient()
        {
            SqlDataAdapter adpt = new SqlDataAdapter("select * from Patients", con);
            DataSet ds = new DataSet();
            adpt.Fill(ds, "pat");
            return ds;
        }
        public DataSet ShowPatient(string id)
        {
            SqlDataAdapter adpt = new SqlDataAdapter("select * from Patients where PatId='"+id+"'", con);
            DataSet ds = new DataSet();
            adpt.Fill(ds, "pat");
            return ds;
        }
        public bool UpdatePatient(Patients d)
        {
            bool b = false;
            try
            {
                SqlDataAdapter adpt = new SqlDataAdapter("update Patients set PatName='" + d.PatName + "',Address='" + d.Address + "',bloodgrp='" + d.bloodgrp + "',phonenumber='" + d.phonenumber + "',age='" + d.age + "',email='" + d.email + "',password='" + d.password + "' where PatId='" + d.PatId + "'", con);
                DataSet ds = new DataSet();
                adpt.Fill(ds, "apt");
                b = true;
            }
            catch (Exception ex)
            {
                b = false;
            }
            return b;
        }
        public bool EditPatient(string id, PatientDemo p)
        {
            Patients a = new Patients();
            a.PatId = id;
            a.PatName = p.PatName;
            a.Address = p.Address;
            a.bloodgrp = p.bloodgrp;
            a.phonenumber = p.phonenumber;
            a.age = p.age;
            a.email = p.email;
            a.password = p.password;
            bool b = UpdatePatient(a);
            return b;
        }
        public bool DeletePatient(string id)
        {

            bool b = false;
            SqlDataAdapter adpt = new SqlDataAdapter("delete from Patients where PatId='" + id + "'", con);
            DataSet ds = new DataSet();
            adpt.Fill(ds, "Pat");
            b = true;
            return b;
            //return RedirectToAction("ViewDoctor");
        }
        public IEnumerable<Doctors> GetDocData()
        {
            SqlDataAdapter data = new SqlDataAdapter("select * from Doctors as d inner join Specialization as s on d.spclId=s.spclId ", con);
            DataSet ds = new DataSet();
            data.Fill(ds, "doc");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Doctors s = new Doctors();
                s.DoctId = (ds.Tables[0].Rows[i]["DoctId"].ToString());
                s.DoctName = ds.Tables[0].Rows[i]["DoctName"].ToString();
                s.specializationName= ds.Tables[0].Rows[i]["specializationName"].ToString();
                slst.Add(s);
            }
            return slst;
        }
        public IEnumerable<Patients> GetPatData()
        {
            SqlDataAdapter data = new SqlDataAdapter("Select * from Patients ", con);
            DataSet ds = new DataSet();
            data.Fill(ds, "pat");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Patients s = new Patients();
                s.PatId = (ds.Tables[0].Rows[i]["PatId"].ToString());
                s.PatName = ds.Tables[0].Rows[i]["PatName"].ToString();
                plst.Add(s);
            }
            return plst;
        }
       
        public bool AddAppointment(Appointments a)
        {
            bool b = false;
            try 
            {
                //create table Appointments(AppointmentId varchar(10) primary key, PatId varchar(5),foreign key(PatId) references Patients,DoctId varchar(5),foreign key(DoctId) references Doctors,
                //disease varchar(50),AppDate date)
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into Appointments (AppointmentId, PatId, DoctId, disease, AppDate, AppTime) values (@aid, @pid, @did, @dis, @adt, @atm)", con);
                cmd.Parameters.AddWithValue("@aid", a.AppointmentId);
                cmd.Parameters.AddWithValue("@pid", a.PatId);
                cmd.Parameters.AddWithValue("@did", a.DoctId);
                cmd.Parameters.AddWithValue("@dis", a.disease);
                cmd.Parameters.AddWithValue("@adt", a.Date);
                cmd.Parameters.AddWithValue("@atm", a.AppTime);

                cmd.ExecuteNonQuery();
                b = true;
            }
            catch(Exception ex) 
            {
                b = false;
            }
            
            return b;
        }
        public DataSet ViewAppointment()
        {
            SqlDataAdapter adpt = new SqlDataAdapter("select * from Appointments as a inner join Patients as p on a.PatId=p.PatId  inner join Doctors as d on a.DoctId=d.DoctId  inner join Specialization as s on d.spclId=s.spclId", con);
            DataSet ds = new DataSet();
            adpt.Fill(ds, "apt");
            return ds;
        }
        public DataSet ViewAppointment(string id)
        {
            SqlDataAdapter adpt = new SqlDataAdapter("select * from Appointments as a inner join Patients as p on a.PatId=p.PatId  inner join Doctors as d on a.DoctId=d.DoctId  inner join Specialization as s on d.spclId=s.spclId where a.AppointmentId='"+id+"'", con);
            DataSet ds = new DataSet();
            adpt.Fill(ds, "apt");
            return ds;
        }
        public bool UpdateAppointment(Appointments d)
        {
            bool b = false;
            try
            {
                SqlDataAdapter adpt = new SqlDataAdapter("update Appointments set DoctId='" + d.DoctId + "',AppDate='" + d.Date + "',AppTime='" + d.AppTime + "' where AppointmentId='" + d.AppointmentId + "'", con);
                DataSet ds = new DataSet();
                adpt.Fill(ds, "apt");
                b = true;
            }
            catch (Exception ex)
            {
                b = false;
            }
            return b;
        }
        public bool EditAppointment(string id, AppointmentDemo p)
        {
            Appointments a = new Appointments();
            a.AppointmentId = id;
            a.DoctId = p.DoctId;
            a.Date = p.Date;
            a.AppTime = p.AppTime;           
            bool b = UpdateAppointment(a);
            return b;
        }
        public bool DeleteAppointment(string id)
        {

            bool b = false;
            SqlDataAdapter adpt = new SqlDataAdapter("delete from Appointments where AppointmentId='" + id + "'", con);
            DataSet ds = new DataSet();
            adpt.Fill(ds, "Pat");
            b = true;
            return b;
            //return RedirectToAction("ViewDoctor");
        }
        public DataSet SAppointment()
        {
            SqlDataAdapter adpt = new SqlDataAdapter("select * from Appointments as a inner join Patients as p on a.PatId = p.PatId  inner join Doctors as d on a.DoctId = d.DoctId  inner join Specialization as s on d.spclId = s.spclId where a.flag = 0", con);
            DataSet ds = new DataSet();
            adpt.Fill(ds, "apt");
            return ds;
        }
        public DataSet CAppointment()
        {
            SqlDataAdapter adpt = new SqlDataAdapter("select * from Appointments as a inner join Patients as p on a.PatId=p.PatId  inner join Doctors as d on a.DoctId=d.DoctId  inner join Specialization as s on d.spclId=s.spclId where a.flag=1", con);
            DataSet ds = new DataSet();
            adpt.Fill(ds, "apt");
            return ds;
        }

    }
}