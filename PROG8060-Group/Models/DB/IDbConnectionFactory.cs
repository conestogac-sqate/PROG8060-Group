using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PROG8060_Group.Models.DB
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();

        IDataAdapter CreateDataAdapter(IDbCommand command);
    }
}