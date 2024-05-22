using EScoreConsole.Metrics;
using EScoreConsole.Schemas;

namespace EScoreConsole.Services.Interfaces;

public interface IExecScore
{
    List<CriticalSuccessFactor> Csf { get; }
    Dictionary<SwotType, StrategyObjective> Strategy { get; }
    string Mission { get; set; } 
    string Vision { get; set; } 

    Task SwotToCsf();
    Task SwotToVision();
    Task SwotToMission();
    Task SwotToStrategy();
    
    void AddSwotComponent(SwotType type, string content);
}