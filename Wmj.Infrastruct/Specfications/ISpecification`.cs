using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Wmj.Infrastruct.Specifications
{
    public interface ISpecification<T> where T : class
    {
        Expression<Func<T, bool>> WhereExpression { get; set; }
        IEnumerable<Expression<Func<T, object>>> IncludeExpression { get; set; }
        int? Take { get; set; }
        int? Skip { get; set; }
        Expression<Func<T, object>> OrderByExpression { get; set; }
        Expression<Func<T, object>> OrderByDescExpression { get; set; }
        IEnumerable<Expression<Func<T, string>>> IncludeString { get; set; }
    }
}
