namespace PcBuilderApi.Dtos
{
    public record struct ComponentTypeGet
    (
        int Id,
        string Name
    );

    public record struct ComponentTypePostPut
    (
         string Name
    );
}
