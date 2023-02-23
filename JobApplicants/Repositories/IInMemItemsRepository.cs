using JobApplicants.Models;

namespace JobApplicants.Repositories
{
    public interface IInMemItemsRepository
    {
        Applicant GetApplicant(Guid id);
        JobPost GetPostById(Guid id);
        IEnumerable<Applicant> GetApplicants();
        IEnumerable<JobPost> GetJobPosts();
        void AddApplicant(Applicant applicant);
        void AddJobPost(JobPost jobPost);
        void UpdateApplicant(Applicant applicant);
        void UpdateJobPost(JobPost jobPost);
        void RemoveApplicant(Guid applicantId);
        void RemoveJob(Guid id);


    }
}