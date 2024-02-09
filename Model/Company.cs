using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EmployeeDetails.Model
{
    public class Company
    {
       [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("companyName")]
        public string CompanyName { get; set; }

        [BsonElement("location")]
        public string Location { get; set; }


    }
}
