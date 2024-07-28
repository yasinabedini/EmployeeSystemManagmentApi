using System;
using System.Application.Common.Commands;
using System.Application.Models.Auth.Common;
using System.Collections.Generic;
using System.Domain.Auth.Repositories;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Models.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler(IUserRepository repository) : ICommandHandler<RefreshTokenCommand, TokenResponse>
    {
        public async Task<TokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.RefreshTokenAsync(request.Token!);

            return new TokenResponse(result.Item1,result.Item2,result.Item3,result.Item4);
        }
    }
}
