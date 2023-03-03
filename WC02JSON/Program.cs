
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using WorkflowCore.Persistence;
using WorkflowCore.Persistence.EntityFramework;
using WorkflowCore.Persistence.EntityFramework.Services;
using WorkflowCore.Persistence.PostgreSQL;
using WorkflowCore.Interface;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using WorkflowCore;
using WorkflowCore.Models;
using WorkflowCore.Services.DefinitionStorage;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

IServiceCollection services = new ServiceCollection();
var serviceProvider = services.BuildServiceProvider();
services.AddControllers();

// Load the workflow definition from the JSON file
var json = File.ReadAllText("MyWorkflow.json");
var loader = serviceProvider.GetService<IDefinitionLoader>();
loader.LoadDefinition(json, Deserializers.Json);

// Use PostgreSQL persistence provider
services.AddWorkflow(x => x.UsePostgreSQL(@"Server=127.0.0.1;Port=5432;Database=workflow;User Id=postgres;Password=password;", true, true));

app.MapGet("/", () => "Hello World!");

app.Run();
