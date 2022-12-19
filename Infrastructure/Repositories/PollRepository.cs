using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories
{
    public class PollRepository : Repository<Poll>, Core.Repositories.IPollRepository
    {
        public PollRepository(IdentityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
