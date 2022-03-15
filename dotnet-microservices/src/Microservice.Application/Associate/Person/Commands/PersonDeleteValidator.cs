﻿using FluentValidation;
using System;

namespace Microservice.Application
{
    public class PersonDeleteValidator : AbstractValidator<Guid>
    {
        public PersonDeleteValidator()
        {
            RuleFor(v => v).NotEqual(Guid.Empty);
            //RuleFor(v => v.ExamType).NotEmpty().NotNull().Equal("microcredential").MaximumLength(25);
            //RuleFor(v => v.CustomerKey).NotEmpty().GreaterThanOrEqualTo(1);
            //RuleFor(v => v.ProgramKey).NotEmpty().GreaterThanOrEqualTo(1);
            //RuleFor(v => v.ExamKey).NotEmpty().GreaterThanOrEqualTo(1);
            //RuleFor(v => v.Attempts).NotEmpty().GreaterThanOrEqualTo(1);
            //RuleFor(v => v.QuestionsCorrect).GreaterThanOrEqualTo(1);
            //RuleFor(v => v.QuestionsCount).NotEmpty().GreaterThanOrEqualTo(1);
            //RuleFor(v => v.ExamTakenDateTime).NotEmpty().GreaterThanOrEqualTo(1);
            //RuleFor(v => v.Status).NotNull().NotEmpty().MaximumLength(15)
            //    .When(v => v.Status != "Pass" || v.Status != "Fail")
            //    .WithMessage($"Status should be either 'Pass' or 'Fail'");

            //RuleFor(v => v.QuestionsCorrect).NotNull().NotEmpty()
            //    .When(v => v.QuestionsCorrect > v.QuestionsCount)
            //    .WithMessage($"Questions Correct cannot be greater than Questions Count");
        }
    }
}