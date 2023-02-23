namespace JobApplicants.Models
{
    public record JobPost
    {
        public Guid jobId { get; init; }
        public String jobTitle { get; init; }
        public string jobDescription { get; init; }
        public string jobCategory { get; init; }
        public string jobLocation { get; init; }
        public string jobCompany { get; init; }
        public string jobDate { get; init; }
        public DateTimeOffset CreatedDate { get; init; }

        
    }
}
