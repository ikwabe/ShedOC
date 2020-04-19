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
    public class SalesReportController : Controller
    {
        // GET: SalesReport

        public ActionResult Index()
        {
            if (Session["ID"] != null && Session["utid"].ToString() == "UT02")
            {
                
                var model = new Reports();
                model.sales = viewSalesReport();
                model.moneyreturned = ReturnedMoneys();
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        

        double totalcost = 0;
        public DataTable viewSalesReport()
        {
            var fdate = Request["fromDate"];
            var tdate = Request["toDate"];
            var showAs = Request["viewas"];
            DataTable Sales = new DataTable();
            totalcost = 0;
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = Login.connection;

                DataTable datetable = new DataTable();
                DataTable Moneydatetable = new DataTable();

                DataTable table = new DataTable();
                DataTable Moneytable = new DataTable();
                DataTable shortDesc = new DataTable();
                MySqlDataAdapter da;

                try
                {
                    con.Open();

                    MySqlCommand com = new MySqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.Text;
                    com.CommandText = "Select  paid 'CASH PAID',paymentFor 'PAID FOR', DATE(date1) as DATE ,approvedBy 'APPROVE BY', CATEGORY from cashpayments where date1 <= @tdate and date1>=@fdate  ";
                    com.Parameters.AddWithValue("@fdate",fdate );
                    com.Parameters.AddWithValue("@tdate", tdate);

                    da = new MySqlDataAdapter(com);
                    da.Fill(table);
                    //da.Dispose();

                    if (table.Rows.Count > 0)
                    {
                        shortDesc.Columns.Add("CASH PAID");
                        shortDesc.Columns.Add("PAID FOR");

                        double initialTotal = 0.00;
                        double registration = 0.00;
                        double laboratory = 0.00;
                        double pharmacy = 0.00;
                        double ultrasound = 0.00;

                        foreach (DataRow row in table.Rows)
                        {
                            if (row["CATEGORY"].ToString().Contains("Initial"))
                                initialTotal += double.Parse(row["CASH PAID"].ToString());
                            else if (row["CATEGORY"].ToString().Contains("Pharmacy"))
                                pharmacy += double.Parse(row["CASH PAID"].ToString());
                            else if (row["CATEGORY"].ToString().Contains("Laboratory"))
                                laboratory += double.Parse(row["CASH PAID"].ToString());
                            else if (row["CATEGORY"].ToString().Contains("Registration"))
                                registration += double.Parse(row["CASH PAID"].ToString());
                            else if (row["CATEGORY"].ToString().Contains("Ultrasound"))
                                ultrasound += double.Parse(row["CASH PAID"].ToString());
                        }

                        DataRow r1 = shortDesc.NewRow();
                        DataRow r2 = shortDesc.NewRow();
                        DataRow r3 = shortDesc.NewRow();
                        DataRow r4 = shortDesc.NewRow();
                        DataRow r5 = shortDesc.NewRow();

                        r1["CASH PAID"] = registration;
                        r1["PAID FOR"] = "Registration Payments";
                        shortDesc.Rows.Add(r1);

                        r2["CASH PAID"] = initialTotal;
                        r2["PAID FOR"] = "Initial Payments";
                        shortDesc.Rows.Add(r2);

                        r3["CASH PAID"] = laboratory;
                        r3["PAID FOR"] = "Laboratory Payments";
                        shortDesc.Rows.Add(r3);

                        r4["CASH PAID"] = pharmacy;
                        r4["PAID FOR"] = "Pharmacy Payments";
                        shortDesc.Rows.Add(r4);

                        r5["CASH PAID"] = ultrasound;
                        r5["PAID FOR"] = "Ultrasound Payments";
                        shortDesc.Rows.Add(r5);

                        for (int j = 0; j < table.Rows.Count; j++)
                        {

                            totalcost += double.Parse(table.Rows[j][0].ToString());


                        }

                        ViewBag.SalesMessage = string.Format("The Total Sales is {0:n} for the specified date range.",totalcost);
                        ViewBag.from = fdate;
                        ViewBag.tdate = tdate;

                        if (showAs == "Short")
                            Sales = shortDesc;
                        else
                            Sales = table;
                    }
                    else
                    {
                        ViewBag.EmptyMessage = string.Format("NO REPORT FOUND FROM DATE "+ fdate + " TO DATE "+ tdate + " TRY TO CHANGE DATE");
                    }

                }
                catch
                {

                }

            }

            return Sales;
        }

        public DataTable ReturnedMoneys()
        {
            var fdate = Request["fromDate"];
            var tdate = Request["toDate"];
            DataTable ReturnedMoney = new DataTable();
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = Login.connection;

                DataTable datetable = new DataTable();
                DataTable Moneydatetable = new DataTable();

                DataTable table = new DataTable();
                DataTable Moneytable = new DataTable();
                DataTable shortDesc = new DataTable();
                MySqlDataAdapter da;

                try
                {
                    con.Open();

                    MySqlCommand com = new MySqlCommand();
                    com.Connection = con;
                    com.CommandType = CommandType.Text;
                    com.CommandText = "Select patient_assign.patientName 'Returned To', returnedmoney.Amount,  users.username 'Returned by', returnedmoney.date 'Returned Date', returnedmoney.Reason " +
                                   "from returnedmoney,patient_assign,users where returnedmoney.pid = patient_assign.pid and returnedmoney.userID =  users.ID and DATE(returnedmoney.date) <= @tdate and DATE(returnedmoney.date) >= @fdate   ";

                    com.Parameters.AddWithValue("@fdate", fdate);
                    com.Parameters.AddWithValue("@tdate", tdate);

                    da = new MySqlDataAdapter(com);
                    da.Fill(Moneytable);
                    //da.Dispose();

                    if (Moneytable.Rows.Count > 0)
                    {

                        double total = 0;

                        for (int i = 0; i < Moneytable.Rows.Count; i++)
                        {
                            total += double.Parse(Moneytable.Rows[i][1].ToString());
                        }

                        DataRow r = Moneytable.NewRow();

                        r["Returned To"] = "TOTAL AMOUNT RETURNED";
                        r["Amount"] = string.Format("{0:n}", total);
                        r["Returned by"] = " ";
                        r["Returned Date"] = DBNull.Value;

                        Moneytable.Rows.Add(r);
                    }

                    ReturnedMoney = Moneytable;
                }
                catch
                {

                }

            }

            return ReturnedMoney;
        }

        // GET: SalesReport/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SalesReport/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalesReport/Create
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

        // GET: SalesReport/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SalesReport/Edit/5
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

        // GET: SalesReport/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SalesReport/Delete/5
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
