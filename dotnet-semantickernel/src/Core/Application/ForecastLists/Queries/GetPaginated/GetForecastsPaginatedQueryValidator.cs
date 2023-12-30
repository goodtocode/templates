﻿namespace dotnet_semantickernel.Core.Application.ForecastLists.Queries.GetPaginated;

public class GetForecastsPaginatedQueryValidator : AbstractValidator<GetForecastsPaginatedQuery>
{
    public GetForecastsPaginatedQueryValidator()
    {
        RuleFor(x => x.PageNumber).NotEqual(0);
        RuleFor(x => x.PageSize).NotEqual(0);
    }
}