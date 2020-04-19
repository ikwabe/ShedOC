using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ShedOc.Models
{
    public class DoctorTreatment
    {
        protected string _symptoms;
        protected string _assesment;
        protected string _patientHistory;

        //lab tests
        protected string _Test;
        protected string _instruction;
        protected string _conditions;

        //drugs
        protected string _drugcode;
        protected string _diagnosis;
        protected double _days;
        protected double _morning;
        protected double _afternoon;
        protected double _evening;
        protected string _otherplan;
        protected string _comment;
        public DataTable labTest;
        public DataTable dignosis;
        public DataTable patientAssigned;
       
        public DoctorTreatment() { }
        public DoctorTreatment( string symptoms, string assesment, string patientHistory, string instruction, string conditions, string drugcode, string diagnosis, double days, double morning, double afternoon, double evening, string otherplan, string comment)
        {
           
            this._symptoms = symptoms;
            this._assesment = assesment;
            this._patientHistory = patientHistory;
            this._instruction = instruction;
            this._conditions = conditions;
            this._drugcode = drugcode;
            this._diagnosis = diagnosis;
            this._days = days;
            this._morning = morning;
            this._afternoon = afternoon;
            this._evening = evening;
            this._otherplan = otherplan;
            this._comment = comment;
            labTest = new DataTable();
            dignosis = new DataTable();
            patientAssigned = new DataTable();
        }


        public virtual string Symptoms
        {
            get { return _symptoms; }
            set { _symptoms = value; }
        }
        public virtual string Assesment
        {
            get { return _assesment; }
            set { _assesment = value; }
        }
        public virtual string PatientHistory
        {
            get { return _patientHistory; }
            set { _patientHistory = value; }
        }
        public virtual string Instruction
        {
            get { return _instruction; }
            set { _instruction = value; }
        }
        public virtual string Conditions
        {
            get { return _conditions; }
            set { _conditions = value; }
        }
        public virtual string Drugcode
        {
            get { return _drugcode; }
            set { _drugcode = value; }
        }
        public virtual string Diagnosis
        {
            get { return _diagnosis; }
            set { _diagnosis = value; }
        }
        public virtual double Days
        {
            get { return _days; }
            set { _days = value; }
        }
        public virtual double Morning
        {
            get { return _morning; }
            set { _morning = value; }
        }
        public virtual double Afternoon
        {
            get { return _afternoon; }
            set { _afternoon = value; }
        }
        public virtual double Evening
        {
            get { return _evening; }
            set { _evening = value; }
        }
        public virtual string Otherplan
        {
            get { return _otherplan; }
            set { _otherplan = value; }
        }
        public virtual string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

    }
}