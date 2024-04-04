using PcBuilder.Core.Abstractions;
using PcBuilder.Core.Models;
using PcBuilder.DAL.MySQL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcBuilder.BL.Servises
{
    public class ComponentServise : IComponentServise
    {
        private readonly IComponentRepository _componentRepository;

        public ComponentServise(IComponentRepository repository)
        {
            _componentRepository = repository;
        }

        public async Task<Guid> Add(Component component)
        {
            return await _componentRepository.Add(component);
        }

        public async Task<Guid> Delete(Guid id)
        {
            return await _componentRepository.Delete(id);
        }

        public async Task<List<Component>> GetAll()
        {
           return await _componentRepository.GetAll();
        }

        public async Task<List<Component>> GetByFilter(string? name, string? value)
        {
           return await _componentRepository.GetByFilter(name, value);
        }

        public async Task<Component> GetById(Guid id)
        {
            return await _componentRepository.GetById(id);
        }

        public async Task<Guid> Update(Guid id, string title, string description, decimal price)
        {
           return await _componentRepository.Update(id, title, description, price);
        }
    }
}
