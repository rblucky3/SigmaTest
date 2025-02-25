﻿using Sigma.Core.Entities;

namespace Sigma.Repository.Settings.Interfaces
{
    public interface ICandidateRepository
    {
        Task<IEnumerable<Candidate>> GetAllAsync();
        Task<Candidate> GetByEmailAsync(string email);
        Task AddAsync(Candidate candidate);
        Task UpdateAsync(Candidate candidate);
        Task SaveChangesAsync();
    }
}
