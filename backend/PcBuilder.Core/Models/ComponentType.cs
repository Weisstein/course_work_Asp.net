namespace PcBuilder.Core.Models
{
    public class ComponentType
    {
        public const int MAS_SIZE_LENGTH = 255;

        private ComponentType(Guid id, string name) 
        { 
            Id = id;
            Name = name;
        }

        public Guid Id { get; }

        public string Name { get; } = string.Empty;

        public static (ComponentType componentType, string Error) Create(Guid id, string name)
        {
            var error = string.Empty;

            if (name.Length > MAS_SIZE_LENGTH)
            {
                error = $"Поле не может быть более {MAS_SIZE_LENGTH} символов";
            }

            if (string.IsNullOrEmpty(name))
            {
                error = "Поле не может быть пустым";
            }

            var componentType = new ComponentType(id, name);

            return (componentType, error);
        }
    }
}
