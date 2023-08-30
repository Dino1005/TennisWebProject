using Backend.Common;
using Backend.Model;
using Backend.Repository.Common;
using Npgsql;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Backend.Repository
{
    public class ReservationMemberRepository : IReservationMemberRepository
    {
        private readonly string connStr = Environment.GetEnvironmentVariable("connStr", EnvironmentVariableTarget.User);

        public async Task<PageList<ReservationMember>> GetAllAsync(Sorting sorting, Paging paging, ReservationMemberFilter reservationMemberFilter)
        {
            var connection = new NpgsqlConnection(connStr);
            var command = new NpgsqlCommand();
            command.Connection = connection;

            StringBuilder query = new StringBuilder("SELECT *, COUNT(*) OVER() as TotalCount FROM \"ReservationMember\" INNER JOIN \"Reservation\" ON \"ReservationMember\".\"ReservationId\" = \"Reservation\".\"Id\" INNER JOIN \"Member\" ON \"ReservationMember\".\"MemberId\" = \"Member\".\"Id\" WHERE \"ReservationMember\".\"IsActive\" = @IsActive");
            command.Parameters.AddWithValue("@IsActive", reservationMemberFilter.IsActive);

            if (reservationMemberFilter.ReservationId != null)
            {
                query.Append("AND \"ReservationId\" = @ReservationId ");
                command.Parameters.AddWithValue("@ReservationId", reservationMemberFilter.ReservationId);
            }
            if (reservationMemberFilter.MemberId != null)
            {
                query.Append("AND \"MemberId\" = @MemberId ");
                command.Parameters.AddWithValue("@MemberId", reservationMemberFilter.MemberId);
            }

            string orderBy = sorting.OrderBy ?? "\"ReservationMember\".\"Id\"";
            query.Append($" ORDER BY {orderBy} {sorting.SortOrder}");
            query.Append(" LIMIT @pageSize OFFSET @offset");
            command.Parameters.AddWithValue("@pageSize", paging.PageSize);
            command.Parameters.AddWithValue("@offset", paging.PageNumber == 0 ? 0 : (paging.PageNumber - 1) * paging.PageSize);
            command.CommandText = query.ToString();

            List<ReservationMember> reservationMembers = new List<ReservationMember>();
            int totalCount = 0;
            using (connection)
            {
                connection.Open();
                var reader = await command.ExecuteReaderAsync();
                while (reader.Read() && reader.HasRows)
                {
                    ReservationMember reservationMember = new ReservationMember(
                        (Guid)reader["Id"],
                        (Guid)reader["ReservationId"],
                        (Guid)reader["MemberId"],
                        new Reservation(
                            (Guid)reader["ReservationId"],
                            (DateTime)reader["Time"],
                            (Guid)reader["CourtId"]
                            ),
                        new Member(
                            (Guid)reader["MemberId"],
                            (string)reader["FirstName"],
                            (string)reader["LastName"],
                            (DateTime)reader["DoB"]
                            )
                        );
                    reservationMembers.Add(reservationMember);
                    totalCount = Convert.ToInt32(reader["TotalCount"]);
                }
            }
            return new PageList<ReservationMember>(reservationMembers, totalCount);
        }

        public async Task<ReservationMember> GetByIdAsync(Guid id)
        {
            var connection = new NpgsqlConnection(connStr);
            var command = new NpgsqlCommand("SELECT * FROM \"ReservationMember\" INNER JOIN \"Reservation\" ON \"ReservationMember\".\"ReservationId\" = \"Reservation\".\"Id\" INNER JOIN \"Member\" ON \"ReservationMember\".\"MemberId\" = \"Member\".\"Id\" WHERE \"ReservationMember\".\"Id\"= @Id AND \"ReservationMember\".\"IsActive\" = TRUE", connection);
            command.Parameters.AddWithValue("@Id", id);

            using (connection)
            {
                connection.Open();
                var reader = await command.ExecuteReaderAsync();

                if (reader.Read())
                {
                    ReservationMember reservationMember = new ReservationMember(
                        (Guid)reader["Id"],
                        (Guid)reader["ReservationId"],
                        (Guid)reader["MemberId"],
                        new Reservation(
                            (Guid)reader["ReservationId"],
                            (DateTime)reader["Time"],
                            (Guid)reader["CourtId"]
                            ),
                        new Member(
                            (Guid)reader["MemberId"],
                            (string)reader["FirstName"],
                            (string)reader["LastName"],
                            (DateTime)reader["DoB"]
                            )
                        );
                    return reservationMember;
                }
                return null;
            }
        }

        public async Task<int> CreateAsync(ReservationMember reservationMemberToCreate)
        {
            var connection = new NpgsqlConnection(connStr);
            var command = new NpgsqlCommand("INSERT INTO \"ReservationMember\" (\"Id\", \"ReservationId\", \"MemberId\") VALUES (@Id, @ReservationId, @MemberId)", connection);
            command.Parameters.AddWithValue("@Id", reservationMemberToCreate.Id);
            command.Parameters.AddWithValue("@ReservationId", reservationMemberToCreate.ReservationId);
            command.Parameters.AddWithValue("@MemberId", reservationMemberToCreate.MemberId);

            using (connection)
            {
                connection.Open();
                int affectedRows = await command.ExecuteNonQueryAsync();

                return affectedRows;
            }
        }

        public async Task<int> UpdateAsync(ReservationMember reservationMemberToUpdate)
        {
            var connection = new NpgsqlConnection(connStr);
            var command = new NpgsqlCommand("UPDATE \"ReservationMember\" SET \"ReservationId\" = @ReservationId, \"MemberId\" = @CourMemberIdtId WHERE \"Id\" = @Id", connection);
            command.Parameters.AddWithValue("@Id", reservationMemberToUpdate.Id);
            command.Parameters.AddWithValue("@ReservationId", reservationMemberToUpdate.ReservationId);
            command.Parameters.AddWithValue("@MemberId", reservationMemberToUpdate.MemberId);

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
            var command = new NpgsqlCommand("UPDATE \"ReservationMember\" SET \"IsActive\" = NOT \"IsActive\" WHERE \"Id\" = @Id", connection);
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
