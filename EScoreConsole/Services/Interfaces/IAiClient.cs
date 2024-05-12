using EScoreConsole.Metrics;
using EScoreConsole.Metrics.Helpers;
using EScoreConsole.Schemas;

namespace EScoreConsole.Services.Interfaces;

public interface IAiClient
{
    public Task<string> Request(string request);
    public Task<string> SwotToCsf(SwotComponent request);

    public Task<string> SwotToStrategy(Swot swot, SwotType type);

    public Task<string> SwotToMission(Swot swot);
    public Task<string> SwotToVision(Swot swot);
}
