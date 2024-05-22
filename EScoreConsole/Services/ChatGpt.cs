using System.Runtime.InteropServices;
using EScoreConsole.Metrics;
using EScoreConsole.Metrics.Helpers;
using EScoreConsole.Schemas;
using EScoreConsole.Services.Interfaces;
using OpenAI_API;
using OpenAI_API.Completions;

namespace EScoreConsole.Services;

public class ChatGpt : IChatGpt, IExternalApi
{
    private readonly OpenAIAPI _client;
    private readonly IConfiguration _configuration;

    private readonly string? Key; 
    
    public ChatGpt(IConfiguration configuration)
    {
        _configuration = configuration;
        APIAuthentication apiAuthentication = new APIAuthentication(Key);
        _client = new OpenAIAPI(apiAuthentication);

        Key = configuration["AppSettings:GptKey"];
    }
    
    public async Task<string> Request(CompletionRequest completionRequest)
    {
        var completionResult = await _client.Completions.CreateCompletionAsync(completionRequest);
        if (completionResult.Completions.Count == 0 || completionResult.Completions[0].Text is null)
        {
            throw new ExternalException("ChatGpt request failed");
        }

        return completionResult.Completions[0].Text;
    }

    public Task<string> Request(string request)
    {
        return Request(new CompletionRequest { Model = "gpt-3.5-turbo-instruct", MaxTokens = 150, Prompt = request });
    }

    public Task<string> SwotToCsf(SwotComponent request)
    {
        return Request($"You should map this SWOT component '{request.Content}' to corresponding critical success factor");
    }

    public Task<string> SwotToStrategy(Swot swot, SwotType type)
    {
        IEnumerable<SwotComponent> components = swot.Components.FindAll( e => e.Type == type);
        
        return Request($"You should map this SWOT components '{string.Join("; ", components)}' of type {type} to corresponding strategy objectives");
    }

    public Task<string> SwotToMission(Swot swot)
    {
        return Request($"You should map this SWOT components '{string.Join("; ", swot.Components)}' to corresponding company`s mission statement");
    }

    public Task<string> SwotToVision(Swot swot)
    {
        return Request($"You should map this SWOT components '{string.Join("; ", swot.Components)}' to corresponding company`s future vision");
    }
}