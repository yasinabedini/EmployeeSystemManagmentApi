using System;
using System.Application.Common.Commands;
using System.Application.DTOs.Auth;
using System.Application.Models.Auth.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Application.Models.Auth.Commands.Register
{
    public class RegisterCommand:AccountBase,ICommand<GeneralResponse>
    {
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string? FullName { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [Required]
        public string? ConfirmPassword { get; set; }
    }
}
