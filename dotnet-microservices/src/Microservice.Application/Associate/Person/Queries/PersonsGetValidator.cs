using FluentValidation;

namespace Microservice.Application
{
    public class PersonsGetValidator : AbstractValidator<PersonsGetQuery>
    {
        public PersonsGetValidator()
        {
            RuleFor(v => v.QueryPredicate).NotEmpty().NotNull();
        }
    }
}