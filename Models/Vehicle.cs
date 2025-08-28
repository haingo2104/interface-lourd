using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MadaTransportConnect.Models
{
    public class Vehicle
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }
        public required string RegistrationNumber { get; set; }
        public required string Model { get; set; }
        public int Capacity { get; set; }
        public required string Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
