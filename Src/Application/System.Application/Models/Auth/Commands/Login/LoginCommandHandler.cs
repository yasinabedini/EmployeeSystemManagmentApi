using System;
using System.Application.Common.Commands;
using System.Application.Models.Auth.Common;
using System.Collections.Generic;
using System.Domain.Auth.Entities;
using System.Domain.Auth.Repositories;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Models.Auth.Commands.Login
{
    public class LoginCommandHandler(IUserRepository repository) : ICommandHandler<LoginCommand, LoginResponse>
    {
        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.SignIn(request.EmailAddress!, request.Password!);

            if (!result.Item1 || result.Item3.Equals(new ApplicationUser()))
            {
                return new LoginResponse(result.Item1, result.Item2!);
            }

            string userRole = repository.GetUserRole(result.Item3.Id);

            var token = repository.GenerateToken(result.Item3!, userRole);
            var refreshToken = repository.GenerateRefreshToken();

            return await Task.FromResult(new LoginResponse(result.Item1, result.Item2, token, refreshToken));
        }
    }
}
