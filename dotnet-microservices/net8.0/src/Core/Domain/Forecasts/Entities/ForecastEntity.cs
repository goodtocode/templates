namespace WeatherForecasts.Core.Domain.Forecasts.Entities;

public class ForecastEntity : Common.Entity
{
    protected ForecastEntity() { }

    public ForecastEntity(ForecastValue weatherForecastAddValue) : base(weatherForecastAddValue.Key)
    {
        ForecastDate = weatherForecastAddValue.Date.ToUniversalTime();
        TemperatureF = weatherForecastAddValue.TemperatureF;
        Summary = SummaryFValue().ToString();
        PostalCodes = new List<ForecastPostalCodeEntity>();
        foreach (var code in weatherForecastAddValue.PostalCodes)
            PostalCodes.Add(new ForecastPostalCodeEntity(code, this));
        PopulatePostalCodeSearch();
        DateAdded = DateTime.UtcNow;
    }

    public int TemperatureF { get; private set; }

    public int TemperatureC => 32 + (int)(TemperatureF / 0.5556);

    public string Summary { get; private set; }

    public string PostalCodesSearch { get; private set; }

    public virtual List<ForecastPostalCodeEntity> PostalCodes { get; } = new();

    public DateTime ForecastDate { get; private set; }

    public DateTime DateAdded { get; private set; }

    public DateTime? DateUpdated { get; private set; }

    public void AddPostalCode(int postalCode)
    {
        if (PostalCodes.Any(x => x.PostalCode == postalCode)) return;
        PostalCodes.Add(new ForecastPostalCodeEntity(postalCode, this));
        PopulatePostalCodeSearch();
        DateUpdated = DateTime.Now;
    }

    public void RemovePostalCode(int postalCode)
    {
        var existing = PostalCodes.FirstOrDefault(x => x.PostalCode == postalCode);
        if (existing == null) return;
        PostalCodes.Remove(existing);
        PopulatePostalCodeSearch();
        DateUpdated = DateTime.Now;
    }

    public Result UpdateDate(DateTime dateTime)
    {
        if (dateTime == DateTime.MinValue)
            return Result.Failure("Date cannot be minimum value");
        ForecastDate = dateTime;
        DateUpdated = DateTime.UtcNow;
        return Result.Success();
    }

    public void UpdateTemperatureF(int temperatureF)
    {
        TemperatureF = temperatureF;
        DateUpdated = DateTime.UtcNow;
    }

    private SummaryType SummaryFValue()
    {
        return TemperatureF switch
        {
            <= 32 => SummaryType.Freezing,
            > 32 and <= 40 => SummaryType.Bracing,
            > 40 and <= 50 => SummaryType.Chilly,
            > 50 and <= 60 => SummaryType.Cool,
            > 60 and <= 70 => SummaryType.Mild,
            > 70 and <= 80 => SummaryType.Warm,
            > 80 and <= 90 => SummaryType.Balmy,
            > 90 and <= 100 => SummaryType.Hot,
            > 100 and <= 110 => SummaryType.Sweltering,
            > 110 => SummaryType.Scorching
        };
    }

    private void PopulatePostalCodeSearch()
    {
        PostalCodesSearch = string.Join(", ", PostalCodes.Select(x => x.PostalCode));
    }

    public void UpdatePostalCodes(List<int> requestCodes)
    {
        PostalCodes.Clear();
        foreach (var code in requestCodes)
            PostalCodes.Add(new ForecastPostalCodeEntity(code, this));
        DateUpdated = DateTime.Now;
        PopulatePostalCodeSearch();
    }

    public enum SummaryType
    {
        Freezing,
        Bracing,
        Chilly,
        Cool,
        Mild,
        Warm,
        Balmy,
        Hot,
        Sweltering,
        Scorching
    }
}