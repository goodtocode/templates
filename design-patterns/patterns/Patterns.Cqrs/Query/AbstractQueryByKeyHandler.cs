using FluentValidation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Patterns.Cqrs
{
    public abstract class AbstractQueryByKeyHandler<TEntity, TValidator> where TEntity : new() where TValidator : AbstractValidator<Guid>, new()
    {
        private readonly TValidator _validator = new TValidator();

        public AbstractQueryByKeyHandler() { }

        public async Task<QueryResponse<TEntity>> Handle(GenericQueryByKey request)
        {
            var result = new QueryResponse<TEntity>() { Errors = GetRequestErrors(request) };

            if (result.Errors.Count == 0)
            {
                try
                {
                    result.Result = new TEntity[] { await ExecuteQueryAsync(request) };
                }
                catch (Exception e)
                {
                    result.ThrownException = e;
                }
            }
            return result;
        }

        protected abstract Task<TEntity> ExecuteQueryAsync(GenericQueryByKey request);

        private List<KeyValuePair<string, string>> GetRequestErrors(GenericQueryByKey request)
        {
            var errors = new List<KeyValuePair<string, string>>();
            var issues = _validator.Validate(request.Key).Errors;

            foreach (var issue in issues)
                errors.Add(new KeyValuePair<string, string>(issue.PropertyName, issue.ErrorMessage));
            return errors;
        }

    }
}
