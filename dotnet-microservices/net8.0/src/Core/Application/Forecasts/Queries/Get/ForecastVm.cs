﻿using WeatherForecasts.Core.Application.Common.Mappings;
using WeatherForecasts.Core.Domain.Forecasts.Models;

namespace WeatherForecasts.Core.Application.Forecasts.Queries.Get;

public class ForecastVm : IMapFrom<ForecastsView>
{
    public Guid Key { get; set; }

    public int TemperatureF { get; set; }

    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public string Summary { get; set; }

    public string PostalCodes { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ForecastsView, ForecastVm>()
            .ForMember(d => d.Key, opt => opt.MapFrom(s => s.Key))
            .ForMember(d => d.Date, opt => opt.MapFrom(s => s.ForecastDate))
            .ForMember(d => d.TemperatureF, opt => opt.MapFrom(s => s.TemperatureF))
            .ForMember(d => d.Summary, opt => opt.MapFrom(s => s.Summary))
            .ForMember(d => d.PostalCodes, opt => opt.MapFrom(s => s.PostalCodesSearch))
            .ForMember(d => d.PostalCodes, opt => opt.NullSubstitute(""));
    }
}