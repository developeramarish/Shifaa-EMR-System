﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Shifaa_EMR_System

{

    //TODO: FUNCTIONALIZE FOR SCHEDULER VIEW TOO

    public partial class NewPatient : Form
    {


        private SiteFunctionsDataContext doAction = new SiteFunctionsDataContext(@"Data Source=shifaaserver.database.windows.net;Initial Catalog=EMRDatabase;Persist Security Info=True;User ID=shifaaAdmin;Password=qalbeefeemasr194!");

        public SiteFunctionsDataContext DoAction { get => doAction; set => doAction = value; }

        public NewPatient()
        {
            InitializeComponent();
            PregnantBox.Hide();
            PregnantLabel.Hide();
            NotPregnantBox.Hide();
           
        }



        private void metroLabel1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void NewPatient_Load(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }


        private string getAge()
        {

            DateTime now = DateTime.Today;
            int age = now.Year - DOBPicker.Value.Year;
            int monthsage = now.Month - DOBPicker.Value.Month;
            if (DOBPicker.Value > now.AddYears(-age) && !(age-- < 0)) return (age--).ToString();
            if (age < 1) return monthsage + " Months";

            return age.ToString();
        }

        private string getGender()
        {
            if (FemaleCheckBox.Checked && !MaleCheckBox.Checked)
            {
                return FemaleCheckBox.Text;
            }
            else if (MaleCheckBox.Checked && !FemaleCheckBox.Checked)
            {
                return MaleCheckBox.Text;
            }
            else
            {
                return null;
            }
        }

        private double getBMI()
        {
            while (getWeight() != 0.0 && getHeight() != 0.0)
            {
                double BMI = getWeight() / Math.Pow(getHeight(), 2) * 10000;
                return BMI;
            }

            return 0.0;
        }

        private double getWeight()
        {

            double weight = 0.0;
            double.TryParse(WeightBox.Text, out weight);
            return weight;

        }

        private Double getHeight()
        {
            double height = 0.0;
            double.TryParse(HeightBox.Text, out height);

            return height;
        }


        string maritalStatus;
        string pregnancyStatus;

        private void Save_Click(object sender, EventArgs e)
        {

            if(MaleCheckBox.Checked && FemaleCheckBox.Checked)
            {
                MessageBox.Show("Please choose either male or female. Not both");
              
            } else if (SingleBox.Checked && MarriedBox.Checked)
            {
                MessageBox.Show("Please choose either single or married. Not both");
            
            } else if (!MaleCheckBox.Checked && !FemaleCheckBox.Checked)
            {
                MessageBox.Show("Please choose a gender");
            } else if (!SingleBox.Checked && !MarriedBox.Checked)
            {
                MessageBox.Show("Please choose a marital status");
            } else if (PregnantBox.Checked && NotPregnantBox.Checked && PregnantBox.Visible && NotPregnantBox.Visible)
            {
                MessageBox.Show("Please choose either pregnant or not pregnant. Not both");

            } else if (!PregnantBox.Checked && !NotPregnantBox.Checked && PregnantBox.Visible && NotPregnantBox.Visible)
            {
                MessageBox.Show("Please choose a pregnancy status");
            }
            else
            {
                try
                {

                    DoAction.createNewPatient(FirstNameBox.Text, LastNameBox.Text, PhoneNumberBox.Text, DOBPicker.Value, getAge(), getGender(), maritalStatus , pregnancyStatus, getWeight(), getHeight(), getBMI(), NationalityBox.Text);

                    System.Data.Linq.ISingleResult<getNewPatientVitalsResult> newPatientVitals = DoAction.getNewPatientVitals(FirstNameBox.Text, LastNameBox.Text, DOBPicker.Value);

                    int selectedPatientID = 0;

                    foreach (getNewPatientVitalsResult result in newPatientVitals)
                    {
                        selectedPatientID = result.PatientID;
                    }

                    DoAction.createNewVitalSign(selectedPatientID, null, null, null, getHeight(), getWeight(), getBMI());

                    this.Close();

                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Please make sure at least first name, last name, date of birth and gender are filled.");

                    Exception ex2 = ex;
                    while (ex2.InnerException != null)
                    {
                        ex2 = ex2.InnerException;
                    }
                    Console.WriteLine(ex.InnerException);
                    throw;


                }
            }
          
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }




        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WeightBox_TextChanged(object sender, EventArgs e)
        {
            double result = 0;


            if (!String.IsNullOrWhiteSpace(WeightBox.Text) && !double.TryParse(WeightBox.Text, out result) || result <= 0)
            {
                WeightBox.Text = null;
                System.Windows.Forms.MessageBox.Show("Please enter a valid weight in Kilograms");

            }

        }

        private void FirstNameBox_TextChanged(object sender, EventArgs e)
        {
            while (FirstNameBox.Text.Any(char.IsDigit))
            {

                FirstNameBox.Text = null;
                System.Windows.Forms.MessageBox.Show("Please enter a valid first name");

            }
        }

        private void LastNameBox_TextChanged(object sender, EventArgs e)
        {
            while (LastNameBox.Text.Any(char.IsDigit))
            {
                LastNameBox.Text = null;
                System.Windows.Forms.MessageBox.Show("Please enter a valid last name");
            }
        }

        private void DOBPicker_ValueChanged(object sender, EventArgs e)
        {
            while (DateTime.Compare(DOBPicker.Value, DateTime.Today) > 0)
            {
                System.Windows.Forms.MessageBox.Show("Please enter a valid date of birth");
                DOBPicker.Value = DateTime.Today;
            }
        }

        private void HeightBox_TextChanged(object sender, EventArgs e)
        {
            double doubleresult = 0.0;



            if (!String.IsNullOrWhiteSpace(HeightBox.Text) && !double.TryParse(HeightBox.Text, out doubleresult) || doubleresult <= 0)
            {
                HeightBox.Text = null;
                System.Windows.Forms.MessageBox.Show("Please enter a valid height in centimeters");

            }

        }

        private void NationalityBox_TextChanged(object sender, EventArgs e)
        {
            while (NationalityBox.Text.Any(char.IsDigit))
            {
                NationalityBox.Text = null;
                System.Windows.Forms.MessageBox.Show("Please enter a valid nationality");
            }

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void cmLabel_Click(object sender, EventArgs e)
        {

        }

   

        private void PhoneNumberBox_TextChanged(object sender, EventArgs e)
        {
            var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();


            if (!String.IsNullOrWhiteSpace(PhoneNumberBox.Text))
            {
                try
                {

                    //TODO: FORMAT THE NUMBER USING ASYOUTYPE FORMATER
                    phoneNumberUtil.Parse(PhoneNumberBox.Text, null);
                    
                }
                catch (FormatException)
                {
                    MessageBox.Show("Please enter a valid phone number");
                    PhoneNumberBox.Text = null;
                }

            }

        }

        private void FemaleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(FemaleCheckBox.Checked && !MaleCheckBox.Checked)
            {
                PregnantLabel.Show();
                PregnantBox.Show();
                NotPregnantBox.Show();
                WeightLabel.Top = 382;
                WeightBox.Top = 382;
                kgLabel.Top = 385;
                HeightLabel.Top = 425;
                HeightBox.Top =  425;
                cmLabel.Top =  428;
                NationalityLabel.Top = 465;
                NationalityBox.Top = 465;

            }
        }

        private void NotPregnantBox_CheckedChanged(object sender, EventArgs e)
        {
            if (NotPregnantBox.Checked && !PregnantBox.Checked) pregnancyStatus = "Not Pregnant";
        }

        private void MaleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!FemaleCheckBox.Checked && MaleCheckBox.Checked || (FemaleCheckBox.Checked && MaleCheckBox.Checked))
            {
                PregnantLabel.Hide();
                PregnantBox.Hide();
                NotPregnantBox.Hide();
                WeightLabel.Top = 338;
                WeightBox.Top = 338;
                kgLabel.Top = 342;
                HeightLabel.Top = 382;
                HeightBox.Top = 382;
                cmLabel.Top = 385;
                NationalityLabel.Top = 422;
                NationalityBox.Top = 422;

            }
        }

        private void PregnantBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!NotPregnantBox.Checked && PregnantBox.Checked) pregnancyStatus = "Pregnant";
        }

        private void SingleBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SingleBox.Checked && !MarriedBox.Checked) maritalStatus = "Single";
        }

        private void MarriedBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!SingleBox.Checked && MarriedBox.Checked) maritalStatus = "Married";
        }
    }
}
