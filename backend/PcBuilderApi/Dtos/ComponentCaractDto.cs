namespace PcBuilderApi.Dtos
{
    public record struct ComponentCharactGet
    (
        int Id,
        string Name,
        string Value
    );

    public record struct ComponentCharactPostPut
    (
        string Name,
        string Value
    );
}
