using System;
using System.Collections.Generic;
using System.Domain.Common.Entities;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace System.Domain.Branch.Entities
{
    public class Branch:AggregateRoot<int>
    {
        public string Name { get; set; }


        [JsonIgnore]
        public List<Employee.Entities.Employee> Employees { get; set; }
    }
}
