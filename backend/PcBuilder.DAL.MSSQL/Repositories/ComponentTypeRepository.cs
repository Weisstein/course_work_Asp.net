using Microsoft.EntityFrameworkCore;
using PcBuilder.Core.Abstractions;
using PcBuilder.Core.Models;
using PcBuilder.DAL.MySQL.Entities;

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
            var componentTypeEntity = await _dbContext.componentTypes
                .AsNoTracking()
                .ToListAsync();

            var componentTypes = componentTypeEntity
                .Select(ct => ComponentType.Create(ct.Id, ct.Name).componentType)
                .ToList();

            return componentTypes;
        }

        public async Task<ComponentType> GetById(Guid id)
        {
            var componentTypeEntity = await _dbContext.componentTypes
                .FirstOrDefaultAsync(ct => ct.Id == id) ?? throw new Exception("");
                

            var componentType = ComponentType.Create(componentTypeEntity.Id, componentTypeEntity.Name).componentType;
                

            return componentType;
        }

        public async Task<Guid> Add(ComponentType componentType)
        {
            var componentTypeEntity = new ComponentTypeEntity
            {
                Id = componentType.Id,
                Name = componentType.Name
            };

            await _dbContext.componentTypes.AddAsync(componentTypeEntity);
            await _dbContext.SaveChangesAsync();

            return componentTypeEntity.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _dbContext.componentTypes
                .Where(ct => ct.Id == id)
                .ExecuteDeleteAsync();

            await _dbContext.SaveChangesAsync();
            return id;
        }

        public async Task<Guid> Update(Guid id, string name)
        {
            await _dbContext.componentTypes
                .Where(ct => ct.Id == id)
                .ExecuteUpdateAsync(ct => 
                 ct.SetProperty(ct => ct.Name, name));
            await _dbContext.SaveChangesAsync();

            return id;
        }
    }
}
