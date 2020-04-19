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
    public class PatientVisitController : Controller
    {
        // GET: PatientVisit
        public ActionResult Index()
        {
            if (Session["ID"] != null && Session["utid"].ToString() == "UT02")
            {
                var model = new Reports();
                model.patientVisit = getReport();
                return View(model);
            }
            else
                return RedirectToAction("Index", "Home");
        }

        private DataTable getReport()
        {
            DataTable table = new DataTable();
            var fdate = Request["fromDate"];
            var tdate = Request["toDate"];
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = Login.connection;
                MySqlDataAdapter da;
                try
                {
                    con.Open();

                    MySqlCommand com = new MySqlCommand("Select PID 'S/NO', recordID 'FILE NO', patientName 'PATIENT NAME',GENDER SEX," +
                                        "TIMESTAMPDIFF(YEAR, Age1, now()) as Y," +
                                        "TIMESTAMPDIFF(MONTH, Age1, now()) % 12 as M,FLOOR(TIMESTAMPDIFF(DAY, Age1, now()) % 30.4375) as D , ADDRESS,clinicVisit 'CLINIC VISIT'," +
                                        "paymentType 'PAYMENT TYPE',SCHEME,MembershipID 'MEMBERSHIP ID',DATE(TimeAssigned2) as DATE FROM  patient_assign " +
                                        "WHERE DATE(TimeAssigned2) >= @fdate and DATE(TimeAssigned2) <= @tdate", con);
                    com.Parameters.AddWithValue("@fdate", fdate);
                    com.Parameters.AddWithValue("@tdate", tdate);

                    da = new MySqlDataAdapter(com);
                    da.Fill(table);
                    //da.Dispose();

                    if (table.Rows.Count > 0)
                    {
                       
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            table.Rows[i][0] = i + 1;
                        }
                        ViewBag.PatientMessage = string.Format("Total patient visit number is {0} for the specified date range.", table.Rows.Count);
                        ViewBag.from = fdate;
                        ViewBag.tdate = tdate;
                    }
                    else
                    {
                        ViewBag.PatientMessage = string.Format("NO REPORT FOUND FROM DATE " + fdate + " TO DATE " + tdate + " TRY TO CHANGE DATE");
                    }


                }
                catch
                {
                    ViewBag.PatientMessage = "Network Error";
                }
                //con.Close();

            }
            return table;
        }
        // GET: PatientVisit/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PatientVisit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientVisit/Create
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

        // GET: PatientVisit/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PatientVisit/Edit/5
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

        // GET: PatientVisit/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PatientVisit/Delete/5
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
