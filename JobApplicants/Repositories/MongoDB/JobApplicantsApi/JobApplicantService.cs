using JobApplicants.Models;
using JobApplicants.Repositories.MongoDB.JobApplicantsApi;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JobApplicants.Repositories.MongoDB.JobPostsApi
{
    public class JobApplicantService : IJobApplicantService

    {
        private readonly IMongoCollection<Applicant> _Applicants;
        private readonly IConfiguration _configuration;


        public JobApplicantService(IJobApplicantStoreDataBaseSettings settings, IMongoClient mongoClient, IConfiguration configuration) {
            var datatabse = mongoClient.GetDatabase(settings.DataBaseName);
            _Applicants = datatabse.GetCollection<Applicant>(settings.JobApplicantCollectionName);
            _configuration = configuration;

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

        public String GetApplicantByLogin(String pass, String email)
        {

           

            Applicant applicant= _Applicants.Find(Applicant => Applicant.Email == email).FirstOrDefault();

            //Applicant not signed up with google and not null
            if(applicant != null && applicant.Password != "Google")
            {
                
                if (!BCrypt.Net.BCrypt.Verify(pass, applicant.Password))
                {
                    return null;
                }

                    
                string token = CreateJWToken(applicant);
                return token;

            }
            else
            {
                //Signed up with Google
                if(applicant?.Password == "Google" && pass == "Google")
                {
                    string token = CreateJWToken(applicant);
                    return token;
                }
                return null;
            }


        }

        public Applicant GetApplicantById(Guid id)
        {

            return _Applicants.Find(Applicant => Applicant.Id == id).FirstOrDefault();
        }

        public Applicant GetApplicantByEmail(String email)
        {

            return _Applicants.Find(Applicant => Applicant.Email == email).FirstOrDefault();
        }

        public Applicant GetApplicantByPassword(Guid id)
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

        public void AddApplicantJobsApplied(Guid applicantId, String jobId)
        {
            _Applicants.UpdateOne(Builders<Applicant>.Filter.Eq("Id", applicantId), Builders<Applicant>.Update.Push("appliedTo", jobId));
        }

        public string CreateJWToken(Applicant applicant)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,applicant.Email),
                new Claim(ClaimTypes.NameIdentifier,applicant.Id.ToString())
            };

            var secretToken = _configuration.GetValue<string>("Admin:Token");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretToken!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(2),
                    signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
