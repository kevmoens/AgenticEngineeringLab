namespace AgenticEngineeringLab.Models;

public class ExpectedArtifact
{
    public string Path { get; set; } = "";
    public List<string> ExpectedContents { get; set; } = new();
}
