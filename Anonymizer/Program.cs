using Anonymizer;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var endpoint = builder.Configuration["Ollama:Endpoint"]
    ?? throw new InvalidOperationException("Configuration value 'Ollama:Endpoint' is required.");
var modelName = builder.Configuration["Ollama:ModelName"]
    ?? throw new InvalidOperationException("Configuration value 'Ollama:ModelName' is required.");
var instructions = builder.Configuration["Ollama:AnonymizeText:Instructions"]
    ?? throw new InvalidOperationException("Configuration value 'Ollama:AnonymizeText:Instructions' is required.");
var description = builder.Configuration["Ollama:AnonymizeText:Description"]
    ?? throw new InvalidOperationException("Configuration value 'Ollama:AnonymizeText:Description' is required.");
var name = builder.Configuration["Ollama:AnonymizeText:Name"]
    ?? throw new InvalidOperationException("Configuration value 'Ollama:AnonymizeText:Name' is required.");
var trustworthinessInstructions = builder.Configuration["Ollama:AssessTextTrustworthiness:Instructions"]
    ?? throw new InvalidOperationException("Configuration value 'Ollama:AssessTextTrustworthiness:Instructions' is required.");
var trustworthinessDescription = builder.Configuration["Ollama:AssessTextTrustworthiness:Description"]
    ?? throw new InvalidOperationException("Configuration value 'Ollama:AssessTextTrustworthiness:Description' is required.");
var trustworthinessName = builder.Configuration["Ollama:AssessTextTrustworthiness:Name"]
    ?? throw new InvalidOperationException("Configuration value 'Ollama:AssessTextTrustworthiness:Name' is required.");
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

app.MapPost("/assess-text-trustworthiness", async ([FromBody] string input) =>
{
    var agent = new AgentClient();
    agent.SetAgentClient(new AgentSettings(
        Endpoint: endpoint,
        ModelName: modelName,
        Instructions: trustworthinessInstructions,
        Description: trustworthinessDescription,
        Name: trustworthinessName
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





