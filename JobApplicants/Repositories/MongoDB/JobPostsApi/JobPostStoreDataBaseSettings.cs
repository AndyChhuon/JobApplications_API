namespace JobApplicants.Repositories.MongoDB.JobPostsApi
{
    public class JobPostStoreDataBaseSettings : IJobPostStoreDataBaseSettings
    {
        public string JobApplicationsCollectionName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DataBaseName { get; set; } = string.Empty;
    }
}
