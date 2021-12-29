using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace HOSPITALMANAGEMENTSYSTEM.Models
{
    public class Doctors
    {
        [Required(ErrorMessage = "Doctor Id is required")]
        public string DoctId { get; set; }
        [Required(ErrorMessage = "Doctor Name is required")]
        public string DoctName { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        public string phonenumber { get; set; }
        [Required(ErrorMessage = "Age is required")]
        public int age { get; set; }
        [Required(ErrorMessage = "Specialization is required")]
        public int spclId { get; set; }
        [Required(ErrorMessage = "Specialization Name is required")]
        public string specializationName { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }
        [Required(ErrorMessage = "Specialization is required")]
        [NotMapped]
        public SelectList SpclDropdown { get; set; }
        public string fullName { get { return DoctName + " (" + specializationName + " )"; } }
        
    }
    public enum gen
    {
        Male,
        Female
    }
}