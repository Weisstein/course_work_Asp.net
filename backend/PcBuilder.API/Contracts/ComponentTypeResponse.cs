namespace PcBuilder.API.Contracts
{
    public record ComponentTypeResponse(
        Guid Id,
        string Name
        );
    
    public record ComponentTypeRequest(
        Guid Id,
        string Name
        );
}
