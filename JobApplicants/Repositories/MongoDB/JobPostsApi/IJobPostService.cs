using JobApplicants.Models;
using MongoDB.Bson;

namespace JobApplicants.Repositories.MongoDB.JobPostsApi
{
    public interface IJobPostService
    {
        JobPost GetPostById(Guid id);
        IEnumerable<JobPost> searchPosts(String keyword);
        IEnumerable<JobPost> GetJobPosts();
        JobPost AddJobPost(JobPost jobPost);
        void UpdateJobPost(JobPost jobPost);
        void RemoveJob(Guid id);

    }
}
