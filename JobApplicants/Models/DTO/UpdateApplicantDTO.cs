using System.ComponentModel.DataAnnotations;

namespace JobApplicants.Models.DTO
{
    public class UpdateApplicantDTO
    {
        public String? FirstName { get; init; }
        public String? LastName { get; init; }
        public String? City { get; init; }
        public String? State { get; init; }
        public String? JobPosition { get; init; }
        public String? Experience { get; init; }
        public String Password { get; init; }
        public String? Education { get; init; }
        public String? About { get; init; }
        public String? CV { get; init; }
        public String? ProfileImg { get; init; }
        public ProfileType profileType { get; init; }
        public String[]? appliedTo { get; init; } = new string[] { };


    }
}
