using System;
using System.Windows.Forms;
using MadaTransportConnect.Data;   
using MadaTransportConnect.Models;

namespace MadaTransportConnect.Forms
{
    public class MainForm : Form
    {
        private readonly Repository<Vehicle> _vehicleRepo;
        private DataGridView dgvVehicles;
        private Button btnAddVehicle;

        public MainForm(MongoDbContext ctx)
        {
            _vehicleRepo = new Repository<Vehicle>(ctx, "vehicles");
            InitializeComponent();
            LoadVehicles();
        }

        private void InitializeComponent()
        {
            this.Text = "Mada Transport Connect - Gestion des véhicules";
            this.Width = 800;
            this.Height = 500;

            dgvVehicles = new DataGridView();
            dgvVehicles.Top = 20;
            dgvVehicles.Left = 20;
            dgvVehicles.Width = 740;
            dgvVehicles.Height = 350;
            dgvVehicles.ReadOnly = true;
            dgvVehicles.AllowUserToAddRows = false;

            btnAddVehicle = new Button();
            btnAddVehicle.Text = "Ajouter un véhicule";
            btnAddVehicle.Top = 390;
            btnAddVehicle.Left = 20;
            btnAddVehicle.Click += BtnAddVehicle_Click;

            this.Controls.Add(dgvVehicles);
            this.Controls.Add(btnAddVehicle);
        }

        private void LoadVehicles()
        {
            var list = _vehicleRepo.GetAll();
            dgvVehicles.DataSource = list;
            if (dgvVehicles.Columns["Id"] != null)
                dgvVehicles.Columns["Id"].Visible = false;
            dgvVehicles.AutoResizeColumns();
        }

        private void BtnAddVehicle_Click(object sender, EventArgs e)
        {
            using (var form = new AddVehicleForm(_vehicleRepo))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadVehicles();
            }
        }
    }
}
