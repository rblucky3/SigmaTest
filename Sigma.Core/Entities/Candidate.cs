using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Sigma.Core.Entities
{
    public class Candidate
    {
        [JsonIgnore]
        public int Id { get; set; }

        [MaxLength(20)]
        public required string FirstName { get; set; }

        [MaxLength(20)]
        public required string LastName { get; set; }

        [MaxLength(15)]
        public string? PhoneNumber { get; set; }

        [MaxLength(30)]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public required string Email { get; set; }  // Unique identifier
        public string? CallTimeInterval { get; set; }

        [MaxLength(200)]
        public string? LinkedInProfileUrl { get; set; }
        [MaxLength(200)]
        public string? GitHubProfileUrl { get; set; }


        public required string Comment { get; set; }
    }
}
