using DapperAspNetCore.Dto;
using DapperAspNetCore.Entity;

namespace DapperAspNetCore.Contracts
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();
        public Task<Company> GetCompany(int id);
        public Task<Company> CreateCompany(CreateCompanyDto createCompany);
        public Task UpdateCompany(int id, UpdateCompanyDto updateCompany);
        public Task DeleteCompany(int id);

    }
}
