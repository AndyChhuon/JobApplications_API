using JobApplicants.Models;
using JobApplicants.Models.DTO;
using JobApplicants.Repositories.MongoDB.JobApplicantsApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Security.Claims;

namespace JobApplicants.Controllers
{
    //Define route to controller
    //Can use Route("api/[controller]") to automatically set route to JobApplicantsApi
    [Route("api/JobApplicantsAPI")] 
    [ApiController]
    public class JobApplicantsApiController : ControllerBase
    {

        private readonly IJobApplicantService repository;
        private readonly IConfiguration _configuration;




        //Constructor
        public JobApplicantsApiController(IJobApplicantService repository, IConfiguration configuration)
        {
            this.repository = repository;
            _configuration = configuration;

        }

        //Identify get endpoint (Get /api/JobApplicantsAPI/)
        [HttpGet]
        public ActionResult<IEnumerable<ApplicantDTO>> GetApplicants(String password) {
            var adminKey = _configuration.GetValue<string>("Admin:AdminPass");


            if (password != adminKey)
            {
                return NotFound();

            }
            var applicants = repository.GetApplicants().Select(Applicant => Applicant.toDTO());
            return Ok(applicants);
        }

        // Get /api/JobApplicantsAPI/{id}
        [HttpGet("{id}")]
        public ActionResult<ApplicantDTO> GetApplicant(Guid id)
        {
            var item = repository.GetApplicant(id);

            if(item is null)
            {
                return NotFound();
            }

            Applicant applicant = new() { Id = item.Id, FirstName = item.FirstName, LastName = item.LastName, City = item.City, State = item.State, JobPosition = item.JobPosition, Experience = item.Experience, Email = item.Email, Password = "", Education = item.Education, About = item.About, CV = item.CV, CreatedDate = item.CreatedDate, ProfileImg = item.ProfileImg, profileType = item.profileType, appliedTo = item.appliedTo };
            

            return applicant.toDTO();
        }

        // Get /api/JobApplicantsAPI/login
        [HttpGet("login")]
        public ActionResult<String> GetApplicantByLogin([FromQuery] LoginDTO loginDetails)
        {
            var item = repository.GetApplicantByLogin(loginDetails.password, loginDetails.email);

            if (item is null)
            {
                var emailLoggedIn = repository.GetApplicantByEmail(loginDetails.email);
                if(emailLoggedIn != null)
                {
                    if(emailLoggedIn.Password == "Google")
                    {
                        return BadRequest("Email Found. Please Login with Google.");

                    }
                    else if (loginDetails.password == "Google")
                    {
                        return BadRequest("Not signed up with google. Please login with password.");

                    }
                    else
                    {
                        return BadRequest("Wrong Password. Please try again.");

                    }
                }
                //Prompt Sign up
                else
                {
                    return NotFound();

                }

            }



            return item;
        }

        // Get /api/JobApplicantsAPI/
        [HttpPost]
        public ActionResult<ApplicantDTO> ApplicantSignup(SignupDTO signupDetails )
        {
            //Check if email in database
            var emailLoggedIn = repository.GetApplicantByEmail(signupDetails.email);

            if (emailLoggedIn != null)
            {
                return BadRequest("Email Found. Please Login instead.");

            }

            string passWordHash;

            //Signed up with google
            if (signupDetails.password == "Google")
            {
                passWordHash = "Google";

            }
            else
            {
                passWordHash = BCrypt.Net.BCrypt.HashPassword(signupDetails.password);
            }

            Applicant applicant = new() { Id = Guid.NewGuid(), FirstName = signupDetails.FirstName, LastName = signupDetails.LastName, City = "", State = "", JobPosition = "", Experience = "", Email = signupDetails.email, Password = passWordHash, Education = "", About = "", CV = "", CreatedDate = DateTimeOffset.UtcNow, ProfileImg = signupDetails.ProfileImg, profileType = ProfileType.Hybrid, appliedTo = new string[] { } };
            repository.AddApplicant(applicant);



            return CreatedAtAction(nameof(GetApplicant), new { id = applicant.Id }, applicant.toDTO());
        }


        // PUT /api/JobApplicantsAPI/{id}
        [HttpPut("{id}"), Authorize]
        public ActionResult<UpdateApplicantDTO> UpdateApplicant(Guid id, UpdateApplicantDTO applicantDTO)
        {

            //Check id is correct in bearer jwt token
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return BadRequest("Identity of JWT token is null.");

            }

            var identifier = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(identifier != id.ToString()) {
                return BadRequest("JWT token holds a different ID than the one requested to operate on.");

            }

            var existingApplicant = repository.GetApplicantById(id);
            
            if(existingApplicant is null)
            {
                return NotFound();
            }

            string passWordHash;

            //Signed up with google
            if (applicantDTO.Password == "Google")
            {
                passWordHash = "Google";

            }
            else
            {
                passWordHash = BCrypt.Net.BCrypt.HashPassword(applicantDTO.Password);
            }

            Applicant updatedApplicant = existingApplicant with
            {
                FirstName = applicantDTO.FirstName,
                LastName = applicantDTO.LastName,
                City = applicantDTO.City,
                State = applicantDTO.State,
                JobPosition = applicantDTO.JobPosition,
                Experience = applicantDTO.Experience,
                Password = passWordHash,
                Education = applicantDTO.Education,
                About = applicantDTO.About,
                CV = applicantDTO.CV,
                ProfileImg = applicantDTO.ProfileImg,
                profileType = applicantDTO.profileType,
                appliedTo = applicantDTO.appliedTo
            };

            repository.UpdateApplicant(updatedApplicant);

            //Convention for PUT (204)
            return NoContent();
        }



        // PUT /api/JobApplicantsAPI/updateJobs/{id}
        [HttpPut("updateJobs/{id}"), Authorize]
        public ActionResult<UpdateApplicantDTO> UpdateApplicantAppliedJobs(Guid id, String JobId)
        {

            //Check id is correct in bearer jwt token
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return BadRequest("Identity of JWT token is null.");

            }

            var identifier = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (identifier != id.ToString())
            {
                return BadRequest("JWT token holds a different ID than the one requested to operate on.");

            }

            //Get applicant and edit
            var existingApplicant = repository.GetApplicantById(id);

            if (existingApplicant is null)
            {
                return NotFound();
            }

            repository.AddApplicantJobsApplied(id, JobId);

            //Convention for PUT (204)
            return NoContent();
        }


        // DELETE /api/JobApplicantsAPI/{id}
        [HttpDelete("{id}"), Authorize]
        public ActionResult<UpdateApplicantDTO> RemoveApplicant(Guid id)
        {

            //Check id is correct in bearer jwt token
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return BadRequest("Identity of JWT token is null.");

            }

            var identifier = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (identifier != id.ToString())
            {
                return BadRequest("JWT token holds a different ID than the one requested to operate on.");

            }

            //Get applicant and delete
            var existingApplicant = repository.GetApplicantById(id);

            if (existingApplicant is null)
            {
                return NotFound();
            }


            repository.RemoveApplicant(id);

            //Convention for Delete (204)
            return NoContent();
        }
    }
}
