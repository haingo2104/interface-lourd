using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MadaTransportConnect.Models
{
    public class Reservation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string TripId { get; set; }
        public string PassengerName { get; set; }
        public string Phone { get; set; }
        public int SeatsBooked { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } 
    }
}
