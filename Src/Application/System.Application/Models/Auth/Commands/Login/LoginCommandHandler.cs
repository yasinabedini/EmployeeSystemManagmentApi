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
    public class LoginCommandHandler(IUserRepository repository) : ICommandHandler<LoginCommand, TokenResponse>
    {
        public async Task<TokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            //return tuple<bool,string,string,string> =>   item1:flag | item2:message | item3:token | item4:refreshToken
            var result = await repository.SignInAsync(request.EmailAddress!, request.Password!);
            
            return new TokenResponse(result.Item1,result.Item2,result.Item3,result.Item4);
        }
    }
}
