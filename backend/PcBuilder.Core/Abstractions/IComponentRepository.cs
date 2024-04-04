using PcBuilder.Core.Models;

namespace PcBuilder.DAL.MySQL.Repositories
{
    public interface IComponentRepository
    {
        Task<Guid> Add(Component component);
        Task<Guid> Delete(Guid id);
        Task<List<Component>> GetAll();
        Task<List<Component>> GetByFilter(Guid? typeId, string? name, Guid? charId, string? value);
        Task<Component> GetById(Guid id);
        Task<Guid> Update(Guid id, string title, string description, decimal price);
    }
}