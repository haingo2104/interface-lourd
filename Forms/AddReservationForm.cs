using System;
using System.Linq;
using System.Windows.Forms;
using MadaTransportConnect.Data;
using MadaTransportConnect.Models;
using MongoDB.Bson; 
namespace MadaTransportConnect.Forms
{
    public class AddReservationForm : Form
    {
        private TextBox txtPassenger;
        private TextBox txtPhone;
        private ComboBox comboTrips;
        private NumericUpDown numSeats;
        private Button btnSave, btnCancel;
        private ComboBox comboTrip;

        private readonly Repository<Reservation> _reservationRepo;
        private readonly Repository<Trip> _tripRepo;

        public AddReservationForm(Repository<Reservation> reservationRepo, Repository<Trip> tripRepo)
        {
            _reservationRepo = reservationRepo;
            _tripRepo = tripRepo;
            LoadTrips();
            InitializeComponent();
        }

        private void LoadTrips()
        {
            var trips = _tripRepo.GetAll();
            foreach (var t in trips)
            {
                comboTrip.Items.Add(new ComboBoxItem
                {
                    Text = $"{t.From} → {t.To} ({t.DepartureTime})",
                    Value = t.Id.ToString()
                });
            }
        }
        private void InitializeComponent()
        {
            this.Text = "Ajouter une réservation";
            this.Width = 400;
            this.Height = 350;

            Label lbl1 = new Label() { Text = "Passager:", Top = 20, Left = 20 };
            txtPassenger = new TextBox() { Top = 20, Left = 150, Width = 200 };

            Label lbl2 = new Label() { Text = "Téléphone:", Top = 60, Left = 20 };
            txtPhone = new TextBox() { Top = 60, Left = 150, Width = 200 };

            Label lbl3 = new Label() { Text = "Trajet:", Top = 100, Left = 20 };
            comboTrip = new ComboBox() { Top = 100, Left = 150, Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };

            Label lbl4 = new Label() { Text = "Places réservées:", Top = 140, Left = 20 };
            numSeats = new NumericUpDown() { Top = 140, Left = 150, Width = 200, Minimum = 1, Maximum = 100 };

            btnSave = new Button() { Text = "Enregistrer", Top = 200, Left = 150 };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button() { Text = "Annuler", Top = 200, Left = 250 };
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { lbl1, txtPassenger, lbl2, txtPhone, lbl3, comboTrip, lbl4, numSeats, btnSave, btnCancel });
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (comboTrip.SelectedItem is ComboBoxItem selectedItem)
            {
                var tripId = selectedItem.Value; // la valeur qu’on a stockée
                var trip = _tripRepo.GetById(new ObjectId(tripId)); // on récupère le trajet depuis Mongo

                var reservation = new Reservation()
                {
                    TripId = new ObjectId(tripId.ToString()),
                    PassengerName = txtPassenger.Text,
                    Phone = txtPhone.Text,
                    SeatsBooked = (int)numSeats.Value,
                    Status = "Confirmée"
                };

                _reservationRepo.Insert(reservation);

                // mettre à jour le nombre de places dispo
                trip.SeatsAvailable -= reservation.SeatsBooked;
                _tripRepo.Update(tripId, trip);

                MessageBox.Show("Réservation effectuée avec succès !");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }


        // --- Classe interne pour ComboBox ---
        public class ComboBoxItem
        {
            public string Text { get; set; }
            public string Value { get; set; }

            public override string ToString() => Text;
        }
    }
}
