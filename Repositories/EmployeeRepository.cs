using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Models;
using Test.Repositories.Mappers;

namespace Test.Repositories
{
    internal class EmployeeRepository : LocalDBRepository
    {

        public List<Employee> GetAll()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT " +
                    "Employees.Id AS Id, " +
                    "Employees.FirstName AS FirstName, " +
                    "Employees.LastName AS LastName, " +
                    "Departments.Id AS DepartmentId, " +
                    "Departments.Name AS DepartmentName, " +
                    "Departments.Address AS DepartmentAddress, " +
                    "Departments.Phone AS DepartmentPhone " +
                    "FROM Employees " +
                    "JOIN Departments ON Employees.DepartmentId = Departments.Id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee department = EmployeeMapper.MapEmployeeFromDataReader(reader);
                        employees.Add(department);
                    }
                }
            }

            return employees;
        }

        public int Create()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Employees" +
                               "(FirstName, LastName, DepartmentId) " +
                               "OUTPUT INSERTED.Id " +
                               "VALUES " +
                               "(@FirstName, @LastName, @DepartmentId)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", "");
                    command.Parameters.AddWithValue("@LastName", "");
                    command.Parameters.AddWithValue("@DepartmentId", 1);

                    int newEmployeeId = Convert.ToInt32(command.ExecuteScalar());

                    return newEmployeeId;
                }
            }
        }

        public Employee Get(int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT " +
                    "Employees.Id AS Id, " +
                    "Employees.FirstName AS FirstName, " +
                    "Employees.LastName AS LastName, " +
                    "Departments.Id AS DepartmentId, " +
                    "Departments.Name AS DepartmentName, " +
                    "Departments.Address AS DepartmentAddress, " +
                    "Departments.Phone AS DepartmentPhone " +
                    "FROM Employees " +
                    "JOIN Departments ON Employees.DepartmentId = Departments.Id " +
                    "WHERE Employees.Id = @Id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", employeeId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Employee employee = EmployeeMapper.MapEmployeeFromDataReader(reader);
                            return employee;
                        }
                    }
                }
                throw new InvalidOperationException("Get operation failed.");
            }
        }

        public Employee Update(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE Employees " +
                               "SET FirstName = @FirstName, " +
                               "    LastName = @LastName, " +
                               "    DepartmentId = @DepartmentId " +
                               "WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlParameter[] parameters = EmployeeMapper.MapEmployeeToSqlParameters(employee);
                    command.Parameters.AddRange(parameters);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return employee;
                    }
                }
            }
            throw new InvalidOperationException("Update operation failed. No rows were affected.");
        }
    }
}
