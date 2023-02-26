namespace JobApplicants.Repositories.MongoDB.JobApplicantsApi
{
    public class JobApplicantStoreDataBaseSettings : IJobApplicantStoreDataBaseSettings
    {
        public string JobApplicantCollectionName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DataBaseName { get; set; } = string.Empty;
    }
}
