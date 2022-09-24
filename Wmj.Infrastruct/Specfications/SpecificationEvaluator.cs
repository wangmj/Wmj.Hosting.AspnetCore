using Microsoft.EntityFrameworkCore;

namespace Wmj.Infrastruct.Specfications
{
    public class SpecificationEvaluator<T> where T : class
    {
        public static void GetQuery(IQueryable<T> query, Specification<T> specification)
        {
            if (specification.WhereExpression != null)
                query.Where(specification.WhereExpression);

            query = specification.IncludeExpression.Aggregate(query, (total, current) => total.Include(current));

            query = specification.IncludeString.Aggregate(query, (total, current) => total.Include(current));

            if (specification.OrderByExpression != null)
                query = query.OrderBy(specification.OrderByExpression);

            if (specification.OrderByDescExpression != null)
                query = query.OrderByDescending(specification.OrderByDescExpression);

            if (specification.Skip.HasValue)
                query.Skip(specification.Skip.Value);
            
            if (specification.Take.HasValue)
                query.Take(specification.Take.Value);
        }
    }
}
