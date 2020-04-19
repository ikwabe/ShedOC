using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShedOc.Models
{
    public class Chechin
    {

        [DisplayName("Search Name Here")]
        public string searchFullname { get; set; }

        public DataTable Names { get; set; }
        [DisplayName("Patient Name")]
        public  string PatientName { get; set; }
        public DateTime Age { get; set; }
        [DisplayName("File Number")]
        public string filenumber { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public  string ClinicVisit { get; set; }
        public string DoctorAssigned { get; set; }
        public  string Scheme { get; set; }
        public DataTable Schemes { get; set; }
        public string FormNumber { get; set; }
        public string MembershipID { get; set; }
        public  double Temperature { get; set; }
        public  double Pulse { get; set; }
        public  double Respiration { get; set; }
        public  double Oxygen { get; set; }
        public  double BPSystolic { get; set; }
        public  double BPDiastolic { get; set; }
        public  double Weight { get; set; }
        public  double Height { get; set; }
        public  double BMI { get; set; }

        public IList<SelectListItem> Doctors { get; set; }
        public IList<SelectListItem> Clinics { get; set; }
        public IList<SelectListItem> schemes { get; set; }

        public Chechin() {
            Names = new DataTable();
            Doctors = new List<SelectListItem>();
            Clinics = new List<SelectListItem>();
            schemes = new List<SelectListItem>();
        }

    }
}