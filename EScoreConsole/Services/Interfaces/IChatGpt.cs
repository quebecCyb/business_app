using EScoreConsole.Metrics;
using OpenAI_API.Completions;

namespace EScoreConsole.Services.Interfaces;

public interface IChatGpt : IAiClient
{
    Task<string> Request(CompletionRequest completionRequest);
}