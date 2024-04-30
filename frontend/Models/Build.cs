using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace frontend.Models
{
    public class Build
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "Введите имя сборки")]
        [StringLength(128, ErrorMessage = "Поле не может превышать 128 символов")]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
       
        public ICollection<System.ComponentModel.Component>? Components { get; set; }
    }
}
