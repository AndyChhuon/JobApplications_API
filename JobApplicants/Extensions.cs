using JobApplicants.Models;
using JobApplicants.Models.DTO;

namespace JobApplicants
{
    public static class Extensions
    {
        public static ApplicantDTO toDTO(this Applicant Applicant)
        {
            return new ApplicantDTO { Id = Applicant.Id, Name = Applicant.Name, CreatedDate = Applicant.CreatedDate };
        }

        public static JobPostDTO toDTO(this JobPost JobPost)
        {
            return new JobPostDTO { jobId = JobPost.jobId, jobTitle = JobPost.jobTitle, jobDescription = JobPost.jobDescription, jobCategory = JobPost.jobCategory, jobLocation = JobPost.jobLocation, jobCompany = JobPost.jobCompany, jobDate = JobPost.jobDate, CreatedDate = JobPost.CreatedDate };
        }
    }
}
