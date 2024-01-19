using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;
using Test.Repositories.Mappers;

namespace Test.Repositories
{
    internal class DepartmentRepository : LocalDBRepository
    {

        public List<Department> GetAll()
        {
            List<Department> departments = new List<Department>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Departments";
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Department department = DepartmentMapper.MapDepartmentFromDataReader(reader);
                        departments.Add(department);
                    }
                }
            }

            return departments;
        }

        public Department Update(Department department)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE Departments " +
                               "SET Name = @Name, " +
                               "    Address = @Address, " +
                               "    Phone = @Phone " +
                               "WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlParameter[] parameters = DepartmentMapper.MapDepartmentToSqlParameters(department);
                    command.Parameters.AddRange(parameters);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return department;
                    }
                }
            }
            throw new InvalidOperationException("Update operation failed. No rows were affected.");
        }

        internal int Create()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Departments" +
                               "(Name, Address, Phone) " +
                               "OUTPUT INSERTED.Id " +
                               "VALUES " +
                               "(@Name, @Address, @Phone)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", "");
                    command.Parameters.AddWithValue("@Address", "");
                    command.Parameters.AddWithValue("@Phone", "");

                    int newDepartmentId = Convert.ToInt32(command.ExecuteScalar());

                    return newDepartmentId;
                }
            }
        }

        internal Department Get(int departmentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * " +
                    "FROM Departments " +
                    "WHERE Departments.Id = @Id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", departmentId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Department department = DepartmentMapper.MapDepartmentFromDataReader(reader);
                            return department;
                        }
                    }
                }
                throw new InvalidOperationException("Get operation failed.");
            }
        }
    }
}
