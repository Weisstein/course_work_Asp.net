namespace PcBuilder.DAL.MySQL.Entities
{
    public class ComponentEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; } = decimal.Zero;

        public List<ComponentCharactEntity> characts { get; set; } = [];

        public List<BuildEntity> builds { get; set; } = [];

        public Guid typeID { get; set; }

        public ComponentTypeEntity? type { get; set; }

    }
}
