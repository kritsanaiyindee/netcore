using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using MySqlConnector;

namespace netcoreapi
{
    public class ROCase
    {

            public int id { get; set; }
            public string diffgrid { get; set; }
            public string msdatarowOrder { get; set; }
            public string OUT_OFFCDE { get; set; }
            public string OUT_CMPCDE { get; set; }
            public string OUT_ROCODE { get; set; }
            public string OUT_CUST_DATE { get; set; }
            public string OUT_RO_STATUS { get; set; }
            public string OUT_RODATE { get; set; }
            public string OUT_ROTIME { get; set; }
            public string OUT_WARRANTY_DATE { get; set; }
            public string OUT_EXPIRY_DATE { get; set; }
            public string OUT_LICENSE { get; set; }
            public string OUT_PRDCDE { get; set; }
            public string OUT_CHASNO { get; set; }
            public string OUT_ENGNO { get; set; }
            public string OUT_MODEL { get; set; }
            public string OUT_KILO_LAST { get; set; }
            public string OUT_LAST_DATE { get; set; }
            public string OUT_IDNO { get; set; }
            public string OUT_CUSNAME { get; set; }
            public string OUT_MOBILE { get; set; }
            public string OUT_ADDRESS { get; set; }
            public string OUT_PROVINCE { get; set; }
            public string OUT_ZIPCODE { get; set; }
            public string OUT_CUSTYPE { get; set; }
            public string A_CODE { get; set; }
            public string B_CODE { get; set; }
            public string C_CODE { get; set; }
            public string CREATED_BY { get; set; }
            public string CREATED_ON { get; set; }
            public string MODIFIED_ON { get; set; }
            public string MODIFIED_BY { get; set; }
            public string STATUS_CODE { get; set; }
            internal AppDb Db { get; set; }

        public ROCase()
        {
        }

        internal ROCase(AppDb db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `BlogPost` (`Title`, `Content`) VALUES (@title, @content);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();

            id = 0;//(int) cmd.l;
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            //cmd.CommandText = @"UPDATE `BlogPost` SET `Title` = @title, `Content` = @content WHERE `Id` = @id;";
            //BindParams(cmd);
            //BindId(cmd);
            //await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            //using var cmd = Db.Connection.CreateCommand();
            //cmd.CommandText = @"DELETE FROM `BlogPost` WHERE `Id` = @id;";
            //BindId(cmd);
            //await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(SqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
        }

        private void BindParams(SqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@title",
                DbType = DbType.String,
              //  Value = Title,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@content",
                DbType = DbType.String,
            //    Value = Content,
            });
        }
    }
}