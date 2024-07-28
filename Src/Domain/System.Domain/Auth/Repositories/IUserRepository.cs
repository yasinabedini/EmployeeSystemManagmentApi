using System;
using System.Collections.Generic;
using System.Domain.Auth.Entities;
using System.Domain.Common.Repositories;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Auth.Repositories
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        //item1:flag | item2:message
        Task<Tuple<bool, string>> CreateUserAsync(string Email, string Password, string FullName);

        //item1:flag | item2:message | item3:token | item4:refreshToken
        Task<Tuple<bool, string, string, string>> SignInAsync(string Email, string Password);
        ApplicationUser FindUserByEmail(string email);
        string GenerateToken(ApplicationUser user, string role);
        string GenerateRefreshToken();
        string GetUserRole(long userId);

        //item1:flag | item2:message | item3:token | item4:refreshToken
        Task<Tuple<bool, string, string, string>> RefreshTokenAsync(string token);
    }
}
