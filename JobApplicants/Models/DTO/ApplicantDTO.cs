namespace JobApplicants.Models.DTO
{
    public record ApplicantDTO
    {
 
        public Guid Id { get; init; }
        public String FirstName { get; init; }
        public String LastName { get; init; }
        public String City { get; init; }
        public String State { get; init; }
        public String UserName { get; init; }
        public String Experience { get; init; }
        public String Email { get; init; }
        public String Password { get; init; }
        public String Education { get; init; }
        public String About { get; init; }
        public String CV { get; init; }
        public String ProfileImg { get; init; }
        public DateTimeOffset CreatedDate { get; init; }

        
    }
}
