using System.Linq.Expressions;

namespace Core.Specifications.Base
{
    public abstract class BaseSpecification<T> : ISpecification<T> where T : class
    {
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();
        public bool IgnoreQueryFilter { get; set; }

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }

        protected virtual void SetIgnoreQueryFilter(bool ignoreQueryFilterValue)
        {
            IgnoreQueryFilter = ignoreQueryFilterValue;
        }
    }
}
