using System;
using System.Windows.Forms;
using MadaTransportConnect.Data;   
using MadaTransportConnect.Models;

namespace MadaTransportConnect.Forms
{
    public class MainForm : Form
    {
        private readonly Repository<Vehicle> _vehicleRepo;

        // Champs WinForms initialisés avec null! pour supprimer les warnings
        private DataGridView dgvVehicles = null!;
        private Button btnAddVehicle = null!;

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

            // Initialisation du DataGridView
            dgvVehicles = new DataGridView();
            dgvVehicles.Top = 20;
            dgvVehicles.Left = 20;
            dgvVehicles.Width = 740;
            dgvVehicles.Height = 350;
            dgvVehicles.ReadOnly = true;
            dgvVehicles.AllowUserToAddRows = false;

            // Initialisation du bouton
            btnAddVehicle = new Button();
            btnAddVehicle.Text = "Ajouter un véhicule";
            btnAddVehicle.Top = 390;
            btnAddVehicle.Left = 20;
            btnAddVehicle.Click += BtnAddVehicle_Click; // event handler

            this.Controls.Add(dgvVehicles);
            this.Controls.Add(btnAddVehicle);
        }

        private void LoadVehicles()
        {
            var list = _vehicleRepo.GetAll();
            dgvVehicles.DataSource = list;
            
            // Masquer la colonne Id
            if (dgvVehicles.Columns["Id"] != null)
                dgvVehicles.Columns["Id"].Visible = false;
            
            dgvVehicles.AutoResizeColumns();
        }

        // Event handler avec sender nullable pour correspondre à EventHandler
        private void BtnAddVehicle_Click(object? sender, EventArgs e)
        {
            using (var form = new AddVehicleForm(_vehicleRepo))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadVehicles();
            }
        }
    }
}
