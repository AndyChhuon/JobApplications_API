using JobApplicants.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace JobApplicants.Repositories.MongoDB.JobPostsApi
{
    public class JobPostService : IJobPostService

    {
        private readonly IMongoCollection<JobPost> _JobPosts;

        public JobPostService(IJobPostStoreDataBaseSettings settings, IMongoClient mongoClient)
        {
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

        public IEnumerable<JobPost> searchPosts(String? keyword, String? Location, String? Category, String? workTime, String? workType, String[]? Benefits)
        {

            BsonDocument[] searchArr = { null, null, null, null, null, null };
            var count = 0;


            //Keyword search
            if (keyword != null)
            {
                String query = "(.*)";
                string[] words = keyword.Split(' ');
                for (var i = 0; i < words.Length; i++)
                {
                    query += words[i] + "(.*)";
                }

                var search = new BsonDocument("$search", new BsonDocument {
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
                } });
                searchArr[count] = search;
                count++;
            }

            //Location Search
            if (Location != null)
            {
                String query = "(.*)";
                string[] words = Location.Split(' ');
                for (var i = 0; i < words.Length; i++)
                {
                    query += words[i] + "(.*)";
                }

                var search = new BsonDocument { { "$match", new BsonDocument {
                    { "jobLocation", new BsonDocument { { "$regex", query }, { "$options", "i" } } } } }
                };

                searchArr[count] = search;
                count++;
            }


            //Category Search
            if (Category != null)
            {
                var search = new BsonDocument { { "$match", new BsonDocument { { "jobCategory", Category } } } };

                searchArr[count] = search;
                count++;
            }

            //workTime Search
            if (workTime != null)
            {
                var search = new BsonDocument { { "$match", new BsonDocument { { "workTime", workTime } } } };

                searchArr[count] = search;
                count++;
            }

            //workType Search
            if (workType != null)
            {
                var search = new BsonDocument { { "$match", new BsonDocument { { "workType", workType } } } };

                searchArr[count] = search;
                count++;
            }

            //Benefits Search
            if (Benefits != null)
            {

                BsonArray bArray = new BsonArray();
                foreach (var Benefit in Benefits)
                {
                    bArray.Add(Benefit);
                }



                var search = new BsonDocument { { "$match", new BsonDocument { { "Benefits", new BsonDocument { { "$in", bArray } } } } } };

                searchArr[count] = search;
                count++;
            }

            BsonDocument[] pipeline = new BsonDocument[count];
            for (var i=0;i<searchArr.Length;i++){

                if(searchArr[i] != null)
                {
                    pipeline[i] = searchArr[i];
                }
            }





            return _JobPosts
                .Aggregate<JobPost>(pipeline).ToList();
        }

        public IEnumerable<JobPost> searchPostsTemp(String keyword)
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
                } }),
              new BsonDocument{ { "$match", new BsonDocument { { "workType", "InPerson" } } } },
              new BsonDocument{ { "$match", new BsonDocument {{ "jobLocation", new BsonDocument { { "$regex", "Montreal" } } } } } }


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
