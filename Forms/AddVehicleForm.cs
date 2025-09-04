using System;
using System.Windows.Forms;
using MadaTransportConnect.Data;
using MadaTransportConnect.Models;

namespace MadaTransportConnect.Forms
{
    public class AddVehicleForm : Form
    {
        // Champs WinForms initialisés avec null! pour supprimer les warnings CS8618
        private TextBox txtRegistration = null!;
        private TextBox txtModel = null!;
        private NumericUpDown numCapacity = null!;
        private ComboBox comboStatus = null!;
        private Button btnSave = null!;
        private Button btnCancel = null!;

        private readonly Repository<Vehicle> _vehicleRepo;

        public AddVehicleForm(Repository<Vehicle> vehicleRepo)
        {
            _vehicleRepo = vehicleRepo;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Ajouter un véhicule";
            this.Width = 400;
            this.Height = 300;

            Label lbl1 = new Label() { Text = "Immatriculation:", Top = 20, Left = 20 };
            txtRegistration = new TextBox() { Top = 20, Left = 150, Width = 200 };

            Label lbl2 = new Label() { Text = "Modèle:", Top = 60, Left = 20 };
            txtModel = new TextBox() { Top = 60, Left = 150, Width = 200 };

            Label lbl3 = new Label() { Text = "Capacité:", Top = 100, Left = 20 };
            numCapacity = new NumericUpDown() { Top = 100, Left = 150, Width = 200, Minimum = 1, Maximum = 100 };

            Label lbl4 = new Label() { Text = "Statut:", Top = 140, Left = 20 };
            comboStatus = new ComboBox() { Top = 140, Left = 150, Width = 200 };
            comboStatus.Items.AddRange(new string[] { "Active", "Maintenance" });
            comboStatus.SelectedIndex = 0;

            btnSave = new Button() { Text = "Enregistrer", Top = 200, Left = 150 };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button() { Text = "Annuler", Top = 200, Left = 250 };
            btnCancel.Click += (object? s, EventArgs e) => this.Close();

            this.Controls.AddRange(new Control[] { lbl1, txtRegistration, lbl2, txtModel, lbl3, numCapacity, lbl4, comboStatus, btnSave, btnCancel });
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            var vehicle = new Vehicle()
           {
                RegistrationNumber = txtRegistration.Text,
                Model = txtModel.Text,
                Capacity = (int)numCapacity.Value,
                Status = comboStatus.SelectedItem.ToString()
            };

            _vehicleRepo.Insert(vehicle);
            MessageBox.Show("Véhicule ajouté avec succès !");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
