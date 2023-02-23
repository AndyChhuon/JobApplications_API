using System.ComponentModel.DataAnnotations;

namespace JobApplicants.Models.DTO
{
    public class AddJobPostDTO
    { 
        [Required]
        public String jobTitle { get; init; }
        [Required]
        public string jobDescription { get; init; }
        [Required]
        public string jobCategory { get; init; }
        [Required]
        public string jobLocation { get; init; }
        [Required]
        public string jobCompany { get; init; }
        [Required]
        public string jobDate { get; init; }

    }
}
