using System.ComponentModel.DataAnnotations;

namespace JobApplicants.Models.DTO
{
    public class AddApplicantDTO
    { 
        [Required]
        public String FirstName { get; init; }
        [Required]
        public String LastName { get; init; }
        [Required]
        public String City { get; init; }
        [Required]
        public String State { get; init; }
        [Required]
        public String UserName { get; init; }
        [Required]
        public String Experience { get; init; }
        [Required]
        public String Email { get; init; }
        [Required]
        public String Password { get; init; }
        [Required]
        public String Education { get; init; }
        [Required]
        public String About { get; init; }
        [Required]
        public String ProfileImg { get; init; }

        [Required]
        public String CV { get; init; }

    }
}
