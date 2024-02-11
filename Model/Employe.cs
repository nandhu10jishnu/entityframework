using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace EmployeeDetails.Model
{
    public class Employe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("companyName")]
        public string CompanyName { get; set; }

        [BsonElement("designation")]
        public string Designation { get; set; } 

        [BsonElement("department")]
        public string Department { get; set; }

        [BsonElement("gender")]
        public string Gender { get; set; } = string.Empty;

        [BsonElement("joinDate")]
        public DateTime? JoinDate { get; set; }

        [BsonElement("active")]
        public bool Active { get; set; }

    }
}
