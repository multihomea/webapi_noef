using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ProductManager
{
    public class DataBaseRespository : IProductRepository
    {
        ISQLDataBase _database;

        public DataBaseRespository()
        {
        }

        public DataBaseRespository(ISQLDataBase dataBase)
        {
            _database = dataBase;
        }

        public DataBaseRespository(IConfiguration configuration, ISQLDataBase database)
        {
            _database = new SQLServerDataBase(configuration.GetConnectionString("ProductManagerDB"));
        }

        public async Task<Guid> Add(Product product)
        {
            List<DbParameter> dbParameters = new List<DbParameter>();
            dbParameters.Add(new SqlParameter("Name", product.Name));
            dbParameters.Add(new SqlParameter("StartDate", product.StartDate));
            dbParameters.Add(new SqlParameter("EndDate", product.EndDate));

            return await _database.ExecuteNonQueryAsync("AddProduct", dbParameters, CommandType.StoredProcedure);
        }



        public IEnumerable<Product> GetProducts()
        {

            DataSet dataSet =  _database.ExecuteQuery("GetProducts", null, CommandType.StoredProcedure);

            //Do something with the result

            IEnumerable<Product> result = dataSet.Tables[0].AsEnumerable()
                            .Select(dataRow => new Product
                            {
                                ProductId = dataRow.Field<Guid>("ProductId"),
                                Name = dataRow.Field<string>("Name"),
                                StartDate = dataRow.Field<DateTime>("StartDate"),
                                EndDate = dataRow.Field<DateTime>("EndDate")
                            }).ToList();

            return result;
        }

    }
}