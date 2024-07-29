using MediatR;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto;
using System.Application.Models.Auth.Commands.Login;
using System.Application.Models.Auth.Commands.RefreshToken;
using System.Application.Models.Auth.Commands.Register;
using System.Endpoints.Abstractions;

namespace System.Endpoints.Endpoints.Auth
{
    public class AuthEndpoints : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("api/Auth/Register/", async ([FromBody]RegisterCommand command, [FromServices]ISender sender, CancellationToken cancellationToken) =>
            {                
                var response = await sender.Send(command, cancellationToken);

                return response;
            });

            app.MapPost("api/Auth/signin/", async ([FromBody] LoginCommand command, [FromServices] ISender sender, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(command, cancellationToken);

                return response;
            });

            app.MapPost("api/Auth/RefreshToken/", async ([FromBody] RefreshTokenCommand command, [FromServices] ISender sender, CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(command, cancellationToken);

                return response;
            });
        }
    }
}
