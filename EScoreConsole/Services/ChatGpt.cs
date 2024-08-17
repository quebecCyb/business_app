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

    public ChatGpt(IConfiguration configuration)
    {
        _configuration = configuration;
        var key = configuration["AppSettings:GptKey"];

        APIAuthentication apiAuthentication = new APIAuthentication(key);
        _client = new OpenAIAPI(apiAuthentication);

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
        return Request($"You should map this SWOT component  ###{request.Content}### of this type '''{request.Type}''' WHERE 'Strength = 0,   Weakness = 1,   Opportunity = 2,    Threat = 3' to corresponding critical success factor. Make critical success factor in one or two sentences MAXIMUM.");
    }

    public Task<string> SwotToStrategy(Swot swot, SwotType type)
    {
        IEnumerable<SwotComponent> components = swot.Components.FindAll( e => e.Type == type);
        
        return Request($"You should map this SWOT components '{string.Join("; ", components)}' of type {type} to corresponding strategy objective. VERY SHORT. Try to use not more 50 words to describe main objectives briefly.");
    }

    public Task<string> SwotToMission(Swot swot)
    {
        return Request($"You should map this SWOT components '{string.Join("; ", swot.Components)}' to corresponding company`s mission statement. VERY SHORT. Try to use not more 100 words to describe main missions briefly.");
    }

    public Task<string> SwotToVision(Swot swot)
    {
        return Request($"You should map this SWOT components '{string.Join("; ", swot.Components)}' to corresponding company`s future vision.");
    }

    public Task<string> CsfToChart(Swot swot, string csf)
    {
        return Request($"""
                        You should estimate (from 0 to 5) corresponding company`s key performance indicators (from list below) using this SWOT components '{string.Join("; ", swot.Components)}' AND this critical success factors: '{csf}'. This KPI:
                        
                        Steering Processes:
                        
                        ROP Quality - SSL
                        ROP Quality - VLU
                        Quality conformity
                        Vacancies
                        HSE frequency
                        Delivery Processes:
                        
                        Schedule Performance Index
                        Strategic Float - Signalling
                        Strategic Float - Trains
                        Critical Path Analysis
                        Design Register
                        Financial Results:
                        
                        Cash Received - VLU
                        Cash Received - SSL
                        NOPAT evolution - LUPD
                        E-A-C Gross Margin evolution - VLU
                        E-A-C Gross Margin evolution - SSL
                        E-A-C Risk & Contingency - VLU
                        E-A-C Risk & Contingency - SSL
                        JTC - VLU
                        Customer Satisfaction:
                        
                        Ambience - VLU
                        Availability (LCH) - VLU
                        Customer Responsiveness
                        Customer Satisfaction - VLU
                        Customer Satisfaction - SSL
                        
                        """);
        
    }

}