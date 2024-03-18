using PcBuilder.Core.Models;

namespace PcBuilder.BL.Servises
{
    public interface IComponentTypeServise
    {
        Task<Guid> Add(ComponentType componentType);
        Task<List<ComponentType>> GetAllComponentTypes();
    }
}