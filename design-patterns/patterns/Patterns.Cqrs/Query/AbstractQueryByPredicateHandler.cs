using FluentValidation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Patterns.Cqrs
{
    public abstract class AbstractQueryByPredicateHandler<TEntity, TValidator> where TEntity : new() where TValidator : AbstractValidator<Func<TEntity, bool>>, new()
    {
        private readonly TValidator _validator = new TValidator();

        public AbstractQueryByPredicateHandler() { }

        public async Task<QueryResponse<TEntity>> Handle(GenericQueryByPredicate<TEntity> request)
        {
            var result = new QueryResponse<TEntity>() { Errors = GetRequestErrors(request) };

            if (result.Errors.Count == 0)
            {
                try
                {
                    result.Result = await ExecuteQueryAsync(request);
                }
                catch (Exception e)
                {
                    result.ThrownException = e;
                }
            }
            return result;
        }

        protected abstract Task<IEnumerable<TEntity>> ExecuteQueryAsync(GenericQueryByPredicate<TEntity> request);

        private List<KeyValuePair<string, string>> GetRequestErrors(GenericQueryByPredicate<TEntity> request)
        {
            var errors = new List<KeyValuePair<string, string>>();
            var issues = _validator.Validate(request.Predicate).Errors;

            foreach (var issue in issues)
                errors.Add(new KeyValuePair<string, string>(issue.PropertyName, issue.ErrorMessage));
            return errors;
        }
    }
}
