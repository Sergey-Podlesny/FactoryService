using Microsoft.Data.SqlClient;
using FactoryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactoryService.Parser
{
    public class CompanyParser : IParser<List<Company>>
    {
        List<Company> companies;
        public void Parse(SqlConnection connection, string DbName)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = $"use {DbName} select * from Companies";
            
            try
            {
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    companies = new List<Company>();
                    for (int i = 0; reader.Read(); i++)
                    {
                        companies.Add(new Company
                        {
                            Id = (int)reader.GetValue(0),
                            CompanyName = (string)reader.GetValue(1),
                            CompanyForm = (string)reader.GetValue(2)
                        });
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
     
        public List<Company> GetData()
        {
            return companies;
        }
    }
}
