﻿using SemanticKernelMicroservice.Core.Application.Common.Mappings;
using SemanticKernelMicroservice.Core.Domain.Forecasts.Entities;

namespace SemanticKernelMicroservice.Core.Application.ForecastLists.Queries.GetPaginated;

public class ForecastPaginatedDto : IMapFrom<ForecastsView>
{
    public Guid Key { get; set; }

    public int TemperatureF { get; set; }

    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public string Summary { get; set; }

    public string ZipCodes { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ForecastsView, ForecastPaginatedDto>()
            .ForMember(d => d.Key, opt => opt.MapFrom(s => s.Key))
            .ForMember(d => d.Date, opt => opt.MapFrom(s => s.ForecastDate))
            .ForMember(d => d.TemperatureC, opt => opt.MapFrom(s => s.TemperatureF))
            .ForMember(d => d.Summary, opt => opt.MapFrom(s => s.Summary))
            .ForMember(d => d.ZipCodes, opt => opt.MapFrom(s => s.ZipCodesSearch))
            .ForMember(d => d.ZipCodes, opt => opt.NullSubstitute(""));
    }
}