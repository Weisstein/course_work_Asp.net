namespace PcBuilderApi.Dtos
{
    public record struct ComponentGet
    (
        int Id,
        string Name,
        string Description,
        decimal Price
    );

    public record struct ComponentPut
    (
        string Name,
        string Description,
        decimal Price
    );

    public record struct ComponentPost
    (
        string Name,
        string Description,
        decimal Price,
        int TypeId,
        List<ComponentCharactPostPut> Characts
    );
}
