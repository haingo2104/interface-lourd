using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MadaTransportConnect.Models
{
    public class Vehicle
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string RegistrationNumber { get; set; }
        public string Model { get; set; }
        public int Capacity { get; set; }
        public string Status { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
