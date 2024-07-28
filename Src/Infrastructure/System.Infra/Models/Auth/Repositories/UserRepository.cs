using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Bcpg;
using System.Domain.Auth.Entities;
using System.Domain.Auth.Repositories;
using System.Domain.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Infra.Common.Repository;
using System.Infra.Contexts;
using System.Infra.Helpers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace System.Infra.Models.Auth.Repositories
{
    public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
    {
        public SystemDbContext Context { get; }

        public IOptions<JwtSection> Config { get; set; }

        public UserRepository(SystemDbContext context, IOptions<JwtSection> config) : base(context)
        {
            Context = context;
            Config = config;
        }

        public async Task<Tuple<bool, string>> CreateUserAsync(string email, string password, string fullName)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(fullName)) return Tuple.Create(false, "Model Is Empty");

            var checkAvailibility = FindUserByEmail(email);
            if (checkAvailibility is not null) return Tuple.Create(false, "User Registered Already");

            string hashPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var createdUser = ApplicationUser.Create(email, fullName, hashPassword);
            Add(createdUser);
            Save();

            // If Does'n Exits any role for admin in database
            if (!Context.SystemRoles.Any(t => t.Name.Equals(Constans.Admin)))
            {
                var createdRole = SystemRole.Create(Constans.Admin);
                Context.SystemRoles.Add(createdRole);
                Save();
                await Context.UserRoles.AddAsync(UserRole.Create(createdUser.Id, createdRole.Id));
                Save();
                return Tuple.Create(true, "Account Created");
            }

            var userRole = Context.SystemRoles.FirstOrDefault(t => t.Name.Equals(Constans.User));
            if (userRole is null)
            {
                var createdRole = SystemRole.Create(Constans.User);
                Context.SystemRoles.Add(createdRole);
                Save();
                await Context.UserRoles.AddAsync(UserRole.Create(createdUser.Id, createdRole.Id));
                Save();
            }
            else
            {
                Context.UserRoles.Add(UserRole.Create(createdUser.Id, userRole.Id));
                Save();
            }

            return Tuple.Create(true, "Account Created");
        }
        
        public async Task<Tuple<bool, string, string, string>> SignInAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) return Tuple.Create(false, "Model is empty", "", "");

            var user = FindUserByEmail(email);

            if (user is null) return Tuple.Create(false, "User not found", "", "");

            if (Context.UserRoles.Any(t => t.UserId == user.Id)) return Tuple.Create(false, "User Not Found", "", "");

            var role = GetUserRole(user.Id);

            var token = GenerateToken(user, role);
            var refreshToken = GenerateRefreshToken();

            //Save Refresh Token
            var findToken =await Context.RefreshTokenInfos.FirstOrDefaultAsync(t => t.UserId == user.Id);
            if (findToken is not null)
            {
                findToken.ChangeToken(refreshToken);
                Context.Update(findToken);
                Save();
            }
            else
            {
                Context.RefreshTokenInfos.Add(RefreshTokenInfo.Create(user.Id, refreshToken));
                Save();
            }

            return Tuple.Create(true, "Login Suucessfully..", token, refreshToken);
        }

        public ApplicationUser FindUserByEmail(string email)
        {
            return Context.ApplicationUsers.FirstOrDefault(t => t.Email.ToLower().Equals(email.ToLower()));
        }

        public string GenerateToken(ApplicationUser user, string role)
        {
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config.Value.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.FullName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,role)
            };

            var token = new JwtSecurityToken(
                                               issuer: Config.Value.Issuer,
                                               audience: Config.Value.Audience,
                                               claims: userClaims,
                                               expires: DateTime.Now.AddDays(2),
                                               signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        public string GetUserRole(long userId)
        {
            var roleId = Context.UserRoles.FirstOrDefault(x => x.UserId == userId).RoleId;

            return Context.SystemRoles.Find(roleId).Name;
        }

        public async Task<Tuple<bool, string, string, string>> RefreshTokenAsync(string token)
        {
            if (string.IsNullOrEmpty(token)) return Tuple.Create(false, "Model is empty", "", "");

            var findToken = Context.RefreshTokenInfos.FirstOrDefault(t => t.Token.Equals(token));
            if (findToken is null)
            {
                return Tuple.Create(false, "The entered Token is not valid", "", "");
            }

            var findUser = await Context.ApplicationUsers.FirstOrDefaultAsync(t => t.Id == findToken.UserId);
            if (findUser is null)
            {
                return Tuple.Create(false, "The entered Token is not valid", "", "");
            }

            var roleName = GetUserRole(findUser.Id);

            var createdToken = GenerateToken(findUser, roleName);
            var refreshToken = GenerateRefreshToken();

             findToken.ChangeToken(refreshToken);
             Context.Update(findToken);
            Save();

            return Tuple.Create(true, "Token refresh Successfully", createdToken, refreshToken);
        }
    }
}
