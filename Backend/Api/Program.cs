using Abstractions;
using Api.Extesions;
using Api.Http;
using Api.Middlewares;
using Api.StartupConfigurations;
using Api.StartupConfigurations.Options;
using Application;
using Domain;
using Infrastructure;
using Keycloak;
using Keycloak.Configurations;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.RegisterKeycloakServices();
builder.Services.RegisterDataAccessService(builder.Configuration);
builder.Services.RegisterUseCasesService();
builder.Services.RegisterInfrastructureServices();

builder.Services.ConfigureOptions<KeycloakConfigurationSetup>();
builder.Services.ConfigureOptions<KeycloakScopesConfigurationSetup>();

builder.Services.AddKeycloakConfiguration();
builder.Services.AddMinioConfiguration(builder.Configuration);

builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new UlidJsonConverter());
});

builder.Services.ConfigureSwagger();

builder.Services.AddScoped<ICurrentHttpContextAccessor, CurrentHttpContextAccessor>();

var app = builder.Build();

app.SetConnectionStringEnvironment(app.Configuration.GetConnectionString("DbConnection"));

app.MapControllers();

app.MigrateDb();

app.ConfigureSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<ContextSetterMiddleware>();

app.Run();
