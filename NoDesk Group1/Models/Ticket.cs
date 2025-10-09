using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NoDeskGroup1.Models
{
    public class Ticket
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }

        public ObjectId ServiceId { get; set; }
        public ObjectId? CategoryId { get; set; }

        public ObjectId ReportedByCustomerId { get; set; }
        public ObjectId LoggedByEmployeeId { get; set; }
        public ObjectId? AssignedToEmployeeId { get; set; }
        public ObjectId? ResolvedByEmployeeId { get; set; }

        public string? CloseReason { get; set; }
        public string RefNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime? ClosedAt { get; set; }

        //  default constructor (MongoDB uses this)
        public Ticket()
        {
            Id = ObjectId.GenerateNewId();
            Title = string.Empty;
            Description = string.Empty;
            Status = "Open";
            Priority = "Medium";
            RefNumber = $"TCK-{DateTime.UtcNow.Ticks}";
            CreatedAt = DateTime.UtcNow;
            LastUpdated = DateTime.UtcNow;
        }

        //  parameterized constructor(Helpful when seeding or manual creation)
        public Ticket(string title, string description, string priority = "Medium", string status = "Open")
        {
            Id = ObjectId.GenerateNewId();
            Title = title;
            Description = description;
            Priority = priority;
            Status = status;
            RefNumber = $"TCK-{DateTime.UtcNow.Ticks}";
            CreatedAt = DateTime.UtcNow;
            LastUpdated = DateTime.UtcNow;
        }
    }
}
