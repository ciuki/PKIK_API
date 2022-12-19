using Core.Specifications.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Base
{
    public class Repository<T> : Core.Repositories.Base.IRepository<T> where T : class
    {
        protected readonly Data.IdentityDbContext _dbContext;

        public Repository(Data.IdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllBySpecAsync(ISpecification<T> spec)
        {
            IQueryable<T> queryableResultWithIncludes = spec.Includes
                  .Aggregate(_dbContext.Set<T>().AsQueryable(),
                      (current, include) => current.Include(include));

            IQueryable<T> secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            if (spec.IgnoreQueryFilter)
            {
                return await secondaryResult
                            .IgnoreQueryFilters()
                            .Where(spec.Criteria).ToListAsync();
            }
            return await secondaryResult
                            .Where(spec.Criteria).ToListAsync();
        }

        public async Task<T> GetBySpecAsync(ISpecification<T> spec)
        {
            IQueryable<T> queryableResultWithIncludes = spec.Includes
                  .Aggregate(_dbContext.Set<T>().AsQueryable(),
                      (current, include) => current.Include(include));

            IQueryable<T> secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            if (spec.IgnoreQueryFilter)
            {
                return await secondaryResult
                            .IgnoreQueryFilters()
                            .Where(spec.Criteria)
                            .SingleOrDefaultAsync();
            }
            return await secondaryResult
                            .Where(spec.Criteria)
                            .SingleOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> GetByLambdaAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
