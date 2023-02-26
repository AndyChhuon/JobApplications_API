namespace JobApplicants.Repositories.MongoDB.JobApplicantsApi
{
    public interface IJobApplicantStoreDataBaseSettings
    {
        string JobApplicantCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DataBaseName { get; set; }

    }

}
