using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Microsoft.Ajax.Utilities;
using MySql.Data.MySqlClient;

namespace ShedOc.Controllers
{
    public class HomeController : Controller
    {
       

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(Login login)
        {
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = Login.connection;
                string titleId = "SELECT TitleCode,username,password,status,ID FROM users WHERE username = '" + login.username + "' and password = '" + Login.Encrypt(login.password, "sblw-3hn8-sqoy19") + "'";
                MySqlCommand UT = new MySqlCommand(titleId, con);
                MySqlDataReader rd;
                DataTable table = new DataTable();
                try
                {
                    con.Open();
                    //retrieving UT ID from the database
                    rd = UT.ExecuteReader();
                    table.Load(rd);
                    rd.Close();


                    if (table.Rows.Count > 0)
                    {
                        login.username = table.Rows[0][1].ToString();
                        Login.uname = table.Rows[0][1].ToString();
                        login.password = table.Rows[0][2].ToString();
                        login.status = table.Rows[0][3].ToString();
                        login.UTID = table.Rows[0][0].ToString();
                        Login.ID = table.Rows[0][4].ToString();

                       Session["ID"] = table.Rows[0][4].ToString();
                       Session["utid"] = table.Rows[0][0].ToString();
                        //doctor
                        if (login.UTID == "UT01")
                        {
                           
                            //update status
                            Login.loginRecord(login.username);
                            Login.loginSt(Login.ID);

                            //Load the Reception page
                            return RedirectToAction("Index", "Doctor");


                        }
                        //Ultrasound
                        else if (login.UTID == "UT12")
                        {

                            //update login.status
                            Login.loginRecord(login.username);
                            Login.loginSt(Login.ID);

                            //load the doctor form

                        }
                        //Administrator
                        else if (login.UTID == "UT02")
                        {

                            //update login.status
                            Login.loginRecord(login.username);
                            Login.loginSt(Login.ID);

                            return RedirectToAction("Index", "Report");
                            //load the admin form


                        }
                        //Administrator Level 1
                        else if (login.UTID == "UT16")
                        {
                            //update login.status
                            Login.loginRecord(login.username);
                            Login.loginSt(Login.ID);

                            //load the admin form


                        }
                        //Receptionist
                        else if (login.UTID == "UT04")
                        {

                            //update login.status
                            Login.loginRecord(login.username);
                            Login.loginSt(Login.ID);

                            //Load the Reception page
                            return RedirectToAction("Index", "Patients");

                        }
                        //Pharmersist
                        else if (login.UTID == "UT05" && login.status == "logout")
                        {

                            //update login.status
                            Login.loginRecord(login.username);
                            Login.loginSt(Login.ID);

                            //load the doctor form


                        }
                        //Cashier
                        else if (login.UTID == "UT06" )
                        {

                            //update login.status
                            Login.loginRecord(login.username);
                            Login.loginSt(Login.ID);



                        }
                        //Laboratorian
                        else if (login.UTID == "UT08")
                        {

                            //update login.status
                            Login.loginRecord(login.username);
                            Login.loginSt(Login.ID);


                        }
                        //Store
                        else if (login.UTID == "UT11")
                        {

                            //update login.status
                            Login.loginRecord(login.username);
                            Login.loginSt(Login.ID);


                        }
                        //Store
                        else if (login.UTID == "UT21")
                        {

                            //update status
                            Login.loginRecord(login.username);
                            Login.loginSt(Login.ID);


                        }
                        else
                        {
                            ViewBag.TheResult = true;

                            //if (MessageBox.Show("It seems you have login somewhere, do you want to login anyway?", "Login Detected", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            //{
                            //    loginRecovery();
                            //    login();
                            //}

                        }

                    }
                    else
                    {
                        ViewBag.Result = true;
                        return RedirectToAction("Index", "Home");
                    }

                }
                catch
                {
                    ViewBag.Message = "Network Error";
                }
                con.Close();

                return View("Index");
            }

           
        }


        public ActionResult SignOut(Login login)
        {
            Login.logoutSt(Login.ID);
            Login.logoutRecord(Login.ID);

            Session["ID"] = null;
            return RedirectToAction("Index", "Home");
        }

      
    }


}