using JobApplicants.Models;
using MongoDB.Bson;

namespace JobApplicants.Repositories.MongoDB.JobPostsApi
{
    public interface IJobPostService
    {
        JobPost GetPostById(Guid id);
        IEnumerable<JobPost> searchPosts(String? keyword, String? Location, String? Category, String? workTime, String? workType, String[]? Benefits );
        IEnumerable<JobPost> GetJobPosts();
        JobPost AddJobPost(JobPost jobPost);
        void UpdateJobPost(JobPost jobPost);
        void RemoveJob(Guid id);

    }
}
