using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MadaTransportConnect.Models
{
    public class Trip
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }
        public required string VehicleId { get; set; }
        public required string From { get; set; }
        public required string To { get; set; }
        public DateTime DepartureTime { get; set; }
        public decimal Price { get; set; }
        public int SeatsAvailable { get; set; }
    }
}
