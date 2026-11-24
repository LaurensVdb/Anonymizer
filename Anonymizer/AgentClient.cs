using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OllamaSharp;
namespace Anonymizer;

public record AgentSettings(string Endpoint, string ModelName, string Instructions, string Description, string Name);
public class AgentClient
{
    private ChatClientAgent? _ollamaApiClient;
    public void SetAgentClient(AgentSettings agentSettings)
    {
        _ollamaApiClient = new OllamaApiClient(
            new Uri(agentSettings.Endpoint),
            agentSettings.ModelName).AsAIAgent(
                name: agentSettings.Name,
                description: agentSettings.Description,
                instructions: agentSettings.Instructions
            );
        
    }

    public async Task<AgentResponse> AskAsync(string input)
    {
        if (_ollamaApiClient == null)
        {
            throw new InvalidOperationException("Agent client is not set. Call SetAgentClient first.");
        }

        return await _ollamaApiClient.RunAsync(input);
    }


}
