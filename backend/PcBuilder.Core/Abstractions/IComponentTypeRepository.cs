﻿using PcBuilder.Core.Models;

namespace PcBuilder.Core.Abstractions
{
    public interface IComponentTypeRepository
    {
        Task<List<ComponentType>> GetAll();
        Task<ComponentType> GetById(Guid id);
        Task<Guid> Add(ComponentType componentType);
        Task<Guid> Delete(Guid id);
        Task<Guid> Update(Guid id, string name);
    }
}
