using PcBuilder.Core.Models;

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

        }


    }
}
