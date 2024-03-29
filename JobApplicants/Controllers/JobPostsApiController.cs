﻿using JobApplicants.Models;
using JobApplicants.Models.DTO;
using JobApplicants.Repositories.InMemory;
using JobApplicants.Repositories.MongoDB.JobPostsApi;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace JobApplicants.Controllers
{
    //Define route to controller
    //Can use Route("api/[controller]") to automatically set route to JobApplicantsApi
    [Route("api/JobPostsAPI")] 
    [ApiController]
    public class JobPostsApiController : ControllerBase
    {

        private readonly IJobPostService repository;

        //Constructor
        public JobPostsApiController(IJobPostService repository)
        {
            this.repository = repository;
        }

        //Identify get endpoint (Get /api/JobPostsAPI/)
        [HttpGet]
        public IEnumerable<JobPostDTO> GetJobPosts() {
            var jobPosts = repository.GetJobPosts().Select(jobPost => jobPost.toDTO());
            return jobPosts;
        }

        // Get /api/JobPostsAPI/{id}
        [HttpGet("{id}")]
        public ActionResult<JobPostDTO> GetPost(Guid id)
        {
            var item = repository.GetPostById(id);

            if(item is null)
            {
                return NotFound();
            }

            return item.toDTO();
        }

        // Get /api/JobPostsAPI/search
        [HttpPost("search")]
        public IEnumerable<JobPost> SearchPost(String? keyword, String? Location, String? Category, String? workTime, String? workType, String[]? Benefits)
        {
            var item = repository.searchPosts(keyword, Location, Category, workTime, workType,Benefits);

            return item;
        }

        // POST /api/JobPostsAPI/
        [HttpPost]
        public ActionResult<JobPostDTO> AddPost(AddJobPostDTO postDTO)
        {
            JobPost newPost = new() { jobId = Guid.NewGuid(), jobTitle = postDTO.jobTitle, CreatedDate = DateTimeOffset.UtcNow, jobDescription = postDTO.jobDescription, jobCategory = postDTO.jobCategory, jobCompany = postDTO.jobCompany, jobLocation = postDTO.jobLocation, jobDate = postDTO.jobDate, WorkType = postDTO.WorkType, WorkTime = postDTO.WorkTime, Benefits = postDTO.Benefits, fullDescription = postDTO.fullDescription };
            repository.AddJobPost(newPost);

            return CreatedAtAction(nameof(GetJobPosts), new { id = newPost.jobId }, newPost.toDTO());
        }

        // PUT /api/JobPostsAPI/{id}
        [HttpPut("{id}")]
        public ActionResult<UpdateJobPostDTO> UpdatePost(Guid id, UpdateJobPostDTO postDTO)
        {
            var existingPost = repository.GetPostById(id);
            
            if(existingPost is null)
            {
                return NotFound();
            }

            JobPost updatedPost = existingPost with
            {
                jobTitle = postDTO.jobTitle,
                jobDescription = postDTO.jobDescription,
                jobCategory = postDTO.jobCategory,
                jobLocation = postDTO.jobLocation,
                jobCompany = postDTO.jobCompany,
                jobDate = postDTO.jobDate,
                WorkType = postDTO.WorkType,
                WorkTime = postDTO.WorkTime,
                Benefits = postDTO.Benefits,
                fullDescription = postDTO.fullDescription

            };

            repository.UpdateJobPost(updatedPost);

            //Convention for PUT (204)
            return NoContent();
        }

        // DELETE /api/JobPostsAPI/{id}
        [HttpDelete("{id}")]
        public ActionResult<UpdateJobPostDTO> RemoveJob(Guid id)
        {
            var existingPost = repository.GetPostById(id);

            if (existingPost is null)
            {
                return NotFound();
            }


            repository.RemoveJob(id);

            //Convention for Delete (204)
            return NoContent();
        }
    }
}
