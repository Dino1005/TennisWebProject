using Backend.Common;
using Backend.Model;
using Backend.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public class CourtRepository : ICourtRepository
    {
        private readonly string connStr = Environment.GetEnvironmentVariable("connStr", EnvironmentVariableTarget.User);

        public async Task<PageList<Court>> GetAllAsync(Sorting sorting, Paging paging, CourtFilter courtFilter)
        {
            var connection = new NpgsqlConnection(connStr);
            var command = new NpgsqlCommand();
            command.Connection = connection;

            StringBuilder query = new StringBuilder("SELECT *, COUNT(*) OVER() as TotalCount FROM \"Court\" WHERE \"IsActive\" = @IsActive");
            command.Parameters.AddWithValue("@IsActive", courtFilter.IsActive);

            if (!string.IsNullOrEmpty(courtFilter.Name))
            {
                query.Append(" AND LOWER(\"Name\") LIKE @Name");
                command.Parameters.AddWithValue("@Name", "%" + courtFilter.Name.ToLower() + "%");
            }

            string orderBy = sorting.OrderBy ?? "\"Court\".\"Id\"";
            query.Append($" ORDER BY {orderBy} {sorting.SortOrder}");
            query.Append(" LIMIT @pageSize OFFSET @offset");
            command.Parameters.AddWithValue("@pageSize", paging.PageSize);
            command.Parameters.AddWithValue("@offset", paging.PageNumber == 0 ? 0 : (paging.PageNumber - 1) * paging.PageSize);
            command.CommandText = query.ToString();

            List<Court> courts = new List<Court>();
            int totalCount = 0;
            using (connection)
            {
                connection.Open();
                var reader = await command.ExecuteReaderAsync();
                while (reader.Read() && reader.HasRows)
                {
                    Court court = new Court(
                        (Guid)reader["Id"],
                        (string)reader["Name"]
                        );
                    courts.Add(court);
                    totalCount = Convert.ToInt32(reader["TotalCount"]);
                }
            }
            return new PageList<Court>(courts, totalCount);
        }

        public async Task<Court> GetByIdAsync(Guid id)
        {
            var connection = new NpgsqlConnection(connStr);
            var command = new NpgsqlCommand("SELECT * FROM \"Court\" WHERE \"Id\"= @Id AND \"IsActive\" = TRUE", connection);
            command.Parameters.AddWithValue("@Id", id);

            using (connection)
            {
                connection.Open();
                var reader = await command.ExecuteReaderAsync();

                if (reader.Read())
                {
                    Court court = new Court(
                        (Guid)reader["Id"],
                        (string)reader["Name"]
                        );
                    return court;
                }
                return null;
            }
        }

        public async Task<int> CreateAsync(Court courtToCreate)
        {
            var connection = new NpgsqlConnection(connStr);
            var command = new NpgsqlCommand("INSERT INTO \"Court\" (\"Id\", \"Name\") VALUES (@Id, @Name)", connection);
            command.Parameters.AddWithValue("@Id", courtToCreate.Id);
            command.Parameters.AddWithValue("@Name", courtToCreate.Name);

            using (connection)
            {
                connection.Open();
                int affectedRows = await command.ExecuteNonQueryAsync();

                return affectedRows;
            }
        }

        public async Task<int> UpdateAsync(Court courtToUpdate)
        {
            var connection = new NpgsqlConnection(connStr);
            var command = new NpgsqlCommand("UPDATE \"Court\" SET \"Name\" = @Name WHERE \"Id\" = @Id", connection);
            command.Parameters.AddWithValue("@Id", courtToUpdate.Id);
            command.Parameters.AddWithValue("@Name", courtToUpdate.Name);

            using (connection)
            {
                connection.Open();
                int affectedRows = await command.ExecuteNonQueryAsync();

                return affectedRows;
            }
        }

        public async Task<int> ToggleActivateAsync(Guid id)
        {
            var connection = new NpgsqlConnection(connStr);
            var command = new NpgsqlCommand("UPDATE \"Court\" SET \"IsActive\" = NOT \"IsActive\" WHERE \"Id\" = @Id", connection);
            command.Parameters.AddWithValue("@id", id);

            using (connection)
            {
                connection.Open();
                int affectedRows = await command.ExecuteNonQueryAsync();

                return affectedRows;
            }
        }
    }
}
