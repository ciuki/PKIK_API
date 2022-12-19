using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories
{
    public class VoteRepository : Repository<Vote>, Core.Repositories.IVoteRepository
    {
        public VoteRepository(IdentityDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<Vote> AddWithoutSavingAsync(Vote entity)
        {
            await _dbContext.Set<Vote>().AddAsync(entity);
            return entity;
        }
    }
}
