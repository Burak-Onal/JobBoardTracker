using JobBoardTrackerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoardTrackerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobBoards : ControllerBase
    {
        [HttpGet]
        public List<JobBoard> GetAllJobBoards()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = "burakserver.database.windows.net";
            builder.UserID = "BurakOnal";
            builder.Password = "02261988_Bo";
            builder.InitialCatalog = "BurakJobBoardDB";
            string sql = "GetAllJobBoards";

            using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
            {
                var jobBoards = new List<JobBoard>();

                try
                {
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = new SqlCommand(sql, conn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;

                        DataSet ds = new DataSet();
                        da.Fill(ds, "result");

                        DataTable dt = ds.Tables["result"];

                        foreach (DataRow row in dt.Rows)
                        {
                            var jobBoard = new JobBoard();
                            jobBoard.Name = row[0] != DBNull.Value ? (string)row[0] : "";
                            jobBoard.Rating = row[1] != DBNull.Value ? (string)row[1] : "";
                            jobBoard.Root_domain = row[2] != DBNull.Value ? (string)row[2] : "";
                            jobBoard.Logo_file = row[3] != DBNull.Value ? (string)row[3] : "";
                            jobBoard.Description = row[4] != DBNull.Value ? (string)row[4] : "";
                            jobBoards.Add(jobBoard);
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

                return jobBoards;
            }
        }
    }
}
