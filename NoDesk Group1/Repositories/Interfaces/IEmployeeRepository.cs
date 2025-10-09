using NoDeskGroup1.Models;

namespace NoDesk_Group1.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(string id);
        Task AddAsync(Employee model);
        Task UpdateAsync(string id, Employee model);
        Task DeleteAsync(string id);
    }
}
