using System;
using System.Application.Common.Commands;
using System.Application.Models.Auth.Common;
using System.Collections.Generic;
using System.Domain.Auth.Repositories;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Models.Auth.Commands.Register
{
    public class RegisterCommandHandler(IUserRepository repository) : ICommandHandler<RegisterCommand, GeneralResponse>
    {
        public async Task<GeneralResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (!request.Password!.Equals(request.ConfirmPassword)) return new GeneralResponse(false, "Password not match with password Confirmation ...");

            var result = await repository.CreateUserAsync(request.EmailAddress!, request.Password, request.FullName!);

            return new GeneralResponse(result.Item1, result.Item2);
        }
    }
}
