using ClinicFiles.Data;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ClinicFiles
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            LoadPatientsToGrid();

            // Attach search button click event
            btnSearch.Click += btnSearch_Click_1;

            // live search as user type
            txtSeachFile.TextChanged += (s, e) => btnSearch_Click_1(s, e);
        }

        // Load patients into DataGridView
        private void LoadPatientsToGrid()
        {
            PatientData.LoadPatients();

            dgvPatients.AutoGenerateColumns = false;
            dgvPatients.Columns.Clear();

            // Columns
            dgvPatients.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FileNumber",
                HeaderText = "File Number"
            });
            dgvPatients.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ID",
                HeaderText = "ID"
            });
            dgvPatients.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = "Patient Name"
            });
            dgvPatients.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Age",
                HeaderText = "Age"
            });
            dgvPatients.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Gender",
                HeaderText = "Gender"
            });
            dgvPatients.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PhoneNumber",
                HeaderText = "Contact"
            });

            dgvPatients.DataSource = PatientData.Patients;

            // Styling
            dgvPatients.ReadOnly = true;
            dgvPatients.AllowUserToAddRows = false;
            dgvPatients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Total patients
            lblTotalFiles.Text = PatientData.Patients.Count.ToString();

            // New files today
            int todayCount = PatientData.Patients.Count(p => p.DateAdded.Date == DateTime.Now.Date);
            lblNewFilesTotal.Text = todayCount.ToString();
        }

        // Add new patient button
        private void btnAddNewFile_Click(object sender, EventArgs e)
        {
            AddFile addFileForm = new AddFile();
            addFileForm.ShowDialog();
            LoadPatientsToGrid();
        }

       

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            string query = txtSeachFile.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(query))
            {
                // If search box is empty, show all patients
                LoadPatientsToGrid();
                return;
            }

            // Filter by FileNumber or Name
            var filteredPatients = PatientData.Patients
                .Where(p => p.FileNumber.ToLower().Contains(query) ||
                            p.Name.ToLower().Contains(query))
                            
                .ToList();

            if (filteredPatients.Count == 0)
            {
                // Option 1: Clear the grid and show "No files found"
                dgvPatients.DataSource = null;
                dgvPatients.Rows.Clear();
                dgvPatients.Columns.Clear();

                dgvPatients.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "No files found",
                    Name = "NoFiles",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                });
            }
            else
            {
                // Show filtered results
                dgvPatients.DataSource = filteredPatients;

                // Maintain grid styling
                dgvPatients.ReadOnly = true;
                dgvPatients.AllowUserToAddRows = false;
                dgvPatients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

        }
    }
}