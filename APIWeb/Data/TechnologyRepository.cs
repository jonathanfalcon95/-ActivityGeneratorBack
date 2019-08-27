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
    public class TechnologyRepository
    {
        private readonly string _connectionString;



        public TechnologyRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BaseContext");
        }
        public async Task<List<Technology>> GetAll()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertUpdateDelete_Technology", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Query", 4);
                    var response = new List<Technology>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToTechnology(reader));
                        }
                    }

                    return response;
                }
            }
        }

        private Technology MapToTechnology(SqlDataReader reader)
        {
            return new Technology()
            {
                techId = (long)reader["techId"],
                techName = reader["techName"].ToString(),
            };
        }
        public async Task<Technology> GetById(long Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertUpdateDelete_Technology", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@techId", Id));
                    cmd.Parameters.Add(new SqlParameter("@Query", 5));
                    Technology response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToTechnology(reader);
                        }
                    }

                    return response;
                }
            }
        }

        //METODO POST 
        public async Task Insert(Technology technology)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertUpdateDelete_Technology", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@techName", technology.techName));
                    cmd.Parameters.AddWithValue("@Query", 1);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
        public async Task DeleteById(long Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertUpdateDelete_Technology", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@techId", Id));
                    cmd.Parameters.AddWithValue("@Query", 3);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
        public async Task Update(long Id, Technology technology)
        {
            

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertUpdateDelete_Technology", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@techId", Id));
                    cmd.Parameters.Add(new SqlParameter("@techName", technology.techName));
                    cmd.Parameters.AddWithValue("@Query", 2);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }



    }
}
