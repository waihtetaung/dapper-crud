using Dapper;
using DapperAspNetCore.Context;
using DapperAspNetCore.Contracts;
using DapperAspNetCore.Dto;
using DapperAspNetCore.Entity;
using System.Data;

namespace DapperAspNetCore.Repository
{
    public class CompanyRepostiory : ICompanyRepository
    {
        private readonly DapperContext _context;

        public CompanyRepostiory(DapperContext context) => _context = context;

        public async Task<Company> CreateCompany(CreateCompanyDto createCompany)
        {
            var query = "INSERT INTO Companies(Name, Address, Country) values (@Name, @Address, @Country)" +
                "SELECT CAST(SCOPE_IDENTITY() AS INT)";

            var parameter = new DynamicParameters();
            parameter.Add("Name", createCompany.Name, DbType.String);
            parameter.Add("Address", createCompany.Address, DbType.String);
            parameter.Add("Country", createCompany.Country, DbType.String);

            using (var connection = _context.CreateConnection)
            {
               var id = await connection.QuerySingleAsync<int>(query, parameter);

                var createdCompany = new Company { 
                    Id = id ,
                    Name = createCompany.Name,
                    Address = createCompany.Address,
                    Country = createCompany.Country
                };

                return createdCompany;
            }
        }

        public async Task DeleteCompany(int id)
        {
            var query = "DELETE FROM Companies WHERE Id = @Id";

            using(var connection = _context.CreateConnection)
            {
                await connection.ExecuteAsync(query, new {id});
            }
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            var query = "SELECT * FROM Companies";

            using (var connection = _context.CreateConnection)
            {
                var companies = await connection.QueryAsync<Company>(query);
                return companies.ToList();
            }
        }

        public async Task<Company> GetCompany(int id)
        {
            var query = "SELECT * FROM Companies WHERE Id = @Id";
            using(var connection = _context.CreateConnection)
            {
                var company = await connection.QuerySingleOrDefaultAsync<Company>(query, new {id});
                return company;
            }
        }

        public async Task UpdateCompany(int id, UpdateCompanyDto updateCompany)
        {
            var query = "UPDATE Companies SET Name=@Name, Address=@Address, Country=@Country WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int32);
            parameters.Add("Name", updateCompany.Name, DbType.String);
            parameters.Add("Address", updateCompany.Address, DbType.String);
            parameters.Add("Country", updateCompany.Country, DbType.String);

            using (var connection = _context.CreateConnection)
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
