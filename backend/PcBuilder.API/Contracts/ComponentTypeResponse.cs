namespace PcBuilder.API.Contracts
{
    public record ComponentTypeResponse(
        string Name
        );
    
    public record ComponentTypeRequest(
        Guid Id,
        string Name
        );
}
