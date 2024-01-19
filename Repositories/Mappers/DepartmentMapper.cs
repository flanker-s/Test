using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Repositories.Mappers
{
    internal class DepartmentMapper
    {
        public static Department MapDepartmentFromDataReader(SqlDataReader reader)
        {
            return new Department
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = Convert.ToString(reader["Name"]),
                Address = Convert.ToString(reader["Address"]),
                Phone = Convert.ToString(reader["Phone"])
            };
        }
        public static SqlParameter[] MapDepartmentToSqlParameters(Department department)
        {
            return new SqlParameter[]
            {
                new SqlParameter("@Id", department.Id),
                new SqlParameter("@Name", department.Name),
                new SqlParameter("@Address", department.Address),
                new SqlParameter("@Phone", department.Phone)
            };
        }
    }
}
