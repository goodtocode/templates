using FluentValidation;

namespace Microservice.Application
{
    public class BusinessGetValidator : AbstractValidator<BusinessGetQuery>
    {
        public BusinessGetValidator()
        {
            RuleFor(v => v.BusinessKey).NotEmpty().NotNull();
        }
    }
}