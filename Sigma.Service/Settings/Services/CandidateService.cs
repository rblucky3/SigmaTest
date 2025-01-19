using Sigma.Core.Entities;
using Sigma.Service.Settings.Interfaces;
using Sigma.Repository.Settings.Interfaces;

namespace Sigma.Service.Settings.Services
{

    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _repository;

        public CandidateService(ICandidateRepository repository)
        {
            _repository = repository;
        }

        public async Task AddOrUpdateCandidateAsync(Candidate candidate)
        {
            // Check if the candidate exists by email
            var existingProfile = await _repository.GetByEmailAsync(candidate.Email);

            if (existingProfile == null)
            {
                // If the candidate doesn't exist, add a new one
                await _repository.AddAsync(candidate);
            }
            else
            {
                // If the candidate exists, update the existing candidate profile
                existingProfile.FirstName = candidate.FirstName;
                existingProfile.LastName = candidate.LastName;
                existingProfile.PhoneNumber = candidate.PhoneNumber;
                existingProfile.CallTimeInterval = candidate.CallTimeInterval;
                existingProfile.LinkedInProfileUrl = candidate.LinkedInProfileUrl;
                existingProfile.GitHubProfileUrl = candidate.GitHubProfileUrl;
                existingProfile.Comment = candidate.Comment;

                await _repository.UpdateAsync(existingProfile);
            }

            // Save changes to the database
            await _repository.SaveChangesAsync();
        }
    }
}
