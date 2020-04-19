using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using ShedOc.Models;

namespace ShedOc.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        public ActionResult Index()
        {
            if (Session["ID"] != null && Session["utid"].ToString() == "UT01")
            {
                var model = new DoctorTreatment();
                model.labTest = LabTest();
                model.dignosis = Diagnosis();
                model.patientAssigned = PatientAssigned();
           
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        public DataTable LabTest()
        {
            DataTable tab = new DataTable();
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = Login.connection;
                string unit = " select name from  laboratory_tests_master ";

                MySqlCommand com = new MySqlCommand(unit, con);

                try
                {
                    con.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(com);
                    da.Fill(tab);
                    da.Dispose();
                }
                catch
                {

                }
                con.Close();
            }

            return tab;
        }

        public DataTable PatientAssigned()
        {
            DataTable tab = new DataTable();
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = Login.connection;
                
                string loadInf = "select * from patient_assign where status = 'Assigned' and doctorAssigned = '" + Login.uname + "'";

                MySqlCommand com = new MySqlCommand(loadInf, con);

                try
                {
                    con.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(com);
                    da.Fill(tab);
                    da.Dispose();
                }
                catch
                {

                }
                con.Close();
            }

            return tab;
        }
        public DataTable Diagnosis()
        {
            DataTable tab = new DataTable();
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = Login.connection;
                string unit = " select diseasename from  diseases_master ";

                MySqlCommand com = new MySqlCommand(unit, con);

                try
                {
                    con.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(com);
                    da.Fill(tab);
                    da.Dispose();
                }
                catch
                {

                }
                con.Close();
            }

            return tab;
        }
        // GET: Doctor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Doctor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Doctor/Create
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

        // GET: Doctor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Doctor/Edit/5
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

        // GET: Doctor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Doctor/Delete/5
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
