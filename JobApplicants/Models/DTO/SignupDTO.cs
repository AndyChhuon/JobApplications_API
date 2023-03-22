using System.ComponentModel.DataAnnotations;

namespace JobApplicants.Models.DTO
{
    public class SignupDTO
    { 
        [Required]
        public String email { get; init; }
        [Required]
        public String password { get; init; }

        public String? FirstName { get; init; } = "";
        public String? LastName { get; init; } = "";
        public String? ProfileImg { get; init; } = "";


    }
}
