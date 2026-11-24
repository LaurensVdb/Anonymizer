using Anonymizer;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var endpoint = builder.Configuration["Ollama:Endpoint"]
    ?? throw new InvalidOperationException("Configuration value 'Ollama:Endpoint' is required.");
var modelName = builder.Configuration["Ollama:ModelName"]
    ?? throw new InvalidOperationException("Configuration value 'Ollama:ModelName' is required.");
var instructions = builder.Configuration["Ollama:Instructions"]
    ?? throw new InvalidOperationException("Configuration value 'Ollama:Instructions' is required.");
var description = builder.Configuration["Ollama:Description"]
    ?? throw new InvalidOperationException("Configuration value 'Ollama:Description' is required.");
var name = builder.Configuration["Ollama:Name"]
    ?? throw new InvalidOperationException("Configuration value 'Ollama:Name' is required.");
var app = builder.Build();



app.MapPost("/anonymize", async ([FromBody] string input) =>
{
    var agent = new AgentClient();
    agent.SetAgentClient(new AgentSettings(
        Endpoint: endpoint,
        ModelName: modelName,
        Instructions: instructions,
        Description: description,
        Name: name
    ));
    var result = await agent.AskAsync(input);
    if (result.Messages.Count > 0)
    {
        return Results.Ok(result.Text);
    }
    else
    {
        return Results.BadRequest("No response from the agent.");
    }
});


app.Run();





