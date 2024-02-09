using EmployeeDetails.Model;

namespace EmployeeDetails.Interface
{
    public interface IEmploye
    {
        Task<List<EmployeeCompanyData>> GetCombinedDataByCompanyNameAsync(string companyName);
        List<Employe> Get();
        Employe Get(string id);
        Employe Create(Employe employe);
        void Update(string id, Employe employe);
        void Remove(string id);
    }
}
