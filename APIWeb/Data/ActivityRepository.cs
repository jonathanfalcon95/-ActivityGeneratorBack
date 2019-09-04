using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using APIWeb.Models;

namespace APIWeb.Data
{
    public class ActivityRepository
    {
        private readonly string _connectionString;



        public ActivityRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BaseContext");
        }
        private object MapToActivities(SqlDataReader reader)
        {
            var Model = new
            {
                activId = (long)reader["activId"],
                subjet = reader["subject"].ToString(),
                description = reader["description"].ToString(),
                levelId = (long)reader["levelId"]


            };
            return Model;
        }
        public async Task<List<object>> GetAll()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("CrudActivities", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Query", 4));
                    var response = new List<object>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToActivities(reader));
                        }
                    }

                    return response;
                }
            }
        }



        public async Task<object> GetById(long Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("CrudActivities", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Query", 5));
                    cmd.Parameters.Add(new SqlParameter("@activId", Id));
                    var response = new List<object>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToActivities(reader));
                        }
                    }

                    return response;
                }
            }
        }

        public async Task Insert(Activity activity)
        {

            Console.WriteLine(activity);

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("CrudActivities1", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Query", 1));
                    cmd.Parameters.Add(new SqlParameter("@subject", activity.subjet));
                    cmd.Parameters.Add(new SqlParameter("@description", activity.description));
                    cmd.Parameters.Add(new SqlParameter("@levelId", activity.levelId));

                    await sql.OpenAsync();

                    //==============================================

                    
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        await reader.ReadAsync();
                        
                            foreach (long item in activity.technologies)
                            {
                                var activTech = new ActivTech()
                                {
                                    activId = (long)reader["activId"],
                                    techId = item
                                };

                                await InsertActivTech(activTech);

                            }
                        
                    }
                    //==============================================

                    //await cmd.ExecuteNonQueryAsync();
                     return;
                    
                }
            }
            //to go through arrangement of technologies inside activity
           
           // return;

            
        }


        public async Task InsertActivTech(ActivTech activTech)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("CrudActivTech", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Query", 1));
                    cmd.Parameters.Add(new SqlParameter("@activId", activTech.activId));
                    cmd.Parameters.Add(new SqlParameter("@techId", activTech.techId));
        

                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    //return;
                }
            }

        }

            public async Task DeleteById(Assignments assignments)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteAss", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@UserID", assignments.UserID));
                    cmd.Parameters.Add(new SqlParameter("@HardwareID", assignments.HardwareID));
                    cmd.Parameters.Add(new SqlParameter("@SoftwareID", assignments.SoftwareID));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }


        public async Task Update(long levelId, Level level)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertUpdateDelete_Level", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@levelId", levelId));
                    cmd.Parameters.Add(new SqlParameter("@levelName", level.levelName));
                    cmd.Parameters.Add(new SqlParameter("@Query", 2));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }


    }
}
