using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, Core.Repositories.IUserRepository
    {
        public UserRepository(IdentityDbContext dbContext) : base(dbContext)
        {
        }
    }
}
