using EScoreConsole.Schemas;

namespace EScoreConsole.Metrics;

public class StrategyObjective
{
    public StrategyObjective(string content, SwotType type)
    {
        Content = content;
        Type = type;
    }

    public string Content { get; set; }
    public SwotType Type { get; set; }

}