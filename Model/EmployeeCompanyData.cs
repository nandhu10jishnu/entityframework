using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmployeeDetails.Model
{
    public class EmployeeCompanyData
    {

        
        public string Designation { get; set; }

        
        public string Name { get; set; }

      
        public string CompanyName { get; set; }

       
        public string Location { get; set; }
    }
}
