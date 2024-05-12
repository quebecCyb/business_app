using EScoreConsole.Metrics;
using EScoreConsole.Metrics.Helpers;
using EScoreConsole.Schemas;
using EScoreConsole.Services.Interfaces;

namespace EScoreConsole.Services;

public class ExecScore : IExecScore
{
    private Swot Swot { get; set; } = new();
    private readonly List<CriticalSuccessFactor> _csf = new();
    private readonly Dictionary<SwotType, StrategyObjective> _strategy = new();
    private readonly IAiClient _aiClient;
    
    public string Mission { get; set; } 
    public string Vision { get; set; } 


    public ExecScore(IAiClient aiClient)
    {
        _aiClient = aiClient;
    }
    
    
    
    public async Task SwotToCsf()
    {
        for (int i = 0; i < Swot.Components.Count; i++)
        {
            var content = await _aiClient.SwotToCsf(Swot.Components[i]);
            _csf.Add(new CriticalSuccessFactor(i, content));
        }
    }
    
    public async Task SwotToStrategy()
    {
        foreach (SwotType type in Enum.GetValues(typeof(SwotType)))
        {
            _strategy[type] = new StrategyObjective(await _aiClient.SwotToStrategy(Swot, type), type);
        }
    }

    public void AddSwotComponent(SwotType type, string content)
    {
        Swot.Components.Add(new SwotComponent { Type = type, Content = content });
    }

    public async Task SwotToMission()
    {
        Mission = await _aiClient.SwotToMission(Swot);
    }
    
    public async Task SwotToVision()
    {
        Vision = await _aiClient.SwotToVision(Swot);
    }
}