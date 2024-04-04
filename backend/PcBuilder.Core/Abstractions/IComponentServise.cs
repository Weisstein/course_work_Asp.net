using PcBuilder.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcBuilder.Core.Abstractions
{
    public interface IComponentServise
    {
        Task<Guid> Add(Component component);
        Task<Guid> Delete(Guid id);
        Task<List<Component>> GetAll();
        Task<List<Component>> GetByFilter(Guid? typeId, string? name, Guid? charId, string? value);
        Task<Component> GetById(Guid id);
        Task<Guid> Update(Guid id, string title, string description, decimal price);
    }
}
