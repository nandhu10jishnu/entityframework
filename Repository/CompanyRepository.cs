using EmployeeDetails.Interface;
using EmployeeDetails.Model;
using MongoDB.Driver;
using System.Collections.Generic;

namespace EmployeeDetails.Repository
{
    public class CompanyRepository : ICompany
    {
        private readonly IMongoCollection<Company> _company;

        public CompanyRepository(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _company = database.GetCollection<Company>("Company");
        }

        public Company Create(Company company)
        {
            _company.InsertOne(company);
            return company;
        }

        public List<Company> Get()
        {
            return _company.Find(company => true).ToList();
        }

        public Company Get(string id)
        {
            return _company.Find(Company => Company.Id == id).FirstOrDefault();
        }

        public void Remove(string id)
        {
            _company.DeleteOne(company => company.Id == id);
        }

        public void Update(string id, Company updatedCompany)
        {
            _company.ReplaceOne(company => company.Id == id, updatedCompany);
        }
    }
}
