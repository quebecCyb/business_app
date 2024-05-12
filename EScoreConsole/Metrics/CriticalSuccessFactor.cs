namespace EScoreConsole.Metrics;

public class CriticalSuccessFactor
{
    public CriticalSuccessFactor(int swotComponentId, string content)
    {
        SwotComponentId = swotComponentId;
        Content = content;
    }

    public string Content { get; set; }
    public int SwotComponentId { get; }
}