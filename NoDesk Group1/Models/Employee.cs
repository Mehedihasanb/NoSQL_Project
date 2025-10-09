using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NoDeskGroup1.Models
{
    public class Employee
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public DateTime DateJoined { get; set; }
        public bool IsActive { get; set; }

        // default constructor (used by MongoDB deserialization)
        public Employee()
        {
            Id = ObjectId.GenerateNewId();
            FullName = string.Empty;
            Email = string.Empty;
            PasswordHash = string.Empty;
            Role = "ServiceDesk";
            DateJoined = DateTime.UtcNow;
            IsActive = true;
        }

        // parameterized constructor (Helpful when seeding or manual creation)
        public Employee(string fullName, string email, string passwordHash, string role = "ServiceDesk")
        {
            Id = ObjectId.GenerateNewId();
            FullName = fullName;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            DateJoined = DateTime.UtcNow;
            IsActive = true;
        }
    }
}
