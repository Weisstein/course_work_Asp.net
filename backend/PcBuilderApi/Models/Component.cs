using System.Text.Json.Serialization;

namespace PcBuilderApi.Models
{
    public class Component
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; } = decimal.Zero;

        public int TypeId { get; set; }

        public ComponentType? Type { get; set; }

        public ICollection<ComponentCharact>? Characts { get; set; }
        [JsonIgnore]
        public ICollection<Build>? Builds { get; set; }
    }
}
