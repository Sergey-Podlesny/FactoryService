using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace FactoryService.Models
{
    // интерфейс нужен для реализации CRUD операций и проверки существования таблиц
    public interface ITable
    {
        bool IsExist(SqlConnection connection, string DbName);
        void CreateTable(SqlConnection connection, string DbName);
        void SetDefaultData(SqlConnection connection, string DbName);
        void InsertData(SqlConnection connection, string DbName);
        void EditData(SqlConnection connection, string DbName);
        void DeleteData(SqlConnection connection, string DbName);

    }
}
