using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

#nullable disable

namespace netcoreapi.Models
{
    public partial class RoCase
    {
        public string Id { get; set; }
        public string Diffgrid { get; set; }
        public string MsdatarowOrder { get; set; }
        public string OutOffcde { get; set; }
        public string OutCmpcde { get; set; }
        public string OutRocode { get; set; }
        public string OutCustDate { get; set; }
        public string OutRoStatus { get; set; }
        public string OutRodate { get; set; }
        public string OutRotime { get; set; }
        public string OutWarrantyDate { get; set; }
        public string OutExpiryDate { get; set; }
        public string OutLicense { get; set; }
        public string OutPrdcde { get; set; }
        public string OutChasno { get; set; }
        public string OutEngno { get; set; }
        public string OutModel { get; set; }
        public string OutKiloLast { get; set; }
        public string OutLastDate { get; set; }
        public string OutIdno { get; set; }
        public string OutCusname { get; set; }
        public string OutMobile { get; set; }
        public string OutAddress { get; set; }
        public string OutProvince { get; set; }
        public string OutZipcode { get; set; }
        public string OutCustype { get; set; }
        public string ACode { get; set; }
        public string BCode { get; set; }
        public string CCode { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string StatusCode { get; set; }


        public string LevelofProblem { get; set; }
        public string CaseTitle { get; set; }
        public string CaseType { get; set; }
        public string CaseSubject { get; set; }
        public string CaseDescription { get; set; }

        internal AppDb Db { get; set; }

        public RoCase()
        {
        }

        internal RoCase(AppDb db)
        {
            Db = db;
        }


        public async Task<List<RoCase>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `Title`, `Content` FROM `BlogPost` ORDER BY `Id` DESC LIMIT 10;";
            return await ROReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<RoCase>> ROReadAllAsync(DbDataReader reader)
        {
            var posts = new List<RoCase>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new RoCase(Db)
                    {
                        Id = reader.GetString(0),
                        
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }

        public async Task InsertAsync()
        {
            int returnValue = -1;
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO ro_case (diffgrid, msdatarowOrder,[OUT_OFFCDE]
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
      ,[STATUS_CODE],LevelofProblem,CaseTitle,CaseType,CaseSubject,CaseDescription) OUTPUT Inserted.ID VALUES        (@diffgrid, @msdatarowOrder,@OutOffcde
      ,@OutCmpcde
      ,@OutRocode
      ,@OutCustDate
      ,@OutRoStatus
      ,@OutRodate
    ,@OutRotime
    ,@OutWarrantyDate
    ,@OutExpiryDate
    ,@OutLicense
    ,@OutPrdcde
    ,@OutChasno
    ,@OutEngno
    ,@OutModel
    ,@OutKiloLast
    ,@OutLastDate
    ,@OutIdno
    ,@OutCusname
    ,@OutMobile
    ,@OutAddress
    ,@OutProvince
    ,@OutZipcode
    ,@OutCustype
    ,@ACode
    ,@BCode
    ,@CCode
    ,@CreatedBy
    ,getdate()
    ,getdate()
    ,@ModifiedBy
    ,@StatusCode,@LevelofProblem,@CaseTitle,@CaseType,@CaseSubject,@CaseDescription);";
            BindParams(cmd);
             //cmd.ExecuteScalarAsync();
            object returnObj = await cmd.ExecuteScalarAsync();
            if (returnObj != null)
            {
                int.TryParse(returnObj.ToString(), out returnValue);
            }
            this.Id = returnValue+"";
        }


        private void BindParams(SqlCommand cmd)
        {
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@Diffgrid",
                DbType = DbType.String,
                Value = Diffgrid,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@MsdatarowOrder",
                DbType = DbType.String,
                Value = MsdatarowOrder,
            });
            
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@OutOffcde",
                DbType = DbType.String,
                Value = OutOffcde,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@OutCmpcde",
                DbType = DbType.String,
                Value = OutCmpcde,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@OutRocode",
                DbType = DbType.String,
                Value = OutRocode,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@OutCustDate",
                DbType = DbType.String,
                Value = OutCustDate,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@OutRoStatus",
                DbType = DbType.String,
                Value = OutRoStatus,
            });

            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@OutRodate",
                DbType = DbType.String,
                Value = OutRodate,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@OutRotime",
                DbType = DbType.String,
                Value = OutRotime,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@OutWarrantyDate",
                DbType = DbType.String,
                Value = OutWarrantyDate,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@OutExpiryDate",
                DbType = DbType.String,
                Value = OutExpiryDate,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@OutLicense",
                DbType = DbType.String,
                Value = OutLicense,
            });



            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@OutPrdcde",
                DbType = DbType.String,
                Value = OutPrdcde,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@OutChasno",
                DbType = DbType.String,
                Value = OutChasno,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@OutEngno",
                DbType = DbType.String,
                Value = OutEngno,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@OutModel",
                DbType = DbType.String,
                Value = OutModel,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@OutKiloLast",
                DbType = DbType.String,
                Value = OutKiloLast,
            });

            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@OutLastDate",
                DbType = DbType.String,
                Value = OutLastDate,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@OutIdno",
                DbType = DbType.String,
                Value = OutIdno,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@OutCusname",
                DbType = DbType.String,
                Value = OutCusname,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@OutMobile",
                DbType = DbType.String,
                Value = OutMobile,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@OutAddress",
                DbType = DbType.String,
                Value = OutAddress,
            });



            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@OutProvince",
                DbType = DbType.String,
                Value = OutProvince,
            });

            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@OutZipcode",
                DbType = DbType.String,
                Value = OutZipcode,
            });

            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@OutCustype",
                DbType = DbType.String,
                Value = OutCustype,
            });

            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@ACode",
                DbType = DbType.String,
                Value = ACode,
            });

            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@BCode",
                DbType = DbType.String,
                Value = BCode,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@CCode",
                DbType = DbType.String,
                Value = CCode,
            });

            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@CreatedBy",
                DbType = DbType.String,
                Value = CreatedBy,
            });
            //cmd.Parameters.Add(new SqlParameter
            //{
            //    ParameterName = "@CreatedOn",
            //    DbType = DbType.DateTime,
            //    Value = CreatedOn,
            //});
            //cmd.Parameters.Add(new SqlParameter
            //{
            //    ParameterName = "@ModifiedOn",
            //    DbType = DbType.DateTime,
            //    Value = ModifiedOn,
            //});

            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@ModifiedBy",
                DbType = DbType.String,
                Value = ModifiedBy,
            });

            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@StatusCode",
                DbType = DbType.String,
                Value = StatusCode,
            });

            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@LevelofProblem",
                DbType = DbType.String,
                Value = LevelofProblem,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@CaseTitle",
                DbType = DbType.String,
                Value = CaseTitle,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@CaseType",
                DbType = DbType.String,
                Value = CaseType,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@CaseSubject",
                DbType = DbType.String,
                Value = CaseSubject,
            });
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@CaseDescription",
                DbType = DbType.String,
                Value = CaseDescription,
            });



        }
    }
}
