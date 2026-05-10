using MongoDB.Bson;
using MongoDB.Driver;
using NoDesk_Group1.Repositories.Interfaces;
using NoDeskGroup1.Models;

namespace NoDeskGroup1.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly IMongoCollection<Ticket> _col;
        public TicketRepository(IMongoDatabase db) => _col = db.GetCollection<Ticket>("tickets");

        public async Task<IEnumerable<Ticket>> GetAllAsync() => await _col.Find(_ => true).ToListAsync();
        public async Task<Ticket?> GetByIdAsync(string id) => await _col.Find(x => x.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();
        public async Task AddAsync(Ticket m) => await _col.InsertOneAsync(m);
        public async Task UpdateAsync(string id, Ticket m)
        {
            m.LastUpdated = DateTime.UtcNow;
            await _col.ReplaceOneAsync(x => x.Id == ObjectId.Parse(id), m);
        }
        public async Task DeleteAsync(string id) => await _col.DeleteOneAsync(x => x.Id == ObjectId.Parse(id));

        // aggregation for rubric
        public async Task<IEnumerable<(string Status, int Count)>> CountByStatusAsync()
        {
            var results = await _col.Aggregate()
                .Group(t => t.Status, g => new { Status = g.Key, Count = g.Count() })
                .SortByDescending(x => x.Count)
                .ToListAsync();

            return results.Select(x => (x.Status, x.Count));
        }
    }
}
