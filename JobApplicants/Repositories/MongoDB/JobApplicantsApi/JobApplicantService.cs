using JobApplicants.Models;
using JobApplicants.Repositories.MongoDB.JobApplicantsApi;
using MongoDB.Driver;

namespace JobApplicants.Repositories.MongoDB.JobPostsApi
{
    public class JobApplicantService : IJobApplicantService

    {
        private readonly IMongoCollection<Applicant> _Applicants;

        public JobApplicantService(IJobApplicantStoreDataBaseSettings settings, IMongoClient mongoClient) {
            var datatabse = mongoClient.GetDatabase(settings.DataBaseName);
            _Applicants = datatabse.GetCollection<Applicant>(settings.JobApplicantCollectionName);
        }

        public Applicant AddApplicant(Applicant applicant)
        {
            _Applicants.InsertOne(applicant);
            return applicant;
        }

        public Applicant GetApplicant(Guid id)
        {
            return _Applicants.Find(Applicant => Applicant.Id == id).FirstOrDefault();
        }

        public IEnumerable<Applicant> GetApplicants()
        {
            return _Applicants.Find(Applicant => true).ToList();
        }

        public void RemoveApplicant(Guid id)
        {
            _Applicants.DeleteOne(Applicant => Applicant.Id == id);
        }

        public void UpdateApplicant(Applicant applicant)
        {
            _Applicants.ReplaceOne(ApplicantExistant => ApplicantExistant.Id == applicant.Id, applicant);
        }
    }
}
