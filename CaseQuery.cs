using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using netcoreapi;
using netcoreapi.Models;

namespace netcoreapinet 
{
    public class CasetQuery
    {
        public AppDb Db { get; }

        public CasetQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<RoCase> ValidateRunumber(string  ro_number)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM vRoCase WHERE OUT_ROCODE = @OUT_ROCODE ";
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@OUT_ROCODE",
                DbType = DbType.String,
                Value = ro_number,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }
        public async Task<RoCase> getRO(string ROID)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM vRoCase WHERE id = @ROID ";
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@ROID",
                DbType = DbType.String,
                Value = ROID,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }
        public async Task<List<RoCase>> ListOwnerCase(string dealer,string createdby)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM vrocase WHERE OUT_OFFCDE = @OUT_OFFCDE  and CREATED_BY=@CREATED_BY ";
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@OUT_OFFCDE",
                DbType = DbType.String,
                Value = dealer,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@CREATED_BY",
                DbType = DbType.String,
                Value = createdby,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result : null;
        }
        public async Task<List<RoCase>> ListOwnerCaseBystatus(string dealer, string createdby, int statuscode)
        {
            string statusGroup = statuscode < 1 ? "(1,2,3,4)" : "(5,6)";
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM vrocase WHERE OUT_OFFCDE = @OUT_OFFCDE  and STATUS_CODE in "+ statusGroup + "  and CREATED_BY=@CREATED_BY ";
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@OUT_OFFCDE",
                DbType = DbType.String,
                Value = dealer,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@CREATED_BY",
                DbType = DbType.String,
                Value = createdby,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result : null;
        }
        public async Task<List<RoCase>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT  top 100 [id]
      ,CASEID
      ,DEALER
      ,[OUT_OFFCDE]
      ,[OUT_CMPCDE]
      ,[OUT_ROCODE]
      ,[OUT_CUST_DATE]
      ,[OUT_RO_STATUS]
      ,[OUT_RODATE]
      ,[OUT_ROTIME]
      ,[OUT_WARRANTY_DATE]
      ,[OUT_EXPIRY_DATE]
      ,[OUT_LICENSE]
      ,[OUT_PRDCDE]
      ,[OUT_CHASNO]
      ,[OUT_ENGNO]
      ,[OUT_MODEL]
      ,[OUT_KILO_LAST]
      ,[OUT_LAST_DATE]
      ,[OUT_IDNO]
      ,[OUT_CUSNAME]
      ,[OUT_MOBILE]
      ,[OUT_ADDRESS]
      ,[OUT_PROVINCE]
      ,[OUT_ZIPCODE]
      ,[OUT_CUSTYPE]
      ,[A_CODE]
      ,[B_CODE]
      ,[C_CODE]
      ,[CREATED_BY]
      ,[CREATED_ON]
      ,[MODIFIED_ON]
      ,[MODIFIED_BY]
      ,[STATUS_CODE],statusCodeText FROM vrocase ORDER BY Id DESC ";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `BlogPost`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        private async Task<List<RoCase>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<RoCase>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                        var post = new RoCase(Db)
                    {
                        Id = reader.GetInt32(0)+"",
                        CaseID = reader.GetString(1),
                        Dealer = reader.GetString(2),
                        OutOffcde = reader.GetString(3),
                        OutCmpcde = reader.GetString(4),
                        OutRocode = reader.GetString(5),
                        OutCustDate = reader.GetString(6),
                        OutRoStatus = reader.GetString(7),
                        OutRodate = reader.GetString(8),
                        OutRotime = reader.GetString(9),
                        OutWarrantyDate = reader.GetString(10),
                        OutExpiryDate = reader.GetString(11),
                        OutLicense = reader.GetString(12),
                        OutPrdcde = reader.GetString(13),
                        OutChasno = reader.GetString(14),
                        OutEngno = reader.GetString(15),
                        OutModel = reader.GetString(16),
                        OutKiloLast = reader.GetString(17),
                        OutLastDate = reader.GetString(18),
                        OutIdno = reader.GetString(19),
                        OutCusname = reader.GetString(20),
                        OutMobile = reader.GetString(21),
                        OutAddress = reader.GetString(22),
                        OutProvince = reader.GetString(23),
                        OutZipcode = reader.GetString(24),

                        OutCustype = reader.GetString(25),
                        ACode = reader.GetString(26),
                        BCode = reader.GetString(27),
                        CCode = reader.GetString(28),
                        CreatedBy = reader.GetString(29),
                        CreatedOn = reader.GetDateTime(30).ToString(),
                        ModifiedOn = reader.GetDateTime(31).ToString(),
                        ModifiedBy = reader.GetString(32),
                        StatusCode = reader.GetString(33),
                        LevelofProblem= reader.GetString(34),
                        CaseTitle= reader.GetString(35),
                        CaseType= reader.GetString(36),
                        CaseSubject= reader.GetString(37),
                        CaseDescription =reader.GetString(38),
                        StatusCodeText = reader.GetString(39),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}