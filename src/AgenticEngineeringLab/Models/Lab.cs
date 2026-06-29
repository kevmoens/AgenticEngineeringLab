namespace AgenticEngineeringLab.Models;

public class Lab
{
    public string Title { get; set; } = "";
    public SafetyLevel SafetyLevel { get; set; }
    public string SafetyDescription { get; set; } = "";
    public List<string> BeforeYouStart { get; set; } = new();
    public string Details { get; set; } = "";
    public List<PromptCard> Prompts { get; set; } = new();
    public ExpectedArtifact ExpectedArtifact { get; set; } = new();
    public List<string> VerificationChecklist { get; set; } = new();
    public LabEvidence Evidence { get; set; } = new();
}
