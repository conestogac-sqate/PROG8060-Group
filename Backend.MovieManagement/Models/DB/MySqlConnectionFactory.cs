using MySql.Data.MySqlClient;
using PROG8060_Group.App_Start;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PROG8060_Group.Models.DB
{
    public class MySqlConnectionFactory : IDbConnectionFactory
    {
        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(MovieConfig.DBConnectionUrl);
        }

        public IDataAdapter CreateDataAdapter(IDbCommand command)
        {
            return new MySqlDataAdapter((MySqlCommand)command);
        }
    }
}