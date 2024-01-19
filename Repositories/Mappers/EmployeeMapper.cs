using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Repositories.Mappers
{
    internal class EmployeeMapper
    {
        public static Employee MapEmployeeFromDataReader(SqlDataReader reader)
        {
            return new Employee
            {
                Id = Convert.ToInt32(reader["Id"]),
                FirstName = Convert.ToString(reader["FirstName"]),
                LastName = Convert.ToString(reader["LastName"]),
                Department = new Department
                {
                    Id = Convert.ToInt32(reader["DepartmentId"]),
                    Name = Convert.ToString(reader["DepartmentName"]),
                    Address = Convert.ToString(reader["DepartmentAddress"]),
                    Phone = Convert.ToString(reader["DepartmentPhone"]),
                }
            };
        }
        public static SqlParameter[] MapEmployeeToSqlParameters(Employee employee)
        {
            return new SqlParameter[]
            {
                new SqlParameter("@Id", employee.Id),
                new SqlParameter("@FirstName", employee.FirstName),
                new SqlParameter("@LastName", employee.LastName),
                new SqlParameter("@DepartmentId", employee.Department.Id)
            };
        }
    }
}
