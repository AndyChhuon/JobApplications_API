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
        Applicant GetApplicantByEmail(String email);
        Applicant GetApplicantById(Guid id);
        String GetApplicantByLogin(String pass, String email);
        void AddApplicantJobsApplied(Guid applicantId, String jobId);
        string CreateJWToken(Applicant applicant);
    }
}
