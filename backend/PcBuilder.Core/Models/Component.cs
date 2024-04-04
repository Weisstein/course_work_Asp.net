namespace PcBuilder.Core.Models
{
    public class Component
    {
        public const int MAX_SIZE_LENGTH = 100;

        private Component(Guid id, string title, string description, decimal price, Guid typeId)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
            TypeID = typeId;
        }

        public Guid Id { get; set; } = Guid.Empty;

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; } = decimal.Zero;

        public List<ComponentCharact> Characts { get; set; } = [];

        public List<Build> Builds { get; set; } = [];

        public Guid TypeID { get; set; } = Guid.Empty;

        public ComponentType? Type { get; set; } = null;

        public static (Component component, string error) Create(Guid id, string title, string description, decimal price, Guid typeId)
        {
            var error = string.Empty;

            if (title.Length > MAX_SIZE_LENGTH)
            {
                error = $"Поле не моет быть боолее {MAX_SIZE_LENGTH} символов" ;
            }

            if (string.IsNullOrEmpty(title))
            {
                error = "Поле не может быть пустым";
            }

            if (string.IsNullOrEmpty(description))
            {
                error = "Поле не может быть пустым";
            }

            if (price == decimal.Zero)
            {
                error = "Поле не может равняться 0";
            }

            if (typeId == Guid.Empty) 
            {
                error = "Поле не моет быть пустым";
            }

            var component = new Component(id,title,description,price,typeId);
            return (component, error);
        }
    }
}
