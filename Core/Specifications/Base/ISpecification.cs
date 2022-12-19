using System.Linq.Expressions;

namespace Core.Specifications.Base
{
    public interface ISpecification<T> where T : class
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        bool IgnoreQueryFilter { get; set; }
    }
}
