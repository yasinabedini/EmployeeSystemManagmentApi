using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Models.Auth.Common
{
    public record LoginResponse(bool flag, string? message,string? token = "",string? refreshToken = "");

}
