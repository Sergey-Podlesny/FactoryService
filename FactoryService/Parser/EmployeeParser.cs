using Microsoft.Data.SqlClient;
using FactoryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactoryService.Parser
{
    public class EmployeeParser : IParser<List<Employee>>
    {
        List<Employee> employees;
        public void Parse(SqlConnection connection, string DbName)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            
            command.CommandText = $"use {DbName} " +
                $"select Employees.Id, Surname, Firstname, Patronymic, EmploymentDate, Position, CompanyName from Employees" +
                $" join Companies on Employees.CompanyId = Companies.Id";
            try
            {
                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    employees = new List<Employee>();
                    for(int i = 0; reader.Read(); i++)
                    {
                        employees.Add(new Employee
                        {
                            Id = (int)reader.GetValue(0),
                            Surname = (string)reader.GetValue(1),
                            Firstname = (string)reader.GetValue(2),
                            Patronymic = (string)reader.GetValue(3),
                            EmploymentDate = (DateTime)reader.GetValue(4),
                            Position = (string)reader.GetValue(5),
                            Company = (string)reader.GetValue(6)
                        });
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<Employee> GetData()
        {
            return employees;
        }
    }
}
