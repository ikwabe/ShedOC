using MySql.Data.MySqlClient;
using ShedOc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShedOc.Controllers
{
    public class CheckInController : Controller
    {

    
        public ActionResult Index()
        {
            if (Session["ID"] != null && Session["utid"].ToString() == "UT04")
            {
                var model = new Chechin();
                model.Names = GetNames(Request["searchFullname"]);
                model.schemes = GetSchemes();
                model.Doctors = GetDoctors();
                model.Clinics = GetClinic();
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public DataTable GetNames(string searchData)
        {
            DataTable table = new DataTable();
            if (string.IsNullOrWhiteSpace(searchData))
            {

            }
            else
            {
                MySqlConnection con = new MySqlConnection();
                con.ConnectionString = Login.connection;
                string clinic = "select recordID,fullname,Gender,Birthdate1,Street,TIMESTAMPDIFF(YEAR, Birthdate1, now()) as Y," +
                                        "TIMESTAMPDIFF(MONTH, Birthdate1, now()) % 12 as M,FLOOR(TIMESTAMPDIFF(DAY, Birthdate1, now()) % 30.4375) as D from patients where fullname like '%" + searchData + "%'";
                MySqlCommand com = new MySqlCommand(clinic, con);
                MySqlDataAdapter ad;

                try
                {
                    con.Open();
                    ad = new MySqlDataAdapter(com);
                    ad.Fill(table);
                    ad.Dispose();

                }
                catch
                {

                }


            }

            return table;
        }

        private IList<SelectListItem> GetClinic()
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = Login.connection;
            string data = "select Clinicname from clinics_master order by Clinicname";
            MySqlCommand com = new MySqlCommand(data, con);
            MySqlDataAdapter ad;
            DataTable table = new DataTable();
            List<SelectListItem> dataresult = new List<SelectListItem>();

            try
            {
                con.Open();
                ad = new MySqlDataAdapter(com);
                ad.Fill(table);
                ad.Dispose();

                if (table.Rows.Count > 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        dataresult.Add(new SelectListItem { Text = table.Rows[i][0].ToString(), Value = table.Rows[i][0].ToString() });
                    }

                }


            }
            catch
            {

            }



            return dataresult;
        }

        private IList<SelectListItem> GetDoctors()
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = Login.connection;
            string data = "select username from users where titlecode = 'UT01' order by username";
            MySqlCommand com = new MySqlCommand(data, con);
            MySqlDataAdapter ad;
            DataTable table = new DataTable();
            List<SelectListItem> dataresult = new List<SelectListItem>();

            try
            {
                con.Open();
                ad = new MySqlDataAdapter(com);
                ad.Fill(table);
                ad.Dispose();

                if (table.Rows.Count > 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        dataresult.Add(new SelectListItem { Text = table.Rows[i][0].ToString(), Value = table.Rows[i][0].ToString() });
                    }

                }


            }
            catch
            {

            }



            return dataresult;
        }

        private IList<SelectListItem> GetSchemes()
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = Login.connection;
            string data = "SELECT name FROM  insurances order by name";
            MySqlCommand com = new MySqlCommand(data, con);
            MySqlDataAdapter ad;
            DataTable table = new DataTable();
            List<SelectListItem> dataresult = new List<SelectListItem>();

            try
            {
                con.Open();
                ad = new MySqlDataAdapter(com);
                ad.Fill(table);
                ad.Dispose();

                if (table.Rows.Count > 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        dataresult.Add(new SelectListItem { Text = table.Rows[i][0].ToString(), Value = table.Rows[i][0].ToString() });
                    }

                }


            }
            catch
            {

            }



            return dataresult;
        }
        // GET: CheckIn/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CheckIn/Create
        public ActionResult Create()
        {
            return View();
        }

       
        private ActionResult Checkin()
        {
            if (Request["filenumber"] == null)
            {
                ViewBag.Message = "File number is empty";
            }
            else
            {
                using (MySqlConnection con = new MySqlConnection())
                {

                    con.ConnectionString = Login.connection;

                    if (Request["PaymentType"] == "Insurance")
                    {

                        MySqlCommand com = new MySqlCommand("insert into patient_assign(patientName " +
                            ",clinicVisit" +
                            ",paymentType" +
                            ",scheme" +
                            ",recordID" +
                            ",Gender" +
                            ",Age1" +
                            ",address" +
                            ",doctorAssigned" +
                            ",TimeAssigned2" +
                            ",Status" +
                            ",temperature" +
                            ",pulse" +
                            ",respiration" +
                            ",oxygen" +
                            ",BPsystolic" +
                            ",BPDiastolic" +
                            ",weight" +
                            ",height" +
                            ",BMI,formNumber,MembershipID) Values(@patientName " +
                            ",@clinicVisit" +
                            ",@paymentType" +
                            ",@scheme" +
                            ",@recordID" +
                            ",@Gender" +
                            ",@Age" +
                            ",@address" +
                            ",@doctorAssigned" +
                            ",@TimeAssigned" +
                            ",'Assigned'" +
                            ",@temperature" +
                            ",@pulse" +
                            ",@respiration" +
                            ",@oxygen" +
                            ",@BPsystolic" +
                            ",@BPDiastolic" +
                            ",@weight" +
                            ",@height" +
                            ",@BMI,@formNumber,@MembershipID)", con);

                        com.Parameters.AddWithValue("@patientName", Request["PatientName"]);
                        com.Parameters.AddWithValue("@clinicVisit", Request["ClinicVisit"]);
                        com.Parameters.AddWithValue("@paymentType", Request["PaymentType"]);
                        com.Parameters.AddWithValue("@scheme", Request["Scheme"]);
                        com.Parameters.AddWithValue("@recordID", Request["filenumber"]);
                        com.Parameters.AddWithValue("@Gender", Request["Gender"]);
                        com.Parameters.AddWithValue("@Age", Request["Age"]);
                        com.Parameters.AddWithValue("@address", Request["Address"]);
                        com.Parameters.AddWithValue("@doctorAssigned", Request["DoctorAssigned"]);
                        com.Parameters.AddWithValue("@TimeAssigned", DateTime.Now);
                        com.Parameters.AddWithValue("@temperature", Request["Temperature"]);
                        com.Parameters.AddWithValue("@pulse", Request["Pulse"]);
                        com.Parameters.AddWithValue("@respiration", Request["Respiration"]);
                        com.Parameters.AddWithValue("@oxygen", Request["Oxygen"]);
                        com.Parameters.AddWithValue("@BPsystolic", Request["BPsystolic"]);
                        com.Parameters.AddWithValue("@BPDiastolic", Request["BPDiastolic"]);
                        com.Parameters.AddWithValue("@weight", Request["Weight"]);
                        com.Parameters.AddWithValue("@height", Request["Height"]);
                        com.Parameters.AddWithValue("@BMI", Request["BMI"]);
                        com.Parameters.AddWithValue("@formNumber", Request["FormNumber"]);
                        com.Parameters.AddWithValue("@MembershipID", Request["MembershipID"]);

                        try
                        {
                            con.Open();
                            com.ExecuteNonQuery();

                            if (Request["PaymentType"] == "EAGT")
                            {

                            }
                            else
                            {
                                //SpecialFunction.initialInsurancePayment(Request["filenumber"], DateTime.Today);
                            }

                            ViewBag.Message = "Patient Addressed Successful";

                        }
                        catch
                        {
                            ViewBag.Message = "Sorry Something went wrong";
                        }
                    }

                    //if the patient has no insurance
                    else if (Request["PaymentType"] == "Cash Payment")
                    {

                        MySqlCommand com = new MySqlCommand("insert into patient_assign(patientName " +
                            ",clinicVisit" +
                            ",paymentType" +
                            ",scheme" +
                            ",recordID" +
                            ",Gender" +
                            ",Age1" +
                            ",address" +
                            ",doctorAssigned" +
                            ",TimeAssigned2" +
                            ",Status" +
                            ",temperature" +
                            ",pulse" +
                            ",respiration" +
                            ",oxygen" +
                            ",BPsystolic" +
                            ",BPDiastolic" +
                            ",weight" +
                            ",height" +
                            ",BMI) Values(@patientName " +
                            ",@clinicVisit" +
                            ",@paymentType" +
                            ",'Cash'" +
                            ",@recordID" +
                            ",@Gender" +
                            ",@Age" +
                            ",@address" +
                            ",@doctorAssigned" +
                            ",@TimeAssigned" +
                            ",'Cashier'" +
                            ",@temperature" +
                            ",@pulse" +
                            ",@respiration" +
                            ",@oxygen" +
                            ",@BPsystolic" +
                            ",@BPDiastolic" +
                            ",@weight" +
                            ",@height" +
                            ",@BMI)", con);

                        com.Parameters.AddWithValue("@patientName", Request["PatientName"]);
                        com.Parameters.AddWithValue("@clinicVisit", Request["ClinicVisit"]);
                        com.Parameters.AddWithValue("@paymentType", Request["PaymentType"]);
                        com.Parameters.AddWithValue("@recordID", Request["filenumber"]);
                        com.Parameters.AddWithValue("@Gender", Request["Gender"]);
                        com.Parameters.AddWithValue("@Age", Request["Age"]);
                        com.Parameters.AddWithValue("@address", Request["Address"]);
                        com.Parameters.AddWithValue("@doctorAssigned", Request["DoctorAssigned"]);
                        com.Parameters.AddWithValue("@TimeAssigned", DateTime.Now);
                        com.Parameters.AddWithValue("@temperature", Request["Temperature"]);
                        com.Parameters.AddWithValue("@pulse", Request["Pulse"]);
                        com.Parameters.AddWithValue("@respiration", Request["Respiration"]);
                        com.Parameters.AddWithValue("@oxygen", Request["Oxygen"]);
                        com.Parameters.AddWithValue("@BPsystolic", Request["BPsystolic"]);
                        com.Parameters.AddWithValue("@BPDiastolic", Request["BPDiastolic"]);
                        com.Parameters.AddWithValue("@weight", Request["Weight"]);
                        com.Parameters.AddWithValue("@height", Request["Height"]);
                        com.Parameters.AddWithValue("@BMI", Request["BMI"]);

                        try
                        {
                            con.Open();
                            com.ExecuteNonQuery();

                            ViewBag.Message = "Patient Addressed Successful";

                        }
                        catch
                        {
                            ViewBag.Message = "Sorry Something went wrong";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Please select the appropriate payment type and scheme";
                    }
                }
            }

            return RedirectToAction("Index");
        }
        // POST: CheckIn/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
               
                
                // TODO: Add insert logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CheckIn/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CheckIn/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CheckIn/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CheckIn/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
