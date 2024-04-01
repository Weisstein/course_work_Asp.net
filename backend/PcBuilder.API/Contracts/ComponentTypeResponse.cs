namespace PcBuilder.API.Contracts
{
    public record ComponentTypeResponse(
        Guid id,
        string Name
        );
    
    public record ComponentTypeRequest(
        string Name
        );
}
