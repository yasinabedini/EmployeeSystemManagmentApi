using System;
using System.Collections.Generic;
using System.Domain.Common.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Auth.Entities
{
    public class ApplicationUser:AggregateRoot
    {
        public string Email { get;private set; }
        public string FullName { get; private set; }
        public string Password { get; private set; }

        public ApplicationUser()
        {
            
        }
        public ApplicationUser(string email, string fullName, string password)
        {
            Email = email;
            FullName = fullName;
            Password = password;
        }
        public static ApplicationUser Create(string email, string fullName, string password)
        {
            return new ApplicationUser(email, fullName, password);
        }
    }
}
