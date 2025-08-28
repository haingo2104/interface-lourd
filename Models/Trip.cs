using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MadaTransportConnect.Models
{
    public class Trip
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string VehicleId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime DepartureTime { get; set; }
        public decimal Price { get; set; }
        public int SeatsAvailable { get; set; }
    }
}
