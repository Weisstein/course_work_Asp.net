using Microsoft.EntityFrameworkCore;
using PcBuilder.Core.Models;
using PcBuilder.DAL.MySQL.Entities;
using System.Runtime.InteropServices.ObjectiveC;

namespace PcBuilder.DAL.MySQL.Repositories
{
    public class ComponentRepository
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
                .Select(c => Component.Create(c.Id,c.Title,c.Description,c.Price,c.TypeID).component)
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

        public async Task<List<Component>> GetByFilter(Guid? typeId, string? name, Guid? charId, string? value)
        {
            var querry = (from c in _dbContext.components
                              join ct in _dbContext.componentTypes on c.TypeID equals ct.Id
                              join cc in _dbContext.componentCharacts on c.Id equals cc.componentId
                              select new
                              {
                                  c.Id,
                                  c.Title,
                                  c.Description,
                                  c.Price,
                                  TypeId = ct.Id,
                                  ct.Name,
                                  CharId = cc.Id,
                                  cc.Value
                              }).AsNoTracking();

            if (typeId != Guid.Empty)
            {
                querry = querry.Where(ct => ct.TypeId == typeId);
            }

            if (!string.IsNullOrEmpty(name)) 
            {
                querry = querry.Where(ct => ct.Name.Contains(name));
            }

            if (charId != Guid.Empty)
            {
                querry = querry.Where(cc => cc.CharId == charId);
            }

            if (!string.IsNullOrEmpty(value))
            {
                querry = querry.Where(cc => cc.Value == value);
            }

            var components = querry.Select(c => Component.Create(c.Id,c.Title,c.Description,c.Price,c.TypeId).component).ToListAsync();

            return await components;
        }
    }
}
