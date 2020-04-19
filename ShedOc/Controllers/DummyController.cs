using MySql.Data.MySqlClient;
using ShedOc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;

namespace ShedOc.Controllers
{
    public class DummyController : Controller
    {
       
        // GET: Dummy
        public ActionResult Index(DummyTick dm)
        {
            if (Session["ID"] != null && Session["utid"].ToString() == "UT04")
            {
                var model = new DummyTick();
                model.Tests = GetTest();
                model.Names = GetNames(dm.searchFullname);
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private IList<SelectListItem> GetTest()
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = Login.connection;
            string clinic = "select name from laboratory_tests_master order by name";
            MySqlCommand com = new MySqlCommand(clinic, con);
            MySqlDataAdapter ad;
            DataTable table = new DataTable();
            List<SelectListItem> test = new List<SelectListItem>();

            try
            {
                con.Open();
                ad = new MySqlDataAdapter(com);
                ad.Fill(table);
                ad.Dispose();

                if (table.Rows.Count > 0)
                {
                    for(int i = 0; i<table.Rows.Count; i++)
                    {
                        test.Add(new SelectListItem { Text = table.Rows[i][0].ToString(), Value = table.Rows[i][0].ToString() });
                    }

                }


            }
            catch 
            {
                
            }
          


            return test;
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
                string clinic = "select recordID, Fullname 'PATIENT NAME' from patients where fullname like '%" + searchData + "%'";
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
       

        // GET: Dummy/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dummy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dummy/Create
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

        // GET: Dummy/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dummy/Edit/5
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

        // GET: Dummy/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dummy/Delete/5
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
