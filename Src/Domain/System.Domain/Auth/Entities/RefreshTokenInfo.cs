using System;
using System.Collections.Generic;
using System.Domain.Common.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Auth.Entities
{
    public class RefreshTokenInfo:Entity
    {
        public long UserId { get;private set; }
        public string Token { get;private set; }

        public RefreshTokenInfo(long userId, string token)
        {
            UserId = userId;
            Token = token;
        }

        public static RefreshTokenInfo Create(long userId, string token)
        {
            return new RefreshTokenInfo(userId, token);
        }

        public void ChangeToken(string token)
        {
            Token = token;
            Modified();
        }
    }
}
