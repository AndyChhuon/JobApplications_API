﻿using JobApplicants.Models;
using JobApplicants.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace JobApplicants
{
    public static class Extensions
    {

        public static ApplicantDTO toDTO(this Applicant Applicant)
        {
            return new ApplicantDTO { Id = Applicant.Id, FirstName = Applicant.FirstName, LastName = Applicant.LastName, City = Applicant.City, State = Applicant.State, JobPosition = Applicant.JobPosition, Experience = Applicant.Experience, Email = Applicant.Email, Password = Applicant.Password, Education = Applicant.Education, About = Applicant.About, CV = Applicant.CV, ProfileImg = Applicant.ProfileImg, CreatedDate = Applicant.CreatedDate, profileType= Applicant.profileType, appliedTo = Applicant.appliedTo };
        }

        public static JobPostDTO toDTO(this JobPost JobPost)
        {
            return new JobPostDTO { jobId = JobPost.jobId, jobTitle = JobPost.jobTitle, jobDescription = JobPost.jobDescription, jobCategory = JobPost.jobCategory, jobLocation = JobPost.jobLocation, jobCompany = JobPost.jobCompany, jobDate = JobPost.jobDate, CreatedDate = JobPost.CreatedDate, WorkType = JobPost.WorkType, WorkTime = JobPost.WorkTime, Benefits = JobPost.Benefits, fullDescription= JobPost.fullDescription };
        }
    }
}
