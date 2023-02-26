using JobApplicants.Models;

namespace JobApplicants.Repositories.MongoDB.JobApplicantsApi
{
    public interface IJobApplicantService
    {
        Applicant GetApplicant(Guid id);
        IEnumerable<Applicant> GetApplicants();
        Applicant AddApplicant(Applicant applicant);
        void UpdateApplicant (Applicant applicant);
        void RemoveApplicant(Guid id);
    }
}
