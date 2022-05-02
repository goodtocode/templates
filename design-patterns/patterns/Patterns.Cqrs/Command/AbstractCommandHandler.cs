using FluentValidation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodToCode.Templates.Patterns.Cqrs
{
    public abstract class AbstractCommandHandler<TEntity, TValidator> where TEntity : new() where TValidator : AbstractValidator<TEntity>, new()
    {
        private readonly TValidator _validator = new TValidator();

        public AbstractCommandHandler() { }

        public async Task<CommandResponse<TEntity>> Handle(GenericCommand<TEntity> request)
        {
            var result = new CommandResponse<TEntity>() { Errors = GetRequestErrors(request) };

            if (result.Errors.Count == 0)
            {
                try
                {
                    result.Result = await ExecuteCommandAsync(request);
                }
                catch (Exception e)
                {
                    result.ThrownException = e;
                }
            }
            return result;
        }

        protected abstract Task<TEntity> ExecuteCommandAsync(GenericCommand<TEntity> request);

        private List<KeyValuePair<string, string>> GetRequestErrors(GenericCommand<TEntity> request)
        {
            var issues = _validator.Validate(request.Item).Errors;
            var errors = new List<KeyValuePair<string, string>>();

            foreach (var issue in issues)
                errors.Add(new KeyValuePair<string, string>(issue.PropertyName, issue.ErrorMessage));
            return errors;
        }

    }
}
