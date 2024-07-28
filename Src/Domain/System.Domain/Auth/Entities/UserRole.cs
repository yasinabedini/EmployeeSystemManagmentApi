using System;
using System.Collections.Generic;
using System.Domain.Common.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Auth.Entities
{
    public class UserRole:Entity<long>
    {
        public long UserId { get;private set; }
        public int RoleId { get; private set; }

        public UserRole(long userId, int roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
        public static UserRole Create(long userId, int roleId)
        {
            return new UserRole(userId, roleId);
        }
    }
}
