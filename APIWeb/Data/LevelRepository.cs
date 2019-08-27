using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIWeb.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace APIWeb.Data
{
    public class LevelRepository
    {
        private readonly string _connectionString;



        public LevelRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BaseContext");
        }

         private Level MapToLevel(SqlDataReader reader)
         {
            return new Level()
            {
                 levelId = (long)reader["levelId"],
                 levelName = reader["levelName"].ToString()

             };
             
         }

        public async Task<List<Level>> GetAll()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertUpdateDelete_Level", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Query", 4));
                    var response = new List<Level>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToLevel(reader));
                        }
                    }

                    return response;
                }
            }
        }

        public async Task<Level> GetById(long levelId)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertUpdateDelete_Level", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@levelId", levelId));
                    cmd.Parameters.Add(new SqlParameter("@Query", 5));
                    Level response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToLevel(reader);
                        }
                    }

                    return response;
                }
            }
        }

        public async Task Insert(Level level)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertUpdateDelete_Level", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@levelName", level.levelName));
                    cmd.Parameters.Add(new SqlParameter("@Query", 1));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task Update(long levelId,Level level)
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

        public async Task DeleteById(long levelId)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertUpdateDelete_Level", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@levelId", levelId));
                    cmd.Parameters.Add(new SqlParameter("@Query", 3));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
    }
}
