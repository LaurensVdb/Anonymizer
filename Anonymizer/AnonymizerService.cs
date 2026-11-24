using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Anonymizer;

public class AnonymizerService(IConfiguration configuration)
{
    public Task<IResult> AnonymizeAsync(string input) =>
        ProcessAsync(input, "AnonymizeText");

    public Task<IResult> AssessTextTrustworthinessAsync(string input) =>
        ProcessAsync(input, "AssessTextTrustworthiness");

    private async Task<IResult> ProcessAsync(string input, string taskName)
    {
        var taskConfigurationPath = $"Ollama:{taskName}";
        var agent = new AgentClient();
        agent.SetAgentClient(new AgentSettings(
            Endpoint: GetRequiredConfigurationValue("Ollama:Endpoint"),
            ModelName: GetRequiredConfigurationValue("Ollama:ModelName"),
            Instructions: GetRequiredConfigurationValue($"{taskConfigurationPath}:Instructions"),
            Description: GetRequiredConfigurationValue($"{taskConfigurationPath}:Description"),
            Name: GetRequiredConfigurationValue($"{taskConfigurationPath}:Name")
        ));

        var result = await agent.AskAsync(input);
        return result.Messages.Count > 0
            ? Results.Ok(result.Text)
            : Results.BadRequest("No response from the agent.");
    }

    private string GetRequiredConfigurationValue(string key) =>
        configuration[key]
        ?? throw new InvalidOperationException($"Configuration value '{key}' is required.");
}
