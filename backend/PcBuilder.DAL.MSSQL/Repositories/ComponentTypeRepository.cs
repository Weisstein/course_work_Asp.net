using Microsoft.EntityFrameworkCore;
using PcBuilder.Core.Abstractions;
using PcBuilder.Core.Models;

namespace PcBuilder.DAL.MySQL.Repositories
{
    public class ComponentTypeRepository : IComponentTypeRepository
    {
        private readonly BuilderDBContext _dbContext;

        public ComponentTypeRepository(BuilderDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<List<ComponentType>> GetAll()
        {
            var componentType = await _dbContext.componentTypes
                .AsNoTracking()
                .ToListAsync();

            var componentTypes = componentType
                .Select(ct => ComponentType.Create(ct.Id, ct.Name).componentType)
                .ToList();

            return componentTypes;
        }
    }
}
