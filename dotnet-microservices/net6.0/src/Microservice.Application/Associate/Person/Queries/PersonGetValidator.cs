using FluentValidation;

namespace Microservice.Application
{
    public class PersonGetValidator : AbstractValidator<PersonGetQuery>
    {
        public PersonGetValidator()
        {
            RuleFor(v => v.PersonKey).NotEmpty().NotNull();
        }
    }
}