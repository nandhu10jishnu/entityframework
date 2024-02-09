using EmployeeDetails.Model;

namespace EmployeeDetails.Interface
{
    public interface ICompany
    {
        List<Company> Get();
        Company Get(string id);
        Company Create(Company company);
        void Update(string id, Company company);
        void Remove(string id);
    }
}
