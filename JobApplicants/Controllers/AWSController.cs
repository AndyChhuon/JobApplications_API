using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using JobApplicants.Models;
using JobApplicants.Models.DTO;
using JobApplicants.Repositories.MongoDB.JobApplicantsApi;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web;

namespace JobApplicants.Controllers
{
    //Define route to controller
    //Can use Route("api/[controller]") to automatically set route to JobApplicantsApi
    [Route("api/AwsAPI")]
    [ApiController]
    public class AWSController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly String bucket = "jobapplicants-bucket";

        //Constructor
        public AWSController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("CV")]
        public async Task<IActionResult> Post(IFormFile formFile, Guid studentId)
        {
            var accessKey = _configuration.GetValue<string>("AWS:AccessKey");
            var secretKey = _configuration.GetValue<string>("AWS:SecretKey");

            var client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.USEast2);

            var objectRequest = new PutObjectRequest()
            {
                BucketName = bucket,
                Key = "CV/"+ studentId+"/"+formFile.FileName,
                InputStream = formFile.OpenReadStream(),


            };

            var response = await client.PutObjectAsync(objectRequest);

            return Ok("https://jobapplicants-bucket.s3.us-east-2.amazonaws.com/CV/" + studentId + "/" + HttpUtility.UrlEncode(formFile.FileName));

        }

        [HttpPost("CoverLetter")]
        public async Task<IActionResult> PostCoverLetter(IFormFile formFile, Guid studentId)
        {
            var accessKey = _configuration.GetValue<string>("AWS:AccessKey");
            var secretKey = _configuration.GetValue<string>("AWS:SecretKey");

            var client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.USEast2);

            var objectRequest = new PutObjectRequest()
            {
                BucketName = bucket,
                Key = "Cover_letter/" + studentId + "/" + formFile.FileName,
                InputStream = formFile.OpenReadStream(),


            };


            var response = await client.PutObjectAsync(objectRequest);


            return Ok("https://jobapplicants-bucket.s3.us-east-2.amazonaws.com/Cover_letter/" + studentId + "/" + HttpUtility.UrlEncode(formFile.FileName));

        }

        [HttpPost("ProfileImages")]
        public async Task<IActionResult> PostProfileImage(IFormFile formFile, Guid studentId)
        {
            var accessKey = _configuration.GetValue<string>("AWS:AccessKey");
            var secretKey = _configuration.GetValue<string>("AWS:SecretKey");

            var client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.USEast2);

            var objectRequest = new PutObjectRequest()
            {
                BucketName = bucket,
                Key = "Profile_images/" + studentId + "/" + formFile.FileName,
                InputStream = formFile.OpenReadStream(),


            };

            var response = await client.PutObjectAsync(objectRequest);

            return Ok("https://jobapplicants-bucket.s3.us-east-2.amazonaws.com/Profile_images/" + studentId + "/" + HttpUtility.UrlEncode(formFile.FileName));
        }
    }
}
