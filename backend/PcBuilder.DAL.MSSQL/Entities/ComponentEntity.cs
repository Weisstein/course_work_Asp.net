namespace PcBuilder.DAL.MySQL.Entities
{
    public class ComponentEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; } = decimal.Zero;

        public List<ComponentCharactEntity> Characts { get; set; } = [];

        public List<BuildEntity> Builds { get; set; } = [];

        public Guid TypeID { get; set; }

        public ComponentTypeEntity? Type { get; set; }

    }
}
