using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using JobBoardTrackerAPI.Models;
using System.Data;

namespace JobBoardTrackerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Jobs : ControllerBase
    {
        [HttpGet]
        public List<JobSourceResolutionModel> GetJobsBySource(string source)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = "burakserver.database.windows.net";
            builder.UserID = "BurakOnal";
            builder.Password = "02261988_Bo";
            builder.InitialCatalog = "BurakJobBoardDB";
            string sql = "GerJobsBySource";

            using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
            {
                var jobs = new List<JobSourceResolutionModel>();

                try
                {
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = new SqlCommand(sql, conn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue(@"source", SqlDbType.NVarChar).Value = source;

                        DataSet ds = new DataSet();
                        da.Fill(ds, "result");

                        DataTable dt = ds.Tables["result"];

                        foreach (DataRow row in dt.Rows)
                        {
                            var job = new JobSourceResolutionModel();
                            job.ID = row[0] != DBNull.Value ? (string)row[0] : "";
                            job.JobTitle = row[1] != DBNull.Value ? (string)row[1] : "";
                            job.CompanyName = row[2] != DBNull.Value ? (string)row[2] : "";
                            job.JobURL = row[3] != DBNull.Value ? (string)row[3] : "";
                            job.Source = row[4] != DBNull.Value ? (string)row[4] : "";
                            jobs.Add(job);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Error: " + ex.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }

                return jobs;
            }
        }
    }
}
