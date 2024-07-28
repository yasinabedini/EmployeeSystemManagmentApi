using System;
using System.Collections.Generic;
using System.Domain.Common.Entities;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace System.Domain.Town.Entities
{
    public class Town:AggregateRoot<int>
    {
        public string Name { get; set; }


        [JsonIgnore]
        public List<Employee.Entities.Employee> Employees { get; set; }
    }
}
