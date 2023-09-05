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
    public class ReservationRepository : IReservationRepository
    {
        private readonly string connStr = Environment.GetEnvironmentVariable("connStr", EnvironmentVariableTarget.User);

        public async Task<PageList<Reservation>> GetAllAsync(Sorting sorting, Paging paging, ReservationFilter reservationFilter)
        {
            var connection = new NpgsqlConnection(connStr);
            var command = new NpgsqlCommand();
            command.Connection = connection;

            StringBuilder query = new StringBuilder("SELECT *, COUNT(*) OVER() as TotalCount FROM \"Reservation\" INNER JOIN \"Court\" ON \"Reservation\".\"CourtId\" = \"Court\".\"Id\" INNER JOIN \"Member\" ON \"Reservation\".\"MemberId\" = \"Member\".\"Id\" WHERE \"Reservation\".\"IsActive\" = @IsActive ");
            command.Parameters.AddWithValue("@IsActive", reservationFilter.IsActive);

            if (reservationFilter.Time != null)
            {
                query.Append("AND CAST(\"Time\" AS DATE) = CAST(@Time AS DATE) ");
                command.Parameters.AddWithValue("@Time", reservationFilter.Time);
            }
            if (reservationFilter.CourtId != null)
            {
                query.Append("AND \"CourtId\" = @CourtId ");
                command.Parameters.AddWithValue("@CourtId", reservationFilter.CourtId);
            }
            if (reservationFilter.MemberId != null)
            {
                query.Append("AND \"MemberId\" = @MemberId ");
                command.Parameters.AddWithValue("@MemberId", reservationFilter.MemberId);
            }

            string orderBy = sorting.OrderBy ?? "\"Reservation\".\"Id\"";
            query.Append($" ORDER BY {orderBy} {sorting.SortOrder}");
            query.Append(" LIMIT @pageSize OFFSET @offset");
            command.Parameters.AddWithValue("@pageSize", paging.PageSize);
            command.Parameters.AddWithValue("@offset", paging.PageNumber == 0 ? 0 : (paging.PageNumber - 1) * paging.PageSize);
            command.CommandText = query.ToString();

            List<Reservation> reservations = new List<Reservation>();
            int totalCount = 0;
            using (connection)
            {
                connection.Open();
                var reader = await command.ExecuteReaderAsync();
                while (reader.Read() && reader.HasRows)
                {
                    Reservation reservation = new Reservation(
                        (Guid)reader["Id"],
                        (DateTime)reader["Time"],
                        (Guid)reader["CourtId"],
                        (Guid)reader["MemberId"],
                        new Court(
                            (Guid)reader["CourtId"],
                            (string)reader["Name"]
                            ),
                        new Member(
                            (Guid)reader["MemberId"],
                            (string)reader["FirstName"],
                            (string)reader["LastName"],
                            (DateTime)reader["DoB"]
                            )
                        );
                    reservations.Add(reservation);
                    totalCount = Convert.ToInt32(reader["TotalCount"]);
                }
            }
            return new PageList<Reservation>(reservations, totalCount);
        }

        public async Task<Reservation> GetByIdAsync(Guid id)
        {
            var connection = new NpgsqlConnection(connStr);
            var command = new NpgsqlCommand("SELECT * FROM \"Reservation\" INNER JOIN \"Court\" ON \"Reservation\".\"CourtId\" = \"Court\".\"Id\" INNER JOIN \"Member\" ON \"Reservation\".\"MemberId\" = \"Member\".\"Id\" WHERE \"Reservation\".\"Id\"= @Id AND \"Reservation\".\"IsActive\" = TRUE", connection);
            command.Parameters.AddWithValue("@Id", id);

            using (connection)
            {
                connection.Open();
                var reader = await command.ExecuteReaderAsync();

                if (reader.Read())
                {
                    Reservation reservation = new Reservation(
                        (Guid)reader["Id"],
                        (DateTime)reader["Time"],
                        (Guid)reader["CourtId"],
                        (Guid)reader["MemberId"],
                        new Court(
                            (Guid)reader["CourtId"],
                            (string)reader["Name"]
                            ),
                        new Member(
                            (Guid)reader["MemberId"],
                            (string)reader["FirstName"],
                            (string)reader["LastName"],
                            (DateTime)reader["DoB"]
                            )
                        );
                    return reservation;
                }
                return null;
            }
        }

        public async Task<int> CreateAsync(Reservation reservationToCreate)
        {
            var connection = new NpgsqlConnection(connStr);
            var command = new NpgsqlCommand("INSERT INTO \"Reservation\" (\"Id\", \"Time\", \"CourtId\", \"MemberId\") VALUES (@Id, @Time, @CourtId, @MemberId)", connection);
            command.Parameters.AddWithValue("@Id", reservationToCreate.Id);
            command.Parameters.AddWithValue("@Time", reservationToCreate.Time);
            command.Parameters.AddWithValue("@CourtId", reservationToCreate.CourtId);
            command.Parameters.AddWithValue("@MemberId", reservationToCreate.MemberId);

            using (connection)
            {
                connection.Open();
                int affectedRows = await command.ExecuteNonQueryAsync();

                return affectedRows;
            }
        }

        public async Task<int> UpdateAsync(Reservation reservationToUpdate)
        {
            var connection = new NpgsqlConnection(connStr);
            var command = new NpgsqlCommand("UPDATE \"Reservation\" SET \"Time\" = @Time, \"CourtId\" = @CourtId, \"MemberId\" = @MemberId  WHERE \"Id\" = @Id", connection);
            command.Parameters.AddWithValue("@Id", reservationToUpdate.Id);
            command.Parameters.AddWithValue("@Time", reservationToUpdate.Time);
            command.Parameters.AddWithValue("@CourtId", reservationToUpdate.CourtId);
            command.Parameters.AddWithValue("@MemberId", reservationToUpdate.MemberId);

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
            var command = new NpgsqlCommand("UPDATE \"Reservation\" SET \"IsActive\" = NOT \"IsActive\" WHERE \"Id\" = @Id", connection);
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
