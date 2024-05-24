using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using WeatherForecasts.Core.Application;
using WeatherForecasts.Infrastructure;
using WeatherForecasts.Infrastructure.Persistence;
using WeatherForecasts.Presentation.WebApi;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]

var builder = WebApplication.CreateBuilder(args);

// Use for B2C
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));
// Use for Azure AD Client Credential Flow
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection(AppConfigurationKeys.AzureAdSectionKey));

builder.Host.UseSerilog((context, config) =>
{
    config.WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day);
});

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebUIServices(builder.Configuration);
//AddKeyVaultConfigurationSettings(builder);
BuildApiVerAndApiExplorer(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Local")
{
    app.UseSwagger();
    UseSwaggerUiConfigs();
    //using var scope = app.Services.CreateScope();
    //var initializer = scope.ServiceProvider.GetRequiredService<WeatherForecastsDbContextInitializer>();
    //await initializer.InitialiseAsync();
    //await initializer.SeedAsync();
}

app.UseRouting();
app.UseStaticFiles();
app.UseHttpsRedirection();
// Enable when auth is setup (builder.Services.AddAuthentication)
//app.UseAuthorization();
//app.UseAuthentication();
app.MapControllers(); 
app.UseCors("AllowOrigin");
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.Run();

void UseSwaggerUiConfigs()
{
    var providers = app.Services.GetService<IApiVersionDescriptionProvider>();

    app.UseSwaggerUI(options =>
    {
        if (providers == null) return;
        foreach (var description in providers.ApiVersionDescriptions)
            options.SwaggerEndpoint($"../swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
    });
}

void BuildApiVerAndApiExplorer(WebApplicationBuilder webApplicationBuilder)
{
    webApplicationBuilder.Services.AddApiVersioning(setup =>
    {
        setup.DefaultApiVersion = new ApiVersion(1, 0);
        setup.AssumeDefaultVersionWhenUnspecified = true;
        setup.ReportApiVersions = true;
    });

    webApplicationBuilder.Services.AddVersionedApiExplorer(setup =>
    {
        setup.GroupNameFormat = "'v'VVV";
        setup.SubstituteApiVersionInUrl = true;
    });
}

void AddKeyVaultConfigurationSettings(WebApplicationBuilder appBuilder)
{
    if (!appBuilder.Configuration.GetValue<bool>("KeyVault:UseKeyVault")) return;

    var azureKeyVaultEndpoint = appBuilder.Configuration["KeyVault:Endpoint"];
    var azureServiceTokenProvider = new AzureServiceTokenProvider();
    var keyVaultClient = new KeyVaultClient(
        new KeyVaultClient.AuthenticationCallback(
            azureServiceTokenProvider.KeyVaultTokenCallback));
    appBuilder.Configuration.AddAzureKeyVault(azureKeyVaultEndpoint, keyVaultClient, new DefaultKeyVaultSecretManager());
}
