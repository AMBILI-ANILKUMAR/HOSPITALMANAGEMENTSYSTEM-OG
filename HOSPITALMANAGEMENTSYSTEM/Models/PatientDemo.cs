using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HOSPITALMANAGEMENTSYSTEM.Models
{
    public class PatientDemo
    {

        [Required(ErrorMessage = "Patient Id is required")]
        public string PatId { get; set; }

        [Required(ErrorMessage = "Patient Name is required")]
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
        //public bgrp bloodgroup { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }
    }
}