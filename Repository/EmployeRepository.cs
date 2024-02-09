using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeDetails.Interface;
using EmployeeDetails.Model;
using MongoDB.Driver;

namespace EmployeeDetails.Repository
{
    public class EmployeRepository : IEmploye
    {
        private readonly IMongoCollection<Employe> _employe;
        private readonly IMongoCollection<Company> _company;

        public EmployeRepository(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _employe = database.GetCollection<Employe>("Employe");
            _company = database.GetCollection<Company>("companies");
        }

        public async Task<List<EmployeeCompanyData>> GetCombinedDataByCompanyNameAsync(string companyName  )
        {
            var filter = Builders<Employe>.Filter.Eq(x => x.Companyname, companyName);

            var employeeProjection = Builders<Employe>.Projection
                .Include(x => x.Designation)
                .Include(x => x.Name)
                .Include(x => x.Companyname);

            var employees = await _employe
                .Find(filter)
                .Project<EmployeeCompanyData>(employeeProjection)
                .ToListAsync();

            var companyProjection = Builders<Company>.Projection
                .Include(x => x.Location);

            var company = await _company
                .Find(Builders<Company>.Filter.Eq(x => x.CompanyName, companyName))
                .Project<Company>(companyProjection)
                .FirstOrDefaultAsync();

            if (company == null)
            {
                
                return null;
            }

            var combinedData = new List<EmployeeCompanyData>();
            foreach (var employee in employees)
            {
                combinedData.Add(new EmployeeCompanyData
                {
                    Designation = employee.Designation,
                    Name = employee.Name,
                    CompanyName = employee.CompanyName,
                    Location = company.Location

                });
            }

            return combinedData;
        }

        public Employe Create(Employe employe)
        {
            _employe.InsertOne(employe);
            return employe;
        }

        public List<Employe> Get()
        {
            return _employe.Find(employe => true).ToList();
        }

        public Employe Get(string id)
        {
            return _employe.Find(Employe => Employe.Id == id).FirstOrDefault();
        }

        public void Remove(string id)
        {
            _employe.DeleteOne(employe => employe.Id == id);
        }

        public void Update(string id, Employe updatedEmploye)
        {
            _employe.ReplaceOne(user => user.Id == id, updatedEmploye);
        }
    }
}
