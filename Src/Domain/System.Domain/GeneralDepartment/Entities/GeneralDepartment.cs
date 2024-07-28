using System.Domain.Common.Entities;
using System.Domain.Employee.Entities;
using System.Text.Json.Serialization;

namespace System.Domain.GeneralDepartment.Entities
{
    public class GeneralDepartment:AggregateRoot<int>
    {
        public string Name { get; set; }


        [JsonIgnore]
        public List<Employee.Entities.Employee> Employees { get; set; }
    }
}
