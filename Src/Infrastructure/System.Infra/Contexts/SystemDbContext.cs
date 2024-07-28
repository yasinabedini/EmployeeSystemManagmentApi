using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Domain.Auth.Entities;
using System.Domain.Branch.Entities;
using System.Domain.Common.Entities;
using System.Domain.Common.ValueObjects;
using System.Domain.Department.Entities;
using System.Domain.Employee.Entities;
using System.Domain.GeneralDepartment.Entities;
using System.Domain.Town.Entities;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System.Infra.Contexts
{
    public class SystemDbContext : DbContext
    {
        public SystemDbContext(DbContextOptions<SystemDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<GeneralDepartment> GeneralDepartments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<SystemRole> SystemRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RefreshTokenInfo> RefreshTokenInfos { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Ignore(typeof(BusinessId));

            

            base.OnModelCreating(builder);
        }
    }
}

