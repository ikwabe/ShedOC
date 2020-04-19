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
    public class PurchasesController : Controller
    {
        // GET: Purchases
        public ActionResult Index()
        {
            if(Session["ID"] != null && Session["utid"].ToString() == "UT02")
            { 
            var model = new Reports();
            model.purchase = getPurchaseReport();
            return View(model);
            }
            else
            return RedirectToAction("Index", "Home");
        }


        private DataTable getPurchaseReport()
        {
            var fdate = Request["fromDate"];
            var tdate = Request["toDate"];
            double totaldrugpurchase = 0;
            DataTable table = new DataTable();

            using (MySqlConnection con = new MySqlConnection())
            {
               con.ConnectionString = Login.connection;

                MySqlDataAdapter da;

                try
                {
                    con.Open();

                    MySqlCommand com = new MySqlCommand("Select purchases.recid, drugs_master.drugname 'ITEM NAME',purchases.balance 'PREVIOUS STOCK (PS)',purchases.qtyreceived 'QUANTITY RECEIVED(QR)'," +
                                "purchases.iu 'INITIAL UNIT(IU)', (purchases.qtyreceived * purchases.iu) 'PRESENT STOCK(PR) ((QRxIU)+PS)', purchases.initialprice 'INITIAL PRICE(IP)', purchases.totalamount 'TOTAL AMOUNT((PR-PS)xIP)', " +
                                "purchases.grn GRN, purchases.grndate1 'GRN DATE', purchases.ordernumber 'ORDER NUMBER', purchases.orderdate1 'ORDER DATE', purchases.expiredate1 'EXPIRE DATE', " +
                                "purchases.stationID STATION  from purchases,drugs_master where DATE(purchases.recordeddate1) <= @tdate and DATE(purchases.recordeddate1) >= @fdate and drugs_master.drugcode = purchases.itemcode ", con);
                    com.Parameters.AddWithValue("@fdate", fdate);
                    com.Parameters.AddWithValue("@tdate", tdate);
                    da = new MySqlDataAdapter(com);
                    da.Fill(table);
                    //da.Dispose();

                    if (table.Rows.Count > 0)
                    {
                        for (int j = 0; j < table.Rows.Count; j++)
                        {

                            totaldrugpurchase += double.Parse(table.Rows[j]["TOTAL AMOUNT((PR-PS)xIP)"].ToString());
                               
                        }

                        ViewBag.PurchaseMessage = string.Format("The Total Drug Purchase is {0:n} for the specified date range.", totaldrugpurchase);
                        ViewBag.from = fdate;
                        ViewBag.tdate = tdate;
                    }
                    else
                    {
                        ViewBag.PurchaseMessage = string.Format("NO REPORT FOUND FROM DATE " + fdate + " TO DATE " + tdate + " TRY TO CHANGE DATE");
                    }


                }
                catch
                {

                }
                   
            }

            return table;
        }

        // GET: Purchases/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Purchases/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Purchases/Create
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

        // GET: Purchases/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Purchases/Edit/5
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

        // GET: Purchases/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Purchases/Delete/5
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
