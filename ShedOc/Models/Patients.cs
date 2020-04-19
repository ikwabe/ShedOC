using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShedOc.Models
{
    public class Patients
    {
        public string Fullname { get; set; }
        public IList<SelectListItem> Genders { get; set; }
        public string Gender { get; set; }
        public IList<SelectListItem> Districts { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public IList<SelectListItem> Occupations { get; set; }
        public string Occupation { get; set; }
        public int Contacts { get; set; }
        [DisplayName("Next of kin")]
        public string NextOfKin { get; set; }
        public IList<SelectListItem> Regions { get; set; }
        public string Region { get; set; }
        public DateTime Birthdate { get; set; }

    }
}