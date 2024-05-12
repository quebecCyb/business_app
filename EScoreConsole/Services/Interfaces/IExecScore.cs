using EScoreConsole.Metrics;
using EScoreConsole.Schemas;

namespace EScoreConsole.Services.Interfaces;

public interface IExecScore
{
    Task SwotToCsf();
    Task SwotToVision();
    Task SwotToMission();
    Task SwotToStrategy();
    
    void AddSwotComponent(SwotType type, string content);
}