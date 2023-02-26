using JobApplicants.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace JobApplicants.Repositories.MongoDB.JobPostsApi
{
    public class JobPostService : IJobPostService

    {
        private readonly IMongoCollection<JobPost> _JobPosts;

        public JobPostService(IJobPostStoreDataBaseSettings settings, IMongoClient mongoClient) {
            var datatabse = mongoClient.GetDatabase(settings.DataBaseName);
            _JobPosts = datatabse.GetCollection<JobPost>(settings.JobApplicationsCollectionName);
        }

        public JobPost AddJobPost(JobPost jobPost)
        {
            _JobPosts.InsertOne(jobPost);
            return jobPost;
        }

        public IEnumerable<JobPost> GetJobPosts()
        {
            return _JobPosts.Find(jobPost => true).ToList();
        }

        public JobPost GetPostById(Guid id)
        {
            return _JobPosts.Find(jobPost => jobPost.jobId == id).FirstOrDefault();
        }

        public IEnumerable<JobPost> searchPosts(String keyword)
        {

            String query = "(.*)";
            string[] words = keyword.Split(' ');
            for (var i = 0; i < words.Length; i++)
            {
                query += words[i] + "(.*)";
            }

            var pipeline = new BsonDocument[]
            {
              new BsonDocument("$search", new BsonDocument {
                {
                  "index",
                  "CaseInsensitiveSearch"
                },
                {
                  "regex",
                  new BsonDocument {
                    {
                      "query",
                      query                    },
                    {
                      "path",
                      new BsonDocument("wildcard", "*")
                    },
                    {
                      "allowAnalyzedField",
                       true
                    }
                  }
                },

              })
            
            };

            return _JobPosts
                .Aggregate<JobPost>(pipeline).ToList();
        }

        public void RemoveJob(Guid id)
        {
            _JobPosts.DeleteOne(jobPost => jobPost.jobId == id);
        }

        public void UpdateJobPost(JobPost jobPost)
        {
            _JobPosts.ReplaceOne(jobExistant => jobExistant.jobId == jobPost.jobId, jobPost);
        }
    }
}
