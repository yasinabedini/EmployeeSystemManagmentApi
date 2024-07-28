using System.Collections.Generic;
using System.Domain.Common.Entities;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace System.Domain.Employee.Entities
{
    public class Employee:AggregateRoot<long>
    {
        public string CivilId { get;private set; }
        public string FileNumber { get; private set; }
        public string FullName { get; private set; }
        public string JobName { get; private set; }
        public string TelphoneNumber { get; private set; }
        public string Address { get; private set; }
        public string Photo { get;private set; }
        public string Other { get; private set; }


        public GeneralDepartment.Entities.GeneralDepartment GeneralDepartment { get; set; }
        public int GeneralDepartmentId { get; set; }

        public Department.Entities.Department Department { get; set; }
        public int DepartmentId { get; set; }

        public Branch.Entities.Branch Branch { get; set; }
        public int BranchId { get; set; }

        public Town.Entities.Town Town { get; set; }
        public int TownId { get; set; }

        public Employee(string civilId, string fileNumber, string fullName, string jobName, string telphoneNumber, string address, string photo, string other)
        {
            CivilId = civilId;
            FileNumber = fileNumber;
            FullName = fullName;
            JobName = jobName;
            TelphoneNumber = telphoneNumber;
            Address = address;
            Other = other;
            Photo = photo;
        }
        public static Employee Create(string civilId, string fileNumber, string fullName, string jobName, string telphoneNumber, string address,string photo ,string other)
        {
            return new Employee(civilId, fileNumber, fullName, jobName, telphoneNumber, address,photo, other);
        }
    }
}
