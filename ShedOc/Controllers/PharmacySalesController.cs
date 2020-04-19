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
    public class PharmacySalesController : Controller
    {
        // GET: PharmacySales
        public ActionResult Index()
        {
            if(Session["ID"] != null && Session["utid"].ToString() == "UT02")
            {
                var model = new Reports();
                model.pharmacySales = getSalesReport();
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
        }

        private DataTable getSalesReport()
        {

            var fdate = Request["fromDate"];
            var tdate = Request["toDate"];
            double totaldrugpurchase = 0;
            double totaldrugsale = 0;
            DataTable table = new DataTable();

            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = Login.connection;


                    MySqlDataAdapter da;

                try
                {
                    con.Open();
                    MySqlCommand com = new MySqlCommand("Select  drugs_master.drugname 'DRUG NAME',drugssales.quantity 'SOLD QNTY',drugs_master.pricepersingle 'BUYING PRICE PER ONE', drugssales.amountPerOne 'SELLING PRICE PER ONE'" +
                                ", (drugssales.quantity * drugs_master.pricepersingle) 'TOTAL BUYING AMOUNT', drugssales.totalAmount 'TOTAL SELLING AMOUNT', (drugssales.totalAmount - (drugs_master.pricepersingle * drugssales.quantity)) 'MARGIN CASH', drugssales.seller SELLER, " +
                                "drugssales.date1 as DATE from drugssales,drugs_master where drugssales.date1 >= @fdate and drugssales.date1 <= @tdate AND drugssales.drugcode = drugs_master.drugcode ", con);
                    com.Parameters.AddWithValue("@fdate", fdate);
                    com.Parameters.AddWithValue("@tdate", tdate);
                    da = new MySqlDataAdapter(com);
                    da.Fill(table);

                    MySqlCommand com1 = new MySqlCommand("select pharmacydummy.drugname 'DRUG NAME', pharmacydummy.totalDrugs 'SOLD QNTY',drugs_master.pricepersingle 'BUYING PRICE PER ONE', drugs_master.pricepersingle 'SELLING PRICE PER ONE'," +
                                        "(pharmacydummy.totalDrugs * drugs_master.pricepersingle) 'TOTAL BUYING AMOUNT',pharmacydummy.price  'TOTAL SELLING AMOUNT',(pharmacydummy.price - (drugs_master.pricepersingle * pharmacydummy.totalDrugs)) 'MARGIN CASH',pharmacydummy.seller SELLER, " +
                                "pharmacydummy.date1 as DATE from pharmacydummy,drugs_master where " +
                                "DATE(pharmacydummy.date1) >= @fdate and DATE(pharmacydummy.date1) <= @tdate and pharmacydummy.STATUS = 'Complete' and pharmacydummy.drugname = drugs_master.drugname ", con);
                    com1.Parameters.AddWithValue("@fdate", fdate);
                    com1.Parameters.AddWithValue("@tdate", tdate);
                    da = new MySqlDataAdapter(com1);
                    da.Fill(table);

                    if (table.Rows.Count > 0)
                    {
                        for (int j = 0; j < table.Rows.Count; j++)
                        {

                            totaldrugpurchase += double.Parse(table.Rows[j]["TOTAL BUYING AMOUNT"].ToString());
                            totaldrugsale += double.Parse(table.Rows[j]["TOTAL SELLING AMOUNT"].ToString());

                        }

                        ViewBag.PurchaseMessage = string.Format("The Total Drug Purchase is {0:n} for the specified date range.", totaldrugpurchase);
                        ViewBag.SalesMessage = string.Format("The Total Drug Sale is {0:n} for the specified date range.", totaldrugsale);
                        ViewBag.MarginMessage = string.Format("The Total Margin(Profit/Loss) is {0:n} for the specified date range.", (totaldrugsale - totaldrugpurchase));
                        ViewBag.from = fdate;
                        ViewBag.tdate = tdate;
                    }
                    else
                    {
                        ViewBag.PurchaseMessage = string.Format("NO REPORT FOUND FROM DATE " + fdate + " TO DATE " + tdate + " TRY TO CHANGE DATE");
                        ViewBag.SalesMessage = "";
                        ViewBag.MarginMessage = "";
                        
                    }


                }
                catch(MySqlException ex)
                {
                    ViewBag.SalesMessage = "Network Error  "+ ex;
                }
                   
            }

            return table;
        }
        // GET: PharmacySales/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PharmacySales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PharmacySales/Create
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

        // GET: PharmacySales/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PharmacySales/Edit/5
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

        // GET: PharmacySales/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PharmacySales/Delete/5
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
