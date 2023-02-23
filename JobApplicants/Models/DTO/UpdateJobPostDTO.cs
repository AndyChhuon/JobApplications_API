using System.ComponentModel.DataAnnotations;

namespace JobApplicants.Models.DTO
{
    public class UpdateJobPostDTO
    {
        public String jobTitle { get; init; }
        public string jobDescription { get; init; }
        public string jobCategory { get; init; }
        public string jobLocation { get; init; }
        public string jobCompany { get; init; }
        public string jobDate { get; init; }
    }
}
