using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using ProductManager;

namespace ProductManagerTest
{


    public class MockDataBase : ISQLDataBase
    {
        protected string ConnectionString { get; set; }
        public List<Product> Products { get; set; }
        public MockDataBase()
        {
        }



        public SqlConnection GetConnection()
        {
            return null;
        }


        public DbCommand GetCommand(DbConnection connection, string commandText, CommandType commandType)
        {
            return null;
        }

        public SqlParameter GetParameter(string parameter, object value)
        {
            return null;
        }

        public SqlParameter GetParameterOut(string parameter, SqlDbType type, object value = null, ParameterDirection parameterDirection = ParameterDirection.InputOutput)
        {
            return null;
        }

        public async Task<Guid> ExecuteNonQueryAsync(string procedureName, List<DbParameter> parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            Guid returnValue = new Guid();

            Products.Add(new Product
            {
                ProductId = new Guid(),
                Name = parameters[0].Value as String,
                StartDate = (DateTime)parameters[1].Value,
                EndDate = (DateTime)parameters[2].Value,

            });

            return returnValue;
        }

        public DataSet ExecuteQuery(string procedureName, List<DbParameter> parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            DataSet ds;

            DataTable dt = new DataTable("Products");

            DataColumn col1 = new DataColumn("ProductId", typeof(Guid));
            DataColumn col2 = new DataColumn("Name", typeof(string));
            DataColumn col3 = new DataColumn("StartDate", typeof(DateTime));
            DataColumn col4 = new DataColumn("EndDate", typeof(DateTime));

            dt.Columns.Add(col1);
            dt.Columns.Add(col2);
            dt.Columns.Add(col3);
            dt.Columns.Add(col4);
            for (int i = 0; i < Products.Count; i++)
            {
                DataRow drow = dt.NewRow();
                dt.Rows.Add(drow);
                dt.Rows[i][col1] = Products[i].ProductId.ToString();// i.ToString();
                dt.Rows[i][col2] = Products[i].Name.ToString();
                dt.Rows[i][col3] = Products[i].StartDate.ToString();
                dt.Rows[i][col4] = Products[i].EndDate.ToString();
            }
            ds = new DataSet();
            ds.Tables.Add(dt);


            return ds;
        }


    }
}