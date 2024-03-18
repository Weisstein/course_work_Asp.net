using PcBuilder.Core.Models;

namespace PcBuilder.Core.Abstractions
{
    public interface IComponentTypeRepository
    {
        Task<List<ComponentType>> GetAll();
    }
}
