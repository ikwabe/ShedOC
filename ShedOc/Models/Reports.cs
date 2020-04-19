using MySql.Data.MySqlClient;
using ShedOc.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace ShedOc.Models
{
    public class Reports
    {
        
        public DataTable sales;
        public DataTable moneyreturned;
        public DataTable purchase;
        public DataTable pharmacySales;
        public DataTable labtest;
        public DataTable patientVisit;


        public Reports()
        {
        }


       
    }
}