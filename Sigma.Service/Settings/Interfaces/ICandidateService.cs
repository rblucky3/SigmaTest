using Sigma.Core.Entities;

namespace Sigma.Service.Settings.Interfaces
{
    public interface ICandidateService
    {
        Task<bool> AddOrUpdateCandidateAsync(Candidate candidate);
        Task<IEnumerable<Candidate>> GetAllCandidateAsync();
        Task<Candidate> GetCandidateByEmailAsync(string email);
    }
}
