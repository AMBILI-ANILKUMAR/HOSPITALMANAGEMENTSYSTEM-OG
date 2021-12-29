using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HOSPITALMANAGEMENTSYSTEM.Models
{
    public class Specializations
    {
        [Required(ErrorMessage = "spclId must be required")]
        public int spclId { get; set; }
        [Required(ErrorMessage = "Specialization is required")]
        public String specializationName { get; set; }
    }


    
}