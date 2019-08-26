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
    public class AssignmentsRepository
    {
        private readonly string _connectionString;
        


        public AssignmentsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BaseContext");
        }

        /*public async Task<List<Assignments>> GetAll()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllAssignments", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Assignments>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToAssignments(reader));
                        }
                    }

                    return response;
                }
            }
        }*/

        private object MapToAssignments(SqlDataReader reader) 
        {
            var Model = new 
            {
                hardwareID = (long)reader["hardwareID"],
                hardwareName = reader["hardwareName"].ToString(),
                SoftwareID = (long)reader["softwareID"],
                softwareName = reader["softwareName"].ToString()

               
            };
            return Model;
        }

        public async Task<object> GetById(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SelectHS", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@UserID", Id));
                    var response = new List<object>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToAssignments(reader));
                        }
                    }

                    return response;
                }
            }
        }

        public async Task Insert(Assignments assignments)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertUHS", sql))
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
    }
}
