using Sigma.Core.Entities;
using Sigma.Service.Settings.Interfaces;
using Sigma.Repository.Settings.Interfaces;
using Sigma.Repository.Settings.Repositories;

namespace Sigma.Service.Settings.Services
{

    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _repository;

        public CandidateService(ICandidateRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddOrUpdateCandidateAsync(Candidate candidate)
        {
            try
            {
                // Check if the candidate exists by email
                var existingCandidate = await _repository.GetByEmailAsync(candidate.Email);

                if (existingCandidate == null)
                {
                    // If the candidate doesn't exist, add a new one
                    await _repository.AddAsync(candidate);
                }
                else
                {
                    // If the candidate exists, update the existing candidate profile
                    existingCandidate.FirstName = candidate.FirstName;
                    existingCandidate.LastName = candidate.LastName;
                    existingCandidate.PhoneNumber = candidate.PhoneNumber;
                    existingCandidate.CallTimeInterval = candidate.CallTimeInterval;
                    existingCandidate.LinkedInProfileUrl = candidate.LinkedInProfileUrl;
                    existingCandidate.GitHubProfileUrl = candidate.GitHubProfileUrl;
                    existingCandidate.Comment = candidate.Comment;

                    await _repository.UpdateAsync(existingCandidate);
                   
                }
                // Save changes to the database
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }

        public async Task<IEnumerable<Candidate>> GetAllCandidateAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Candidate> GetCandidateByEmailAsync(string email)
        {
            return await _repository.GetByEmailAsync(email);
        }
    }
}
