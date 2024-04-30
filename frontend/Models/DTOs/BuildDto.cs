using System.ComponentModel;

namespace frontend.Models.DTOs
{
    public record struct BuildGet
     (
         int Id,
         string Name,
         string Description,
         ICollection<Component> Components
     );

    public record struct BuildPostPut
    (
        string Name,
        string Descriptions,
        List<int> componentIds
    );
}
