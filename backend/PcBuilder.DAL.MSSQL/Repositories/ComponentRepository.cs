using Microsoft.EntityFrameworkCore;
using PcBuilder.Core.Models;
using PcBuilder.DAL.MySQL.Entities;
using System.Runtime.InteropServices.ObjectiveC;
using System.Threading.Tasks.Dataflow;

namespace PcBuilder.DAL.MySQL.Repositories
{
    public class ComponentRepository : IComponentRepository
    {
        private readonly BuilderDBContext _dbContext;

        public ComponentRepository(BuilderDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Component>> GetAll()
        {
            var componentEntity = await _dbContext.components
                .AsNoTracking()
                .ToListAsync();

            var components = componentEntity
                .Select(c => Component.Create(c.Id, c.Title, c.Description, c.Price, c.TypeID).component)
                .ToList();

            return components;
        }

        public async Task<Component> GetById(Guid id)
        {
            var componentEntity = await _dbContext.components
                .FirstOrDefaultAsync(c => c.Id == id) ?? throw new Exception("");

            var component = Component.Create(componentEntity.Id, componentEntity.Title, componentEntity.Description, componentEntity.Price, componentEntity.TypeID).component;

            return component;
        }

        public async Task<List<Component>> GetByFilter(string? name, string? value)
        {
            var componentEntity = await (from c in _dbContext.components
                          join ct in _dbContext.componentTypes on c.TypeID equals ct.Id
                          join cc in _dbContext.componentCharacts on c.Id equals cc.componentId
                          where EF.Functions.Like(ct.Name,name)
                          where EF.Functions.Like(cc.Value,value)
                          select new
                          {
                              ComponentId = c.Id,
                              ComponentTitle = c.Title,
                              ComponentDesc = c.Description,
                              ComponentPrice = c.Price,
                              ComponentTypeID = c.TypeID,
                          }).AsNoTracking().ToListAsync();

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
            {
                componentEntity = await (from c in _dbContext.components
                                         join ct in _dbContext.componentTypes on c.TypeID equals ct.Id
                                         join cc in _dbContext.componentCharacts on c.Id equals cc.componentId
                                         where EF.Functions.Like(ct.Name, name)
                                         where EF.Functions.Like(cc.Value, value)
                                         select new
                                         {
                                             ComponentId = c.Id,
                                             ComponentTitle = c.Title,
                                             ComponentDesc = c.Description,
                                             ComponentPrice = c.Price,
                                             ComponentTypeID = c.TypeID,
                                         }).AsNoTracking().ToListAsync();
            } 
            else
            {
                if (!string.IsNullOrEmpty(name))
                {
                    componentEntity = await (from c in _dbContext.components
                                         join ct in _dbContext.componentTypes on c.TypeID equals ct.Id
                                         where EF.Functions.Like(ct.Name, name)
                                         select new
                                         {
                                             ComponentId = c.Id,
                                             ComponentTitle = c.Title,
                                             ComponentDesc = c.Description,
                                             ComponentPrice = c.Price,
                                             ComponentTypeID = c.TypeID,
                                         }).AsNoTracking().ToListAsync();
                }
            }

           

            var components = componentEntity
                .Select(c => Component.Create(c.ComponentId,c.ComponentTitle,c.ComponentDesc,c.ComponentPrice,c.ComponentTypeID).component)
                .ToList();

            return components;
        }

        public async Task<Guid> Add(Component component)
        {
            var componentEntity = new ComponentEntity
            {
                Id = component.Id,
                Title = component.Title,
                Description = component.Description,
                Price = component.Price,
                TypeID = component.TypeID
            };

            await _dbContext.components.AddAsync(componentEntity);
            await _dbContext.SaveChangesAsync();

            return componentEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string title, string description, decimal price)
        {
            await _dbContext.components
                .Where(c => c.Id == id)
                .ExecuteUpdateAsync(c =>
                    c.SetProperty(c => c.Title, title)
                     .SetProperty(c => c.Description, description)
                     .SetProperty(c => c.Price, price)
                );

            await _dbContext.SaveChangesAsync();
            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _dbContext.components
                .Where(c => c.Id == id)
                .ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();

            return id;
        }
    }
}
