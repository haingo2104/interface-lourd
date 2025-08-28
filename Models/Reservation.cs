using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

public class Reservation
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; set; }

    public required string TripId { get; set; }
    public required string PassengerName { get; set; }

    // soit on rend nullable
    public string? Phone { get; set; }

    // ou on lui donne une valeur par défaut
    // public string Phone { get; set; } = string.Empty;

    public int SeatsBooked { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public required string Status { get; set; }
}
