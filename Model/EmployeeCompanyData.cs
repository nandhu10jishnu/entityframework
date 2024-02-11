using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmployeeDetails.Model
{
    public class EmployeeCompanyData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string designation { get; set; }
        public string name { get; set; }
        public string companyName { get; set; }
        public string location { get; set; }
    }
}
