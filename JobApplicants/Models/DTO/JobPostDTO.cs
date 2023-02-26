using MongoDB.Bson.Serialization.Attributes;

namespace JobApplicants.Models.DTO
{
    public record JobPostDTO
    {

        public Guid jobId { get; init; }
        public String jobTitle { get; init; }
        public string jobDescription { get; init; }
        public string jobCategory { get; init; }
        public string jobLocation { get; init; }
        public string jobCompany { get; init; }
        public string jobDate { get; init; }
        public string fullDescription { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
        public WorkType WorkType { get; init; }
        public WorkTime WorkTime { get; init; }
        public String[] Benefits { get; init; }


    }
}
