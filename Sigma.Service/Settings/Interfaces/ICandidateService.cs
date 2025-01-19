using Sigma.Core.Entities;

namespace Sigma.Service.Settings.Interfaces
{
    public interface ICandidateService
    {
        Task AddOrUpdateCandidateAsync(Candidate candidate);
    }
}
