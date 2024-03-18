namespace PcBuilder.DAL.MySQL.Entities
{
    public class ComponentTypeEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<ComponentEntity> components { get; set; } = [];
    }
}
