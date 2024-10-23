namespace frontend.Models
{
    public class ComponentCharact
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        public int ComponentId { get; set; }

        public Component? Component { get; set; }
    }
}
