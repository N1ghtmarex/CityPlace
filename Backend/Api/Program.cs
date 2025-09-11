using Api.Extesions;
using Api.Middlewares;
using Api.StartupConfigurations;
using Api.StartupConfigurations.Options;
using Application;
using Domain;
using Keycloak;
using Keycloak.Configurations;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.RegisterKeycloakServices();
builder.Services.RegisterDataAccessService(builder.Configuration);
builder.Services.RegisterUseCasesService();

builder.Services.ConfigureOptions<KeycloakConfigurationSetup>();
builder.Services.ConfigureOptions<KeycloakScopesConfigurationSetup>();

builder.Services.AddKeycloakConfiguration();

builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new UlidJsonConverter());
});

builder.Services.ConfigureSwagger();

var app = builder.Build();

app.SetConnectionStringEnvironment(app.Configuration.GetConnectionString("DbConnection"));

app.MapControllers();

app.MigrateDb();

app.ConfigureSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.Run();
