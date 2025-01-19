using Sigma.Data;
using Sigma.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Sigma.Repository.Settings.Interfaces;

namespace Sigma.Repository.Settings.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {

        private readonly ApplicationDbContext _context;

        public CandidateRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Candidate>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<Candidate> GetByEmailAsync(string email)
        {
            return await _context.Candidates.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task AddAsync(Candidate candidate)
        {
            await _context.Candidates.AddAsync(candidate);
        }

        public async Task UpdateAsync(Candidate candidate)
        {
            _context.Candidates.Update(candidate);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

       
    }
}
