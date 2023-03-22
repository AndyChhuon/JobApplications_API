using System.Xml.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace JobApplicants.Models
{
    public enum WorkType
    {
        Remote,
        InPerson,
        Hybrid,
    }
    public enum WorkTime
    {
        FullTime,
        PartTime,
        Contract,
    }

    [BsonIgnoreExtraElements]
    public record JobPost
    {
        [BsonId]
        //Converts mongo data type to .net data type and vice versa
        [BsonElement("jobId")]
        public Guid jobId { get; init; }
        [BsonElement("jobTitle")]
        public String jobTitle { get; init; }
        [BsonElement("jobDescription")]
        public string jobDescription { get; init; }
        [BsonElement("jobCategory")]
        public string jobCategory { get; init; }
        [BsonElement("jobLocation")]
        public string jobLocation { get; init; }
        [BsonElement("jobCompany")]
        public string jobCompany { get; init; }
        [BsonElement("jobDate")]
        public string jobDate { get; init; }
        [BsonElement("fullDescription")]
        public string fullDescription { get; init; }
        [BsonRepresentation(BsonType.String)]
        [BsonElement("workType")]
        public WorkType WorkType { get; init; }
        [BsonRepresentation(BsonType.String)]
        [BsonElement("workTime")]
        public WorkTime WorkTime { get; init; }
        [BsonElement("Benefits")]
        public String[] Benefits { get; init; }
        [BsonElement("CreatedDate")]
        public DateTimeOffset CreatedDate { get; init; }

        
    }
}
