using System.Linq.Expressions;

namespace Store.Repository.Specification
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification(Expression<Func<T, bool>> criteria) {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public string OrderByDir { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        => Includes.Add(includeExpression);
        protected void AddOrderBy(Expression<Func<T, object>> orderBy)
            => OrderBy = orderBy;
        protected void AddOrderByDir(string orderByDir = "ASC")
            => OrderByDir = orderByDir;
        protected void ApplyPagination(int skip,int take)
        {
            Skip = skip;
            Take = take;
            IsPaginated = true;
        }
    }
}
