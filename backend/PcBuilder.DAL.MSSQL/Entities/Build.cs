using System.ComponentModel;
namespace PcBuilder.DAL.MySQL.Entities
{
    public class Build
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<Component> components { get; set; } = [];
    }
}
