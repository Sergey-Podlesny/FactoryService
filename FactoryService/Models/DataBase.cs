using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using FactoryService.Parser;

namespace FactoryService.Models
{
    // класс, через который происходит взаимодействие с БД
    public class DataBase
    {
        public string ServerName { get; set; }
        public string DbName { get; set; }

        // проверка существования БД
        public bool IsExist()
        {
            bool isExist = true;
            using (SqlConnection connection = new SqlConnection($"Server={ServerName};Database={DbName};Trusted_Connection=True;"))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    isExist = false;
                }
            }
            return isExist;
        }

        public void CreateDb()
        {
            string sqlExpression = $"CREATE DATABASE {DbName}";
            using (SqlConnection connection = new SqlConnection($"Server={ServerName};Database=master;Trusted_Connection=True;"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }

        public void CheckTable(ITable table)
        {
            using (SqlConnection connection = new SqlConnection($"Server={ServerName};Database={DbName};Trusted_Connection=True;"))
            {
                connection.Open();
                if (!table.IsExist(connection, DbName)) // если таблица не существует
                {
                    table.CreateTable(connection, DbName); // то создаем ее
                    table.SetDefaultData(connection, DbName); // и заполняем базовыми значениями
                }
            }
        }

        public void ParseData<T>(IParser<T> parser)
        {
            using (SqlConnection connection = new SqlConnection($"Server={ServerName};Database={DbName};Trusted_Connection=True;"))
            {
                connection.Open();
                parser.Parse(connection, DbName);
            }
        }

        public void InsertData(ITable table)
        {
            using (SqlConnection connection = new SqlConnection($"Server={ServerName};Database={DbName};Trusted_Connection=True;"))
            {
                connection.Open();
                table.InsertData(connection, DbName);
            }
        }

        public void EditData(ITable table)
        {
            using (SqlConnection connection = new SqlConnection($"Server={ServerName};Database={DbName};Trusted_Connection=True;"))
            {
                connection.Open();
                table.EditData(connection, DbName);
            }
        }

        public void DeleteData(ITable table)
        {
            using (SqlConnection connection = new SqlConnection($"Server={ServerName};Database={DbName};Trusted_Connection=True;"))
            {
                connection.Open();
                table.DeleteData(connection, DbName);
            }
        }
    }
}
