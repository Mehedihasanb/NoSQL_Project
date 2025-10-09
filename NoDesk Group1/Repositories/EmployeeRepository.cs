using MongoDB.Bson;
using MongoDB.Driver;
using NoDesk_Group1.Repositories.Interfaces;
using NoDeskGroup1.Models;

namespace NoDeskGroup1.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IMongoCollection<Employee> _col;
        public EmployeeRepository(IMongoDatabase db) => _col = db.GetCollection<Employee>("employees");

        public async Task<IEnumerable<Employee>> GetAllAsync() => await _col.Find(_ => true).ToListAsync();
        public async Task<Employee?> GetByIdAsync(string id) => await _col.Find(x => x.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();
        public async Task AddAsync(Employee m) => await _col.InsertOneAsync(m);
        public async Task UpdateAsync(string id, Employee m) => await _col.ReplaceOneAsync(x => x.Id == ObjectId.Parse(id), m);
        public async Task DeleteAsync(string id) => await _col.DeleteOneAsync(x => x.Id == ObjectId.Parse(id));
    }
}
