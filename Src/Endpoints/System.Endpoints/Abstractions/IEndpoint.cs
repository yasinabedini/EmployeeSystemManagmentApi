namespace System.Endpoints.Abstractions;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}