using System;
using System.Application.Common.Commands;
using System.Application.DTOs.Auth;
using System.Application.Models.Auth.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Models.Auth.Commands.Login
{
    public class LoginCommand:AccountBase,ICommand<LoginResponse>
    {
    }
}
