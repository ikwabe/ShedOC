using Microsoft.Ajax.Utilities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ShedOc.Controllers
{
    public  class Login
    {
        public static string connection = "server = localhost; user =root;password = '';database= sdahealth";
        [DisplayName("User Name")]
        public string username { get; set; }
        [DisplayName("Password")]
        public string password { get; set; }
        public string status { get; set; }
        public string UTID { get; set; }
        public static string ID { get; set; }
        public static string uname { get; set; }

        //sblw-3hn8-sqoy19 keys
        public static string Encrypt(string input, string key)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string input, string key)
        {
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        //function to record  logouts
        public static void logoutRecord(string username)
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = connection;
            string updateD = " update login_logs set logouttime = '" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "', status = 'logout' where loginname = '" + username + "'";

            MySqlCommand Update = new MySqlCommand(updateD, con);

            MySqlDataReader rd;

            try
            {
                con.Open();

                rd = Update.ExecuteReader();
                rd.Close();

            }
            catch 
            {
                 
            }

        }

        //function to record  login
        public static void loginRecord(string username)
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = connection;
            string updateD = "insert into login_logs(logintime,status,loginname) values('" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "','login','" + username + "')";

            MySqlCommand Update = new MySqlCommand(updateD, con);

            MySqlDataReader rd;

            try
            {
                con.Open();

                rd = Update.ExecuteReader();
                rd.Close();

            }
            catch 
            {
                 
            }

        }
        //function to change the status of the login user
        public static void loginSt(string ID)
        {
           
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = connection;
            string updateD = " update users set Status = 'login' where ID = '" + ID + "'";

            MySqlCommand Update = new MySqlCommand(updateD, con);

            MySqlDataReader rd;

            try
            {
                con.Open();

                rd = Update.ExecuteReader();
                rd.Close();

            }
            catch 
            {
                 
            }

        }

        //the function to change the status of the logout user
        public static void logoutSt(string ID)
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = connection;
            string updateD = "update  users set status = 'logout' where ID = '" + ID + "'";

            MySqlCommand Update = new MySqlCommand(updateD, con);

            MySqlDataReader rd;

            try
            {
                con.Open();

                rd = Update.ExecuteReader();
                rd.Close();

            }
            catch 
            {
                 
            }
        }

       
    }
}