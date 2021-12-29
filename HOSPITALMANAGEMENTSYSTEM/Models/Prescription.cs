using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HOSPITALMANAGEMENTSYSTEM.Models
{
    public class Prescription
    {
        [Required(ErrorMessage = "Appointment Id is required")]

        public string AppointmentId { get; set; }
        [Required(ErrorMessage = "Patient Id is required")]
        public string PatId { get; set; }
        [Required(ErrorMessage = "Patient  is required")]
        public string PatName { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phonenumber is required")]
        public string phonenumber { get; set; }

        [Required(ErrorMessage = "Age must be required")]
        public int age { get; set; }

        [Required(ErrorMessage = "Bloodgroup must be required")]
        public string bloodgrp { get; set; }

        [Required(ErrorMessage = "Doctor Id is required")]
        public string DoctId { get; set; }
        [Required(ErrorMessage = "Doctor  is required")]
        public string DoctName { get; set; }
        [Required(ErrorMessage = "Disease is required")]
        public string disease { get; set; }
        [Required(ErrorMessage = "Date is required")]

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Time is required")]
        public string AppTime { get; set; }
        [Required(ErrorMessage = "Specialization is required")]
        public int spclId { get; set; }
        [Required(ErrorMessage = "Diagnosis  is required")]
        public string diagnosis { get; set; }
        [Required(ErrorMessage = "Medicine  is required")]
        public string medicine { get; set; }

        [Required(ErrorMessage = "Patient Name is required")]
        [NotMapped]
        public SelectList PatDropdown { get; set; }
        [Required(ErrorMessage = "Doctor Name is required")]
        [NotMapped]
        public SelectList DocDropdown { get; set; }
        [NotMapped]
        public SelectList ApIDDropdown { get; set; }
    }
    

}