namespace PcBuilder.API.Contracts
{
    public record ComponentResponse
    (
        Guid Id,
        string Title,
        string Description,
        decimal Price,
        Guid TyptId
    );

    public record ComponentRequest
    (
        string Title,
        string Description,
        decimal Price,
        Guid TyptId
    );

    public record ComponentRequestPut
    (
        string Title,
        string Description,
        decimal Price
    );
}
