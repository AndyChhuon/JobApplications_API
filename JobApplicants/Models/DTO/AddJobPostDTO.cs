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
        [Required]
        public WorkType WorkType { get; init; }
        [Required]
        public String[] Benefits { get; init; }
        [Required]
        public WorkTime WorkTime { get; init; }
        [Required]
        public string fullDescription { get; init; }


    }
}
