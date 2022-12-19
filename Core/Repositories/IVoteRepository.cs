using Core.Entities;
using Core.Repositories.Base;

namespace Core.Repositories
{
    public interface IVoteRepository : IRepository<Vote>
    {
        Task<Vote> AddWithoutSavingAsync(Vote entity);
    }
}
