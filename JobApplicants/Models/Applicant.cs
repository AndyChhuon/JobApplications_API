using MongoDB.Bson.Serialization.Attributes;

namespace JobApplicants.Models
{
    [BsonIgnoreExtraElements]
    public record Applicant
    {
        [BsonId]
        //Converts mongo data type to .net data type and vice versa
        [BsonElement("Id")]
        public Guid Id { get; init; }
        [BsonElement("FirstName")]
        public String FirstName { get; init; }
        [BsonElement("LastName")]
        public String LastName { get; init; }
        [BsonElement("City")]
        public String City { get; init; }
        [BsonElement("State")]
        public String State { get; init; }
        [BsonElement("UserName")]
        public String UserName { get; init; }
        [BsonElement("Experience")]
        public String Experience { get; init; }
        [BsonElement("Email")]
        public String Email { get; init; }
        [BsonElement("Password")]
        public String Password { get; init; }
        [BsonElement("Education")]
        public String Education { get; init; }
        [BsonElement("About")]
        public String About { get; init; }
        [BsonElement("ProfileImg")]
        public String ProfileImg { get; init; }
        [BsonElement("CV")]
        public String CV { get; init; }
        [BsonElement("CreatedDate")]
        public DateTimeOffset CreatedDate { get; init; }

        
    }
}
