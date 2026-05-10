using NoDeskGroup1.Models;

namespace NoDesk_Group1.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> GetAllAsync();
        Task<Ticket?> GetByIdAsync(string id);
        Task AddAsync(Ticket model);
        Task UpdateAsync(string id, Ticket model);
        Task DeleteAsync(string id);

        // aggregation for the rubric
        Task<IEnumerable<(string Status, int Count)>> CountByStatusAsync();
    }
}
