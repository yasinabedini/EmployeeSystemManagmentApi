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
        Task<Tuple<bool, string>> CreateUser(string Email, string Password, string FullName);
        Task<Tuple<bool, string, ApplicationUser>> SignIn(string Email, string Password);
        ApplicationUser FindUserByEmail(string email);
        string GenerateToken(ApplicationUser user, string role);
        string GenerateRefreshToken();
        string GetUserRole(long userId);
    }
}
