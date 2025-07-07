using Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterDataAccessService(builder.Configuration);

var app = builder.Build();

app.SetConnectionStringEnvironment(app.Configuration.GetConnectionString("DbConnection"));

app.MigrateDb();

app.Run();
