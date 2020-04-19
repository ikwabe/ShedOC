using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShedOc.Models
{
    public class DummyTick
    {
       
        [DisplayName("Search Name Here")]
        public string searchFullname { get; set; }

        [DisplayName("File Number")]
        public string fileNumber { get; set; }

        [DisplayName("Full Name")]
        public string fullname { get; set; }

        [DisplayName("Payment Type")]
        public string paymentType { get; set; }

        public int ID { get; set; }
       
        public IList<SelectListItem> Tests { get; set; }

        public DataTable Names { get; set; }  

        public DummyTick() 
        {
            Tests = new List<SelectListItem>();
        }  



    }
}