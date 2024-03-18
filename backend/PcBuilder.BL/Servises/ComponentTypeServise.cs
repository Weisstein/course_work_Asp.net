using PcBuilder.Core.Abstractions;
using PcBuilder.Core.Models;

namespace PcBuilder.BL.Servises
{
    public class ComponentTypeServise
    {
        private readonly IComponentTypeRepository _componentTypeRepository;

        public ComponentTypeServise(IComponentTypeRepository componentTypeRepository)
        {
            _componentTypeRepository = componentTypeRepository;
        }

        public async Task<List<ComponentType>> GetAllComponentTypes()
        {
            return await _componentTypeRepository.GetAll();
        }

        public async Task<Guid> Add(ComponentType componentType) 
        {
            return await _componentTypeRepository.Add(componentType);
        }
    }
}
