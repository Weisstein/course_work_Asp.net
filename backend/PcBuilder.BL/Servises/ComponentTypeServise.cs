using PcBuilder.Core.Abstractions;
using PcBuilder.Core.Models;

namespace PcBuilder.BL.Servises
{
    public class ComponentTypeServise : IComponentTypeServise
    {
        private readonly IComponentTypeRepository _componentTypeRepository;

        public ComponentTypeServise(IComponentTypeRepository componentTypeRepository)
        {
            _componentTypeRepository = componentTypeRepository;
        }

        public async Task<List<ComponentType>> GetAll()
        {
            return await _componentTypeRepository.GetAll();
        }

        public async Task<ComponentType> GetById(Guid id)
        {
            return await _componentTypeRepository.GetById(id);
        }

        public async Task<Guid> Add(ComponentType componentType)
        {
            return await _componentTypeRepository.Add(componentType);
        }
    }
}
