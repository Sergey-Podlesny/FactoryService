using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactoryService.Parser
{
    // интерфейс нужен для парсинга данныт от SqlReader и выдачи данных
    public interface IParser<T>
    {
        void Parse(SqlConnection connection, string DbName);
        T GetData();
    }
}
