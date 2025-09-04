using System;
using System.Windows.Forms;
using MadaTransportConnect.Data;
using MadaTransportConnect.Models;
using MongoDB.Bson; 
namespace MadaTransportConnect.Forms
{
    public class AddTripForm : Form
    {
        private ComboBox comboVehicle;
        private TextBox txtFrom;
        private TextBox txtTo;
        private DateTimePicker dtDeparture;
        private NumericUpDown numSeats;
        private NumericUpDown numPrice;
        private Button btnSave;
        private Button btnCancel;

        private readonly Repository<Trip> _tripRepo;
        private readonly Repository<Vehicle> _vehicleRepo;

        public AddTripForm(Repository<Trip> tripRepo, Repository<Vehicle> vehicleRepo)
        {
            _tripRepo = tripRepo;
            _vehicleRepo = vehicleRepo;
            InitializeComponent();
            LoadVehicles();
        }

        private void InitializeComponent()
        {
            this.Text = "Ajouter un trajet";
            this.Width = 400;
            this.Height = 350;

            Label lbl1 = new Label() { Text = "Véhicule :", Top = 20, Left = 20 };
            comboVehicle = new ComboBox() { Top = 20, Left = 150, Width = 200 };

            Label lbl2 = new Label() { Text = "Départ :", Top = 60, Left = 20 };
            txtFrom = new TextBox() { Top = 60, Left = 150, Width = 200 };

            Label lbl3 = new Label() { Text = "Arrivée :", Top = 100, Left = 20 };
            txtTo = new TextBox() { Top = 100, Left = 150, Width = 200 };

            Label lbl4 = new Label() { Text = "Date/Heure départ :", Top = 140, Left = 20 };
            dtDeparture = new DateTimePicker() { Top = 140, Left = 150, Width = 200, Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy HH:mm" };

            Label lbl5 = new Label() { Text = "Prix :", Top = 180, Left = 20 };
            numPrice = new NumericUpDown() { Top = 180, Left = 150, Width = 200, DecimalPlaces = 2, Minimum = 1000, Maximum = 100000 };

            Label lbl6 = new Label() { Text = "Places dispo :", Top = 220, Left = 20 };
            numSeats = new NumericUpDown() { Top = 220, Left = 150, Width = 200, Minimum = 1, Maximum = 100 };

            btnSave = new Button() { Text = "Enregistrer", Top = 270, Left = 150 };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button() { Text = "Annuler", Top = 270, Left = 250 };
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { lbl1, comboVehicle, lbl2, txtFrom, lbl3, txtTo, lbl4, dtDeparture, lbl5, numPrice, lbl6, numSeats, btnSave, btnCancel });
        }

        private void LoadVehicles()
        {
            var vehicles = _vehicleRepo.GetAll();
            comboVehicle.DataSource = vehicles;
            comboVehicle.DisplayMember = "RegistrationNumber";
            comboVehicle.ValueMember = "Id";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var trip = new Trip()
            {
                VehicleId = new ObjectId(comboVehicle.SelectedValue.ToString()),
                From = txtFrom.Text,
                To = txtTo.Text,
                DepartureTime = dtDeparture.Value,
                Price = numPrice.Value,
                SeatsAvailable = (int)numSeats.Value
            };

            _tripRepo.Insert(trip);
            MessageBox.Show("Trajet ajouté avec succès !");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
