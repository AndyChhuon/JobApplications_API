namespace JobApplicants.Repositories.MongoDB.JobPostsApi
{
    public interface IJobPostStoreDataBaseSettings
    {
        string JobApplicationsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DataBaseName { get; set; }

    }

}
