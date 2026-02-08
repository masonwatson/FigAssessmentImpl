using FigAssessmentImpl.Application.Users;
using FigAssessmentImpl.Application.Users.CreateUsers;
using FigAssessmentImpl.Application.Users.GetUsers;
using FigAssessmentImpl.Application.Users.ValidateUsers;
using FigAssessmentImpl.Domain.Users;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FigAssessmentImpl.Infrastructure.Repositories
{
    public sealed class SqlUserRepository: IUserRepository
    {
        private readonly string _connectionString;

        public SqlUserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IReadOnlyList<User>?> GetUsersAsync(GetUserQueryOptions options, CancellationToken ct)
        {
            var users = new List<User>();
            var whereSql = new StringBuilder("WHERE 1=1");
            var parameters = new List<SqlParameter>();
            var commandBehavior = CommandBehavior.Default;

            await using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync(ct);

            // Filter handling
            if (options.Id.HasValue && options.Id.Value > 0)
            {
                commandBehavior = CommandBehavior.SingleRow;

                whereSql.Append(" AND Id = @Id");
                parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = options.Id.Value });
            }

            if (!string.IsNullOrWhiteSpace(options.Username))
            {
                whereSql.Append(" AND Username = @Username");
                parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar) { Value = options.Username });
            }

            if (!string.IsNullOrWhiteSpace(options.Email))
            {
                whereSql.Append(" AND Email = @Email");
                parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar) { Value = options.Email });
            }

            if (options.IsActive.HasValue)
            {
                whereSql.Append(" AND IsActive = @IsActive");
                parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = options.IsActive.Value });
            }

            if (!string.IsNullOrWhiteSpace(options.Role))
            {
                whereSql.Append(" AND Role = @Role");
                parameters.Add(new SqlParameter("@Role", SqlDbType.NVarChar) { Value = options.Role });
            }

            // Build query
            var query = $@"SELECT Id, Username, Email, Password, CreatedDate, IsActive, Role FROM Users {whereSql};";
            using var command = new SqlCommand(query, connection);

            await using var reader = await command.ExecuteReaderAsync(commandBehavior, ct);

            var ordId = reader.GetOrdinal("Id");
            var ordUsername = reader.GetOrdinal("Username");
            var ordEmail = reader.GetOrdinal("Email");
            var ordPassword = reader.GetOrdinal("Password");
            var ordCreatedDate = reader.GetOrdinal("CreatedDate");
            var ordIsActive = reader.GetOrdinal("IsActive");
            var ordRole = reader.GetOrdinal("Role");

            while (await reader.ReadAsync(ct))
            {
                users.Add(new User
                {
                    Id = reader.GetInt32(ordId),
                    Username = reader.GetString(ordUsername),
                    Email = reader.GetString(ordEmail),
                    Password = reader.GetString(ordPassword),
                    CreatedDate = reader.GetDateTime(ordCreatedDate),
                    IsActive = reader.GetBoolean(ordIsActive),
                    Role = reader.GetString(ordRole),
                });
            }

            return users;
        }

        public async Task<bool> ValidateUserAsync(ValidateUserRequest request, CancellationToken ct)
        {
            await using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync(ct);

            // Build query
            var query = @"
                SELECT CASE WHEN EXISTS (
                    SELECT 1
                    FROM Users
                    WHERE Username = @Username AND Password = @Password
                ) THEN 1 ELSE 0 END;
            ";
            using var command = new SqlCommand(query, connection);

            command.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar) { Value = request.Username });
            command.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar) { Value = request.Password });

            var result = (int)await command.ExecuteScalarAsync(ct);
            return result > 0;

        }

        public async Task<User?> InsertUserAsync(CreateUserRequest request, CancellationToken ct)
        {
            await using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync(ct);

            await using var transaction = await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted, ct);

            try
            {
                // Build query
                var query = @"
                    INSERT INTO Users (Username, Email, Password, CreatedDate, IsActive, Role)
                    VALUES (@Username, @Email, @Password, GETDATE(), @IsActive, @Role);

                    SELECT TOP 1 Id, Username, Email, Password, CreatedDate, IsActive, Role
                    FROM Users
                    WHERE Username = @Username
                    ORDER BY CreatedDate DESC
                ";

                await using var command = new SqlCommand(query, connection);

                command.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 30) { Value = request.Username });
                command.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 30) { Value = request.Email });
                command.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 500) { Value = request.Password });
                command.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = request.IsActive });
                command.Parameters.Add(new SqlParameter("@Role", SqlDbType.Bit) { Value = request.Role });

                await using var reader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow, ct);

                if (!await reader.ReadAsync(ct))
                    return null;

                await transaction.CommitAsync(ct);

                return new User
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Username = reader.GetString(reader.GetOrdinal("Username")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    Password = reader.GetString(reader.GetOrdinal("Password")),
                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                    Role = reader.GetString(reader.GetOrdinal("Role")),
                };
            }
            catch
            {
                // Allow SQL Server to rollback if something happens
                await transaction.RollbackAsync(ct);
                throw;
            }
        }
    }
}
