using System;
using System.Collections.Generic;
using System.Domain.Common.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Auth.Entities
{
    public class SystemRole:Entity<int>
    {
        public string Name { get;private set; }

        public SystemRole(string name)
        {
            Name = name;
        }
        public static SystemRole Create(string name) {return new SystemRole(name); }
    }
}
