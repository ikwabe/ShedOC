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
    public class PatientsController : Controller
    {
        // GET: Patients
        public ActionResult Index()
        {
            if (Session["ID"] != null && Session["utid"].ToString() == "UT04")
            {
                var model = new Patients();
                model.Districts = GetDistrict();
                model.Occupations = GetOccupations();
                model.Regions = GetRegion();
                List<SelectListItem> gender = new List<SelectListItem>();
                gender.Add(new SelectListItem { Text = "Male", Value = "M", Selected = true });
                gender.Add(new SelectListItem { Text = "Female", Value = "F" });

                model.Genders = gender;

                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        #region Ilists
        private IList<SelectListItem> GetOccupations()
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = Login.connection;
            string occupation = "select occupationname from occupation_master order by occupationname";
            MySqlCommand com = new MySqlCommand(occupation, con);
            MySqlDataAdapter ad;
            DataTable table = new DataTable();
            List<SelectListItem> occupations = new List<SelectListItem>();

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
                        occupations.Add(new SelectListItem { Text = table.Rows[i][0].ToString().ToUpper(), Value = table.Rows[i][0].ToString().ToUpper() });
                    }

                }


            }
            catch
            {
                
            }


            return occupations;
        }
        private IList<SelectListItem> GetRegion()
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = Login.connection;
            string region = "select name from regions order by name";
            MySqlCommand com = new MySqlCommand(region, con);
            MySqlDataAdapter ad;
            DataTable table = new DataTable();
            List<SelectListItem> regions = new List<SelectListItem>();

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
                        regions.Add(new SelectListItem { Text = table.Rows[i][0].ToString().ToUpper(), Value = table.Rows[i][0].ToString().ToUpper() });
                    }

                }


            }
            catch
            {

            }

            return regions;
        }
        private IList<SelectListItem> GetDistrict()
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = Login.connection;
            string district = "select name from districts order by name";
            MySqlCommand com = new MySqlCommand(district, con);
            MySqlDataAdapter ad;
            DataTable table = new DataTable();
            List<SelectListItem> districts = new List<SelectListItem>();

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
                        districts.Add(new SelectListItem { Text = table.Rows[i][0].ToString().ToUpper(), Value = table.Rows[i][0].ToString().ToUpper() });
                    }

                }


            }
            catch
            {

            }

            return districts;
        }
        #endregion

        // GET: Patients/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        [HttpPost]
        public ActionResult Index(Patients collection)
        {
            string fullname = collection.Fullname;
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = Login.connection;
            MySqlCommand com = new MySqlCommand("insert into patients(Fullname,Gender,Birthdate1,District,Street,Occupationcode,status,registrationdate1,contacts,nextOfKin,region) " +
                    "values(@Fullname,@Gender,@Birthdate,@District,@Street,@Occupationcode,'INACTIVE',@registrationdate,@contacts,@nextOfKin,@region)", con);
            com.Parameters.AddWithValue("@Fullname", collection.Fullname.ToUpper());
            com.Parameters.AddWithValue("@Birthdate", collection.Birthdate.Date);
            com.Parameters.AddWithValue("@gender", collection.Gender.ToUpper());
            com.Parameters.AddWithValue("@region", collection.Region.ToUpper());
            com.Parameters.AddWithValue("@District", collection.District.ToUpper());
            com.Parameters.AddWithValue("@Street", collection.Street.ToUpper());
            com.Parameters.AddWithValue("@contacts", collection.Contacts);
            com.Parameters.AddWithValue("@nextOfKin", collection.NextOfKin.ToUpper());
            com.Parameters.AddWithValue("@Occupationcode", collection.Occupation.ToUpper());
            com.Parameters.AddWithValue("@registrationdate", DateTime.Today);

            try
            {
                // TODO: Add insert logic here
                con.Open();
                com.ExecuteNonQuery();
                ViewBag.Message = "Patient registered successful";
                return RedirectToAction("Check","CheckIn");
            }
            catch
            {
                ViewBag.Message = "Registration Failed";
                return View();
            }
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Patients/Edit/5
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

        // GET: Patients/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Patients/Delete/5
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
