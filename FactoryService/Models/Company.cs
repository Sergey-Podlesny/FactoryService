using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace FactoryService.Models
{
    public class Company : ITable
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyForm { get; set; }

        public bool IsExist(SqlConnection connection, string DbName)
        {
            SqlCommand command = new SqlCommand();
            bool isExist = false;
            command.CommandText = $"use {DbName} select count(table_name) " +
                $"from INFORMATION_SCHEMA.tables " +
                $"where table_schema = 'dbo' " +
                $"and table_name = 'Companies'";
            command.Connection = connection;
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                if((int)reader.GetValue(0) > 0)
                {
                    isExist = true;
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return isExist;
        }

        public void CreateTable(SqlConnection connection, string DbName)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = $"use {DbName} create table Companies (" +
                $"Id int primary key identity, " +
                $"CompanyName nvarchar(50) not null, " +
                $"CompanyForm nvarchar(50) not null)";
            command.Connection = connection;
            try
            {
                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Create table error: {ex.Message}");
            }
        }

        public void SetDefaultData(SqlConnection connection, string DbName)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = $"use {DbName} insert Companies values " +
                $"('Qulix Systems', 'ОАО')," +
                $" ('Epam', 'ООО')," +
                $" ('ItechArt', 'ЗАО')," +
                $" ('IsSoft', 'ООО')," +
                $" ('БМЗ', 'ОАО')";
            command.Connection = connection;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Insert error: {ex.Message}");
            }
        }

        public void InsertData(SqlConnection connection, string DbName)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = $"use {DbName} insert Companies values" +
                $" ('{CompanyName}', '{CompanyForm}')";
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void EditData(SqlConnection connection, string DbName)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = $"use {DbName} update Companies set CompanyName='{CompanyName}', CompanyForm='{CompanyForm}' " +
                $"where Id={Id}";
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteData(SqlConnection connection, string DbName)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = $"use {DbName} delete Companies where Id = {Id}";
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
