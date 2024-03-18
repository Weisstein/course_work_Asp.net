namespace PcBuilder.DAL.MySQL.Entities
{
    public class ComponentCharactEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Value {  get; set; } = string.Empty;

        public Guid componentId { get; set; }

        public ComponentEntity? component { get; set; }
    }
}
