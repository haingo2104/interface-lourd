using System;
using System.Linq;
using System.Windows.Forms;
using MadaTransportConnect.Data;
using MadaTransportConnect.Models;
using MongoDB.Bson; 
namespace MadaTransportConnect.Forms
{
    public class MainForm : Form
    {
        private readonly Repository<Vehicle> _vehicleRepo;
        private readonly Repository<Trip> _tripRepo;
        private readonly Repository<Reservation> _reservationRepo;

        private DataGridView dgvVehicles;
        private Button btnAddVehicle;

        private DataGridView dgvTrips;
        private Button btnAddTrip;

         private DataGridView dgvReservations;
         private Button btnAddReservation;

        private TabControl tabControl;
        private TabPage tabVehicles;
        private TabPage tabTrips;

        public MainForm(MongoDbContext ctx)
        {
            _vehicleRepo = new Repository<Vehicle>(ctx, "vehicles");
            _tripRepo = new Repository<Trip>(ctx, "trips");
             _reservationRepo = new Repository<Reservation>(ctx, "reservations");

            InitializeComponent();

            LoadVehicles();
            LoadTrips();
            LoadReservations();
        }

        private void InitializeComponent()
        {
            this.Text = "Mada Transport Connect - Gestion";
            this.Width = 900;
            this.Height = 600;

            // Création du TabControl
            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            // Onglet Véhicules
            tabVehicles = new TabPage("Véhicules");
            dgvVehicles = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 400,
                ReadOnly = true,
                AllowUserToAddRows = false
            };

            btnAddVehicle = new Button { Text = "Ajouter un véhicule", Dock = DockStyle.Bottom, Height = 40 };
            btnAddVehicle.Click += BtnAddVehicle_Click;

            tabVehicles.Controls.Add(dgvVehicles);
            tabVehicles.Controls.Add(btnAddVehicle);

            // Onglet Trajets
            tabTrips = new TabPage("Trajets");
            dgvTrips = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 400,
                ReadOnly = true,
                AllowUserToAddRows = false
            };

            btnAddTrip = new Button { Text = "Ajouter un trajet", Dock = DockStyle.Bottom, Height = 40 };
            btnAddTrip.Click += BtnAddTrip_Click;

            tabTrips.Controls.Add(dgvTrips);
            tabTrips.Controls.Add(btnAddTrip);

             // --- Onglet Réservations ---
            var tabReservations = new TabPage("Réservations");
            dgvReservations = new DataGridView() { Dock = DockStyle.Top, Height = 400, ReadOnly = true, AllowUserToAddRows = false };
            btnAddReservation = new Button() { Text = "Ajouter une réservation", Top = 410, Left = 20 };
            btnAddReservation.Click += BtnAddReservation_Click;
            tabReservations.Controls.AddRange(new Control[] { dgvReservations, btnAddReservation });


            // Ajout des onglets au TabControl
            tabControl.TabPages.Add(tabVehicles);
            tabControl.TabPages.Add(tabTrips);
            tabControl.TabPages.Add(tabReservations);

            this.Controls.Add(tabControl);
        }

        private void LoadVehicles()
        {
            var list = _vehicleRepo.GetAll();
            dgvVehicles.DataSource = list;
            if (dgvVehicles.Columns["Id"] != null)
                dgvVehicles.Columns["Id"].Visible = false;
            dgvVehicles.AutoResizeColumns();
        }

        private void LoadTrips()
        {
            var trips = _tripRepo.GetAll();
            var vehicles = _vehicleRepo.GetAll();

           var list = trips.Select(t => new
            {
                t.Id,
                Vehicle = vehicles.FirstOrDefault(v => v.Id == t.VehicleId)?.RegistrationNumber ?? "Inconnu",
                t.From,
                t.To,
                DepartureTime = t.DepartureTime.ToString("dd/MM/yyyy HH:mm"),
                t.Price,
                t.SeatsAvailable
            }).ToList();

            dgvTrips.DataSource = list;
            if (dgvTrips.Columns["Id"] != null)
                dgvTrips.Columns["Id"].Visible = false;
            dgvTrips.AutoResizeColumns();
        }

        private void LoadReservations()
        {
            var reservations = _reservationRepo.GetAll().ToList();
            var reservationDisplay = reservations.Select(r =>
            {
                var trip = _tripRepo.GetById(r.TripId);
                var vehicle = trip != null ? _vehicleRepo.GetById(trip.VehicleId) : null;
                return new
                {
                    r.Id,
                    r.PassengerName,
                    r.Phone,
                    Trip = trip != null ? $"{trip.From} → {trip.To}" : "",
                    Vehicle = vehicle?.Model ?? "",
                    r.SeatsBooked,
                    r.Status,
                    r.CreatedAt
                };
            }).ToList();

            dgvReservations.DataSource = reservationDisplay;
            dgvReservations.AutoResizeColumns();
        }

        private void BtnAddVehicle_Click(object sender, EventArgs e)
        {
            using (var form = new AddVehicleForm(_vehicleRepo))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadVehicles();
            }
        }

        private void BtnAddTrip_Click(object sender, EventArgs e)
        {
            using (var form = new AddTripForm(_tripRepo, _vehicleRepo))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    LoadTrips();
            }
        }

        private void BtnAddReservation_Click(object sender, EventArgs e)
        {
            using var form = new AddReservationForm(_reservationRepo, _tripRepo);
            if (form.ShowDialog() == DialogResult.OK)
                LoadReservations();
        }
    }
}
