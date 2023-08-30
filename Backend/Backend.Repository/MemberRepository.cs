using Backend.Common;
using Backend.Model;
using Backend.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly string connStr = Environment.GetEnvironmentVariable("connStr", EnvironmentVariableTarget.User);

        public async Task<PageList<Member>> GetAllAsync(Sorting sorting, Paging paging, MemberFilter memberFilter)
        {
            var connection = new NpgsqlConnection(connStr);
            var command = new NpgsqlCommand();
            command.Connection = connection;

            StringBuilder query = new StringBuilder("SELECT *, COUNT(*) OVER() as TotalCount FROM \"Member\" WHERE \"IsActive\" = @IsActive");
            command.Parameters.AddWithValue("@IsActive", memberFilter.IsActive);

            if (!string.IsNullOrEmpty(memberFilter.Name))
            {
                query.Append(" AND (LOWER(\"FirstName\") LIKE @Name OR LOWER(\"LastName\") LIKE @Name)");
                command.Parameters.AddWithValue("@Name", "%" + memberFilter.Name.ToLower() + "%");
            }

            string orderBy = sorting.OrderBy ?? "\"Member\".\"Id\"";
            query.Append($" ORDER BY {orderBy} {sorting.SortOrder}");
            query.Append(" LIMIT @pageSize OFFSET @offset");
            command.Parameters.AddWithValue("@pageSize", paging.PageSize);
            command.Parameters.AddWithValue("@offset", paging.PageNumber == 0 ? 0 : (paging.PageNumber - 1) * paging.PageSize);
            command.CommandText = query.ToString();

            List<Member> members = new List<Member>();
            int totalCount = 0;
            using (connection)
            {
                connection.Open();
                var reader = await command.ExecuteReaderAsync();
                while (reader.Read() && reader.HasRows)
                {
                    Member member = new Member(
                        (Guid)reader["Id"],
                        (string)reader["FirstName"],
                        (string)reader["LastName"],
                        (DateTime)reader["DoB"]
                        );
                    members.Add(member);
                    totalCount = Convert.ToInt32(reader["TotalCount"]);
                }
            }
            return new PageList<Member>(members, totalCount);
        }

        public async Task<Member> GetByIdAsync(Guid id)
        {
            var connection = new NpgsqlConnection(connStr);
            var command = new NpgsqlCommand("SELECT * FROM \"Member\" WHERE \"Id\"= @Id AND \"IsActive\" = TRUE", connection);
            command.Parameters.AddWithValue("@Id", id);

            using (connection)
            {
                connection.Open();
                var reader = await command.ExecuteReaderAsync();

                if (reader.Read())
                {
                    Member member = new Member(
                        (Guid)reader["Id"],
                        (string)reader["FirstName"],
                        (string)reader["LastName"],
                        (DateTime)reader["DoB"]
                        );
                    return member;
                }
                return null;
            }
        }

        public async Task<int> CreateAsync(Member memberToCreate)
        {
            var connection = new NpgsqlConnection(connStr);
            var command = new NpgsqlCommand("INSERT INTO \"Member\" (\"Id\", \"FirstName\", \"LastName\", \"DoB\") VALUES (@Id, @FirstName, @LastName, @DoB)", connection);
            command.Parameters.AddWithValue("@Id", memberToCreate.Id);
            command.Parameters.AddWithValue("@FirstName", memberToCreate.FirstName);
            command.Parameters.AddWithValue("@LastName", memberToCreate.LastName);
            command.Parameters.AddWithValue("@DoB", memberToCreate.DoB);

            using (connection)
            {
                connection.Open();
                int affectedRows = await command.ExecuteNonQueryAsync();

                return affectedRows;
            }
        }

        public async Task<int> UpdateAsync(Member memberToUpdate)
        {
            var connection = new NpgsqlConnection(connStr);
            var command = new NpgsqlCommand("UPDATE \"Member\" SET \"FirstName\" = @FirstName, \"LastName\" = @LastName, \"DoB\" = @DoB WHERE \"Id\" = @Id", connection);
            command.Parameters.AddWithValue("@Id", memberToUpdate.Id);
            command.Parameters.AddWithValue("@FirstName", memberToUpdate.FirstName);
            command.Parameters.AddWithValue("@LastName", memberToUpdate.LastName);
            command.Parameters.AddWithValue("@DoB", memberToUpdate.DoB);

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
            var command = new NpgsqlCommand("UPDATE \"Member\" SET \"IsActive\" = NOT \"IsActive\" WHERE \"Id\" = @Id", connection);
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
