using JobApplicants.Models;

namespace JobApplicants.Repositories
{
    public class InMemItemsRepository : IInMemItemsRepository
    {
        //Applicants list
        private readonly List<Applicant> applicants = new()
        {
            new Applicant { Id = Guid.NewGuid(), Name = "Joe", CreatedDate = DateTimeOffset.UtcNow },
            new Applicant { Id = Guid.NewGuid(), Name = "Jack", CreatedDate = DateTimeOffset.UtcNow },
            new Applicant { Id = Guid.NewGuid(), Name = "John", CreatedDate = DateTimeOffset.UtcNow }


        };

        //Jobs list
        private readonly List<JobPost> JobPosts = new()
        {
            new JobPost { jobId = Guid.NewGuid(), jobTitle = "Software Developer in test",  CreatedDate = DateTimeOffset.UtcNow,jobDescription = "Deploy unit tests for our aviation simulation technology.", jobCategory = "Software", jobLocation= "Montreal, CA", jobCompany= "CDPQ", jobDate="2023/04/16"},
            new JobPost { jobId = Guid.NewGuid(), jobTitle = "Front-End Developer", CreatedDate = DateTimeOffset.UtcNow,jobDescription = "Build React Components for UI library.", jobCategory ="Software", jobLocation= "Montreal, CA", jobCompany= "Haivision", jobDate="2023/02/16"},
            new JobPost { jobId = Guid.NewGuid(), jobTitle = "Backend Developer", CreatedDate = DateTimeOffset.UtcNow,jobDescription = "Build REST Api for our web application.", jobCategory ="Software", jobLocation="Montreal, CA", jobCompany= "Desjardins", jobDate = "2023/03/16"},


        };

        public IEnumerable<Applicant> GetApplicants()
        {
            return applicants;
        }

        public IEnumerable<JobPost> GetJobPosts()
        {
            return JobPosts;
        }

        public Applicant GetApplicant(Guid id)
        {
            //Note: singleordefault throws error if more than one as opposed to firstordefault
            return applicants.Where(applicant => applicant.Id == id).SingleOrDefault();
        }

        public JobPost GetPostById(Guid id)
        {
            //Note: singleordefault throws error if more than one as opposed to firstordefault
            return JobPosts.Where(jobPost => jobPost.jobId == id).SingleOrDefault();
        }

        public void AddApplicant(Applicant applicant)
        {
            applicants.Add(applicant);
        }

        public void AddJobPost(JobPost jobPost)
        {
            JobPosts.Add(jobPost);
        }

        public void UpdateApplicant(Applicant applicant)
        {
            var index = applicants.FindIndex(applicantExistant => applicantExistant.Id == applicant.Id);
            applicants[index] = applicant;
        }

        public void UpdateJobPost(JobPost jobPost)
        {
            var index = JobPosts.FindIndex(jobExistant => jobExistant.jobId == jobPost.jobId);
            JobPosts[index] = jobPost;
        }

        public void RemoveApplicant(Guid id)
        {
            var index = applicants.FindIndex(applicantExistant => applicantExistant.Id == id);
            applicants.RemoveAt(index);
        }

        public void RemoveJob(Guid id)
        {
            var index = JobPosts.FindIndex(jobExistant => jobExistant.jobId == id);
            JobPosts.RemoveAt(index);
        }
    }
}
