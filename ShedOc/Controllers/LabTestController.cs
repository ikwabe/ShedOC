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
    public class LabTestController : Controller
    {
        // GET: LabTest
        public ActionResult Index()
        {
            if(Session["ID"] != null && Session["utid"].ToString() == "UT02")
            {
                var model = new Reports();
                model.labtest = getAllLabTest();

                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        private DataTable getAllLabTest()
        {
            var fdate = Request["fromDate"];
            var tdate = Request["toDate"];
            int total = 0;
            DataTable test = new DataTable();
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = Login.connection;

                string getAlltest = "select name 'TEST NAME',typeid 'TOTAL NUMBER OF TEST' from laboratory_tests_master";
                MySqlCommand testcom = new MySqlCommand(getAlltest, con);
                DataTable numTab = new DataTable();

                MySqlDataAdapter da;

                try
                {
                    con.Open();
                    da = new MySqlDataAdapter(testcom);
                    da.Fill(test);

                    if (test.Rows.Count > 0)
                    {
                        
                        for (int i =0; i<test.Rows.Count;i++)
                        {

                            MySqlCommand com3 = new MySqlCommand("SELECT patient_assign.PID, patient_assign.patientName FROM patient_assign, laboratorytestrecords WHERE laboratorytestrecords.labtest = @labtest " +
                                "and laboratorytestrecords.status = 'Tested' and laboratorytestrecords.date1 <= @tdate and laboratorytestrecords.date1 >=@fdate and laboratorytestrecords.pID = patient_assign.pID",con);
                            com3.Connection = con;
                            com3.Parameters.AddWithValue("@tdate", tdate);
                            com3.Parameters.AddWithValue("@fdate", fdate);
                            com3.Parameters.AddWithValue("@labtest", test.Rows[i][0].ToString());
                            da = new MySqlDataAdapter(com3);
                            da.Fill(numTab);

                            DataTable table = new DataTable();
                            da.Fill(table);

                            test.Rows[i][1] = table.Rows.Count;

                            total += int.Parse(test.Rows[i][1].ToString());
                        }

                        ViewBag.TestCount = "Total number of tests done is " + total.ToString();
                        ViewBag.from = fdate;
                        ViewBag.tdate = tdate;
                        //counting patients
                        int numcount = 0;

                        DataTable distinctTable = numTab.DefaultView.ToTable(true);

                        for (int j = 0; j < distinctTable.Rows.Count; j++)
                        {
                            numcount++;

                        }

                        ViewBag.LabMessage = "Total number of people done test is "+ numcount.ToString();
                    }
                    else
                    {
                        ViewBag.LabMessage = string.Format("NO REPORT FOUND FROM DATE " + fdate + " TO DATE " + tdate + " TRY TO CHANGE DATE");
                    }

                }
                catch
                {
                    ViewBag.LabMessage = "Network Error";
                }
            }

            return test;
        }
        // GET: LabTest/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LabTest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LabTest/Create
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

        // GET: LabTest/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LabTest/Edit/5
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

        // GET: LabTest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LabTest/Delete/5
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
