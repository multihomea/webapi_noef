using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ProductManager
{
    public interface ISQLDataBase
    {
        Task<Guid> ExecuteNonQueryAsync(string procedureName, List<DbParameter> parameters, CommandType commandType = CommandType.StoredProcedure);
        DataSet ExecuteQuery(string procedureName, List<DbParameter> parameters, CommandType commandType = CommandType.StoredProcedure);
        DbCommand GetCommand(DbConnection connection, string commandText, CommandType commandType);
        SqlConnection GetConnection();
        SqlParameter GetParameter(string parameter, object value);
        SqlParameter GetParameterOut(string parameter, SqlDbType type, object value = null, ParameterDirection parameterDirection = ParameterDirection.InputOutput);
    } 
}