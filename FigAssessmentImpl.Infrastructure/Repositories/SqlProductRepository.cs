using FigAssessmentImpl.Application.Products;
using FigAssessmentImpl.Application.Products.CreateProducts;
using FigAssessmentImpl.Application.Products.GetProducts;
using FigAssessmentImpl.Domain.Products;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigAssessmentImpl.Infrastructure.Repositories
{
    public sealed class SqlProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public SqlProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IReadOnlyList<Product>?> GetProductsAsync(GetProductQueryOptions options, CancellationToken ct)
        {
            var products = new List<Product>();
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

            if (!string.IsNullOrWhiteSpace(options.Name))
            {
                whereSql.Append(" AND Name = @Name");
                parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar) { Value = options.Name });
            }

            if (options.MinPrice.HasValue && options.MinPrice.Value > 0)
            {
                whereSql.Append(" AND Price >= @MinPrice");
                parameters.Add(new SqlParameter("@MinPrice", SqlDbType.Decimal) { Value = options.MinPrice.Value });
            }

            if (options.MaxPrice.HasValue && options.MaxPrice.Value > 0)
            {
                whereSql.Append(" AND Price <= @MaxPrice");
                parameters.Add(new SqlParameter("@MaxPrice", SqlDbType.Decimal) { Value = options.MaxPrice.Value });
            }

            if (!string.IsNullOrWhiteSpace(options.Category))
            {
                whereSql.Append(" AND Category = @Category");
                parameters.Add(new SqlParameter("@Category", SqlDbType.NVarChar) { Value = options.Category });
            }

            if (options.InStock.HasValue)
            {
                whereSql.Append(" AND InStock = @InStock");
                parameters.Add(new SqlParameter("@InStock", SqlDbType.Bit) { Value = options.InStock.Value });
            }

            if (!string.IsNullOrWhiteSpace(options.SearchTerm))
            {
                whereSql.Append(" AND (Name LIKE '%@SearchTerm%' OR Description LIKE '%@SearchTerm%')");
                parameters.Add(new SqlParameter("@SearchTerm", SqlDbType.NVarChar) { Value = options.SearchTerm });
            }

            // Pagination handling
            var offsetSql = $"OFFSET {(options.Page - 1) * options.PageSize} ROWS FETCH NEXT {options.PageSize} ROWS ONLY";

            // Build query
            var query = $@"SELECT Id, Name, Description, Price, Category, InStock, CreatedDate FROM Products {whereSql} {offsetSql};";
            using var command = new SqlCommand(query, connection);

            await using var reader = await command.ExecuteReaderAsync(commandBehavior, ct);

            var ordId = reader.GetOrdinal("Id");
            var ordName = reader.GetOrdinal("Name");
            var ordDescription = reader.GetOrdinal("Description");
            var ordPrice = reader.GetOrdinal("Price");
            var ordCategory = reader.GetOrdinal("Category");
            var ordInStock = reader.GetOrdinal("InStock");
            var ordCreatedDate = reader.GetOrdinal("CreatedDate");

            while (await reader.ReadAsync(ct))
            {
                products.Add(new Product
                {
                    Id = reader.GetInt32(ordId),
                    Name = reader.GetString(ordName),
                    Description = reader.GetString(ordDescription),
                    Price = reader.GetDecimal(ordPrice),
                    Category = reader.GetString(ordCategory),
                    InStock = reader.GetBoolean(ordInStock),
                    CreatedDate = reader.GetDateTime(ordCreatedDate),
                });
            }

            return products;
        }

        public async Task InsertProductAsync(CreateProductRequest request, CancellationToken ct)
        {
            await using var connection = new SqlConnection(_connectionString);

            await connection.OpenAsync(ct);

            await using var transaction = await connection.BeginTransactionAsync(IsolationLevel.ReadCommitted, ct);

            try
            {
                // Build query
                var query = @"
                    INSERT INTO Products (Name, Description, Price, Category, InStock, CreatedDate)
                    VALUES (@Name, @Description, @Price, @Category, @InStock, GETDATE());
                ";

                await using var command = new SqlCommand(query, connection);

                command.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 200) { Value = request.Name });
                command.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 2000) { Value = request.Description });
                command.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal) { Value = request.Price });
                command.Parameters.Add(new SqlParameter("@Category", SqlDbType.NVarChar, 500) { Value = request.Category });
                command.Parameters.Add(new SqlParameter("@InStock", SqlDbType.Bit) { Value = request.InStock });

                await command.ExecuteNonQueryAsync(ct);

                await transaction.CommitAsync(ct);
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
