using System.Linq.Expressions;
using Wmj.Infrastruct.Specifications;

namespace Wmj.Infrastruct.Specfications
{
    public class Specification<T> : ISpecification<T> where T : class
    {
        public Expression<Func<T, bool>>? WhereExpression { get; set; } 
        public IEnumerable<Expression<Func<T, object>>> IncludeExpression { get; set; } = new List<Expression<Func<T, object>>>();
        public IEnumerable<Expression<Func<T,string>>> IncludeString { get; set; } = new List<Expression<Func<T,string>>>();
        public int? Take { get; set; } = null;
        public int? Skip { get; set; } = null;
        public Expression<Func<T, object>>? OrderByExpression { get; set; } = null;
        public Expression<Func<T, object>>? OrderByDescExpression { get; set; } = null;
    }
}
