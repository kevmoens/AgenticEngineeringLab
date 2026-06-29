using System.Text.Json.Serialization;
using AgenticEngineeringLab.Models.Converters;

namespace AgenticEngineeringLab.Models;

public class Module
{
    public string Id { get; set; } = "";
    public int ModuleNumber { get; set; }
    public string Title { get; set; } = "";
    public string ShortTitle { get; set; } = "";
    public string Course { get; set; } = "";
    public string AppName { get; set; } = "";
    public int EstimatedMinutes { get; set; }
    public bool RepoRequired { get; set; }
    public bool SourceCodeChangesAllowed { get; set; }
    public SafetyLevel SafetyLevel { get; set; }
    public string? PrimaryArtifact { get; set; }
    public List<string> Prerequisites { get; set; } = new();
    public string ModuleType { get; set; } = "";
    public string Status { get; set; } = "";

    [JsonConverter(typeof(MarkdownLinesConverter))]
    public string Summary { get; set; } = "";

    [JsonConverter(typeof(MarkdownLinesConverter))]
    public string WhyItMatters { get; set; } = "";

    [JsonConverter(typeof(MarkdownLinesConverter))]
    public string HowItFits { get; set; } = "";

    public List<string> LearningObjectives { get; set; } = new();

    [JsonConverter(typeof(MarkdownLinesConverter))]
    public string KeyConceptsMarkdown { get; set; } = "";

    [JsonConverter(typeof(MarkdownLinesConverter))]
    public string MechanicsMarkdown { get; set; } = "";

    [JsonConverter(typeof(MarkdownLinesConverter))]
    public string VariationNotesMarkdown { get; set; } = "";

    [JsonConverter(typeof(MarkdownLinesConverter))]
    public string InstructorDemoMarkdown { get; set; } = "";

    public Lab Lab { get; set; } = new();
    public List<SkillCheckQuestion> SkillCheck { get; set; } = new();
    public ModuleReview Review { get; set; } = new();
}

