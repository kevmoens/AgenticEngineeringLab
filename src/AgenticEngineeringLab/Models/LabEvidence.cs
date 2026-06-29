namespace AgenticEngineeringLab.Models;

public class LabEvidence
{
    public string ArtifactPath { get; set; } = "";
    public string SummaryPrompt { get; set; } = "";
    public List<string> ReflectionQuestions { get; set; } = new();
}
