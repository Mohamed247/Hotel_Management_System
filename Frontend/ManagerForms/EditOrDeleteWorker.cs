﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Frontend.HttpService;
namespace Frontend.ManagerForms
{
    public partial class EditOrDeleteWorker : Form
    {
        dynamic worker;

        public EditOrDeleteWorker()
        {
            InitializeComponent();
        }

        private void EditOrDeleteWorker_Load(object sender, EventArgs e)
        {
            worker = new ExpandoObject();
            passwordTextBox.Enabled = !isRoomService.Checked;
        }


        private void searchByIdBtn_Click(object sender, EventArgs e)
        {
            worker.id = searchbyIdTextbox.Text;

            //TODO: api set worker to the api returend worker
            dynamic obj = new ExpandoObject();
            obj.id = worker.id;
            dynamic work = Service.GetWorkerById(obj);
            nameTextBox.Text = work.name.ToString();
            ageTextBox.Text = work.age.ToString();
            emailTextBox.Text = work.email.ToString();
            if (isRoomService.Checked) { work.password = ""; }
            else { passwordTextBox.Text = work.password; }
            phoneTextBox.Text = work.phoneNumber.ToString();
            salaryTextBox.Text = work.salary.ToString();
            jobTitleTextBox.Text = work.jobType.ToString();
            incomeTypeTextBox.Text = work.incomeType.ToString();
        }

        private void isRoomService_CheckedChanged(object sender, EventArgs e)
        {
            passwordTextBox.Enabled = !isRoomService.Checked;
        }

        private void editWorkerBtn_Click(object sender, EventArgs e)
        {
            //api takes all data and edit it
            dynamic obj = new ExpandoObject();
            obj.id = searchbyIdTextbox.Text;
            obj.userName = nameTextBox.Text;
            obj.age = ageTextBox.Text;
            obj.email = emailTextBox.Text;
            obj.password = passwordTextBox.Text;
            obj.phoneNumber = phoneTextBox.Text;
            obj.salary = salaryTextBox.Text;
            obj.jobTitle = jobTitleTextBox.Text;
            obj.incomeType = incomeTypeTextBox.Text;

            Service.EditResident(obj);

            // and delete it
            clearBtn_Click(sender, e);
        }

        private void deleteWorkerBtn_Click(object sender, EventArgs e)
        {
            dynamic obj = new ExpandoObject();
            obj.id = searchbyIdTextbox.Text;

            Service.DeleteResident(obj);
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            searchbyIdTextbox.Text = "";
            nameTextBox.Text = "";
            ageTextBox.Text = "";
            emailTextBox.Text = "";
            passwordTextBox.Text = "";
            phoneTextBox.Text = "";
            salaryTextBox.Text = "";
            jobTitleTextBox.Text = "";
            incomeTypeTextBox.Text = "";
        }





        //apis needed
        //edit worker -> input worker object, typeOfWorker manager
        //delete worker -> input workerId,    typeOfWorker
        //getWorkers -> input nothing, output all workers and their types
    }
}
