using Microsoft.Extensions.Logging.Abstractions;
using WorkflowCore.Interface;
using WorkflowCore.Services.DefinitionStorage;
using WC01HelloWorld.Steps;
using WorkflowCore.Services;

using Microsoft.Extensions.DependencyInjection;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Services.DefinitionStorage;
using WorkflowCore.Services.Middleware;

// Create a new service collection and add the required Workflow Core services
IServiceCollection services = new ServiceCollection()
    .AddLogging()
    .AddWorkflow()
    .AddSingleton<IDefinitionStorageProvider>(new MemoryDefinitionStorageProvider(workflowDefinitions));

// Configure and build a WorkflowHost instance using IWorkflowHostBuilder
IWorkflowHostBuilder hostBuilder = services.BuildServiceProvider().GetRequiredService<IWorkflowHostBuilder>();
hostBuilder
    .UseConsoleLifetime()
    .UseMemoryCache()
    .UseMemoryPersistence()
    .RegisterWorkflow<MyWorkflow>()
    .UseWorkflowMiddleware(new WorkflowMiddleware(hostBuilder.Services.GetService<IDefinitionLoader>(), hostBuilder.Services.GetService<IWorkflowRegistry>(), hostBuilder.Services.GetService<ISerializer>(), hostBuilder.Services.GetService<IWorkflowErrorHandler>()));
    
// Build and start the WorkflowHost instance
var host = hostBuilder.Build();
await host.StartAsync();



var serviceProvider = new ServiceCollection()
    .AddWorkflow()
    .BuildServiceProvider();

// 
var json = File.ReadAllText("MyWorkflow.json");
var loader = serviceProvider.GetService<IDefinitionLoader>();
var workflowDefinition = loader.LoadDefinition(json, Deserializers.Json);

var host = new WorkflowHost(serviceProvider, new NullLogger<WorkflowHost>());
await host.StartAsync();

var workflowId = await host.StartWorkflowAsync(workflowDefinition.Id, workflowDefinition.Version, workflowDefinition.Steps);

await host.StopAsync();

//start the workflow host
// var host = serviceProvider.GetService<IWorkflowHost>();
// host.RegisterWorkflow<HelloWorldWorkflow>();        
// host.Start();            
//
// host.StartWorkflow("HelloWorld");
//             
// Console.ReadLine();
// host.Stop();

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
