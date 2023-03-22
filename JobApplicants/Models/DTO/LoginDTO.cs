using System.ComponentModel.DataAnnotations;

namespace JobApplicants.Models.DTO
{
    public class LoginDTO
    { 
        [Required]
        public String email { get; init; }
        [Required]
        public String password { get; init; }


    }
}
