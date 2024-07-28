using System;
using System.Application.Common.Commands;
using System.Application.Models.Auth.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Models.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommand:ICommand<TokenResponse>
    {
        public string? Token { get; set; }
    }
}
