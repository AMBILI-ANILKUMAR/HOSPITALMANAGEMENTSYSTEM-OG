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
    public class DoctorOperations
    {
        List<Appointments> alst = new List<Appointments>();
        List<Appointments> plst = new List<Appointments>();
        List<Appointments> dlst = new List<Appointments>();
        List<Specializations> slst = new List<Specializations>();

        SqlConnection con = null;
        public DoctorOperations()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);
        }
        public DataSet logincheck(string uname, string pwd)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter madpt = new SqlDataAdapter("select * from Doctors where email='" + uname + "' and password='" + pwd + "'", con);
                madpt.Fill(ds, "doc");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return ds;
        }
        public DataSet ViewProfile(string id)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adpt = new SqlDataAdapter("select * from Doctors as d inner join Specialization as s on d.spclId=s.spclId  where DoctId='" + id + "'", con);
                adpt.Fill(ds, "apt");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
                slst.Add(s);
            }
            return slst;
        }
        public bool UpdateProfile(Doctors d)
        {
            bool b = false;
            try
            {
                SqlDataAdapter adpt = new SqlDataAdapter("update Doctors set DoctName='" + d.DoctName + "',Address='" + d.Address + "',phonenumber='"+d.phonenumber+ "',age='"+d.age+ "',email='"+d.email+ "',password='"+d.password+ "' where DoctId='" + d.DoctId + "'", con);
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
        public bool EditProfile(string id, DoctorDemo p)
        {
            Doctors a = new Doctors();
            a.DoctId = id;
            a.DoctName = p.DoctName;
            a.Address = p.Address;
            a.phonenumber = p.phonenumber;
            a.age = p.age;
            a.email = p.email;
            a.password = p.password;
            bool b = UpdateProfile(a);
            return b;
        }
        public DataSet PatientsInfo(string id)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adpt = new SqlDataAdapter("select * from Patients as d inner join Appointments as s on d.PatId=s.PatId where DoctId='" + id + "'", con);
                adpt.Fill(ds, "pat");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return ds;
        }
        public DataSet ViewPatient(string id)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adpt = new SqlDataAdapter("select * from Appointments as a inner join Doctors as d on a.DoctId=d.DoctId inner join Patients as p on a.PatId=p.PatId   where a.DoctId='" + id + "' and a.flag=1", con);
                adpt.Fill(ds, "apt");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;
        }
        public DataSet ViewAppointment(string id)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adpt = new SqlDataAdapter("select * from Appointments as a inner join Doctors as d on a.DoctId=d.DoctId inner join Patients as p on a.PatId=p.PatId   where a.DoctId='" + id + "' and a.flag=0", con);
                adpt.Fill(ds, "apt");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;
        }
        public DataSet ViewPrescription(string id,string aid)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adpt = new SqlDataAdapter("select a.AppointmentId,p.PatName,p.Gender,p.phonenumber,p.age,p.bloodgrp,a.disease,a.Apptime,a.Appdate,a.diagnosis,a.medicine from Appointments as a inner join Doctors as d on a.DoctId=d.DoctId inner join Patients as p on a.PatId=p.PatId  where a.DoctId='" + id+ "' and a.AppointmentId='"+aid+"' and a.flag=0", con);
                adpt.Fill(ds, "apt");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;
        }
        public DataSet PatientPrescriptions(string id, string aid)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adpt = new SqlDataAdapter("select a.AppointmentId,p.PatName,p.Gender,p.phonenumber,p.age,p.bloodgrp,a.disease,a.Apptime,a.Appdate,a.diagnosis,a.medicine from Appointments as a inner join Doctors as d on a.DoctId=d.DoctId inner join Patients as p on a.PatId=p.PatId  where a.DoctId='" + id + "' and a.AppointmentId='" + aid + "' and a.flag=1", con);
                adpt.Fill(ds, "apt");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;
        }
        public bool Update(Appointments a)
        {
            bool b = false;
            try
            {
                SqlDataAdapter adpt = new SqlDataAdapter("update Appointments set diagnosis='" + a.diagnosis + "',medicine='" + a.medicine + "',flag=1 where AppointmentId='"+a.AppointmentId+"'", con);
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
        public bool Edit(string id, Prescription p)
        {
            Appointments a = new Appointments();
            a.AppointmentId = id;
            a.diagnosis = p.diagnosis;
            a.medicine = p.medicine;
            bool b = Update(a);
            return b;
        }
        public bool AddPrescription(Appointments a)
        {
            bool b = false;
            try
            {
                //create table Appointments(AppointmentId varchar(10) primary key, PatId varchar(5),foreign key(PatId) references Patients,DoctId varchar(5),foreign key(DoctId) references Doctors,
                //disease varchar(50),AppDate date)
                con.Open();
                SqlCommand cmd = new SqlCommand("update Appointments set diagnosis='"+a.diagnosis+"',medicine='"+a.medicine+"',flag=1", con);
                                
                cmd.ExecuteNonQuery();
                b = true;
            }
            catch (Exception ex)
            {
                b = false;
            }

            return b;
        }
        public DataSet EditPrescription(string id, string aid)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adpt = new SqlDataAdapter("select a.AppointmentId,p.PatName,p.Gender,p.phonenumber,p.age,p.bloodgrp,a.disease,a.Apptime,a.Appdate,a.diagnosis,a.medicine from Appointments as a inner join Doctors as d on a.DoctId=d.DoctId inner join Patients as p on a.PatId=p.PatId  where a.DoctId='" + id + "' and a.AppointmentId='" + aid + "' and a.flag=1", con);
                adpt.Fill(ds, "apt");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;
        }
        public IEnumerable<Appointments> GetAPIDData()
        {
            SqlDataAdapter data = new SqlDataAdapter("select * from Appointments  ", con);
            DataSet ds = new DataSet();
            data.Fill(ds, "spcl");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Appointments s = new Appointments();
                s.AppointmentId = (ds.Tables[0].Rows[i]["AppointmentId"].ToString());

                alst.Add(s);
            }
            return alst;
        }
        public IEnumerable<Appointments> GetPatData()
        {
            SqlDataAdapter data = new SqlDataAdapter("select * from Appointments as a inner join Doctors as d on a.DoctId=d.DoctId inner join Patients as p on a.PatId=p.PatId inner join Prescriptions as pr on pr.AppointmentId=a.AppointmentId ", con);
            DataSet ds = new DataSet();
            data.Fill(ds, "spcl");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Appointments s = new Appointments();
                s.PatId = (ds.Tables[0].Rows[i]["PatId"].ToString());
                s.PatName = (ds.Tables[0].Rows[i]["PatName"].ToString());
                plst.Add(s);
            }
            return plst;
        }
        
   
    }

}