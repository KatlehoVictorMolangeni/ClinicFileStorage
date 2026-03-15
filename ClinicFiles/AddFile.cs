using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClinicFiles.Models; 
using ClinicFiles.Data;

namespace ClinicFiles
{
    public partial class AddFile : Form
    {
        public AddFile()
        {
            InitializeComponent();

            cmbGender.Items.Add("Select Gender");
            cmbGender.Items.Add("Male");
            cmbGender.Items.Add("Female");
            cmbGender.SelectedIndex = 0;
            cmbGender.Items.Add("Other");
        }

        private void btnBack1_Click(object sender, EventArgs e)
        {
            Dashboard dashboardForm = new Dashboard();
            dashboardForm.Show();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Please enter the patient's full name.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Please enter the patient's ID Number.");
                return;
            }
            if (!int.TryParse(txtAge.Text, out int age))
            {
                MessageBox.Show("Please enter a valid age.");
                return;
            }
            if (cmbGender.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a gender.");
                return;
            }

            string fileNumber = PatientData.GenerateUniqueFileNumber();

            Patient newPatient = new Patient
            {
                Name = txtFullName.Text,
                ID = txtID.Text,
                Age = age,
                Gender = cmbGender.SelectedItem.ToString(),
                Address = txtAddress.Text,
                PhoneNumber = txtContact.Text,
                MedicalHistory = rchMedicalHistory.Text,
                FileNumber = fileNumber,
                DateAdded = DateTime.Now,
            };

            // Load existing patients, add new, save
            PatientData.LoadPatients();
            PatientData.Patients.Add(newPatient);
            PatientData.SavePatients();

            MessageBox.Show("Patient saved successfully! New File Recorded.\nFile Number: " + fileNumber);

            // Close AddPatientForm
            this.Close();




        }
    }
}
