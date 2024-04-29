using PcBuilderApi.Models;

namespace PcBuilderApi.Dtos
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
