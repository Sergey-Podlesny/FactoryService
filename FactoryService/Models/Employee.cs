using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace FactoryService.Models
{
    public class Employee : ITable
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Patronymic { get; set; }
        public DateTime EmploymentDate { get; set; }
        public string Position { get; set; }
        public string Company { get; set; }
        public bool IsExist(SqlConnection connection, string DbName)
        {
            SqlCommand command = new SqlCommand();
            bool isExist = false;
            command.CommandText = $"use {DbName} select count(table_name) " +
                $"from INFORMATION_SCHEMA.tables " +
                $"where table_schema = 'dbo' " +
                $"and table_name = 'Employees'";
            command.Connection = connection;
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                if ((int)reader.GetValue(0) > 0)
                {
                    isExist = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return isExist;
        }
        public void CreateTable(SqlConnection connection, string DbName)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = $"use {DbName} create table Employees (" +
                "Id int primary key identity, " +
                " Surname nvarchar(50) not null, " +
                " Firstname nvarchar(50) not null," +
                " Patronymic nvarchar(50) not null," +
                " EmploymentDate date not null," +
                " Position nvarchar(50) not null," +
                " CompanyId int not null references Companies (Id) on delete cascade)";
            command.Connection = connection;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Create table error: {ex.Message}");
            }
        }

        public void SetDefaultData(SqlConnection connection, string DbName)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = $"use {DbName} insert Employees values" +
                $" ('Кузков', 'Илья', 'Викторович', '2020-12-12', 'Разработчик', 4)," +
                $" ('Северин', 'Павел', 'Генадьевич', '2021-04-13', 'Тестировщик', '3')," +
                $" ('Кривой', 'Никита', 'Владимирович', '2021-09-01', 'Менеджер', '2')," +
                $" ('Прямой', 'Кот', 'Иванович', '2019-09-02', 'Бизнес-аналитик', '1')," +
                $" ('Соков', 'Ярослав', 'Сергеевич', '2016-08-17', 'Разработчик', '5')," +
                $" ('Русина', 'Мария', 'Сергеевна', '2017-06-29', 'Бизнес-аналитик', '4')," +
                $" ('Карпова', 'Виктория', 'Юрьевна', '2010-01-19', 'Тестировщик', '3')," +
                $" ('Хиневич', 'Лена', 'Андреевна', '2018-11-18', 'Разработчик', '1')";
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
            command.CommandText = $"use {DbName} insert Employees values" +
                $" ('{Surname}', '{Firstname}', '{Patronymic}', '{EmploymentDate.Year}-{EmploymentDate.Month}-{EmploymentDate.Day}', '{Position}', " +
                $"(select Id from Companies where CompanyName = '{Company}'))";
            try
            {
                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void EditData(SqlConnection connection, string DbName)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = $"use {DbName} update Employees set Surname='{Surname}', " +
                $"Firstname='{Firstname}', " +
                $"Patronymic='{Patronymic}', " +
                $"EmploymentDate='{EmploymentDate.Year}-{EmploymentDate.Month}-{EmploymentDate.Day}', " +
                $"Position='{Position}', " +
                $"CompanyId=(select Id from Companies where CompanyName='{Company}') " +
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
            command.CommandText = $"use {DbName} delete Employees where Id = {Id}";
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
