using System.Text.Json.Serialization;
using AgenticEngineeringLab.Models.Converters;

namespace AgenticEngineeringLab.Models;

public class PromptCard
{
    public string Label { get; set; } = "";

    [JsonConverter(typeof(MarkdownLinesConverter))]
    public string IntroMarkdown { get; set; } = "";

    [JsonConverter(typeof(MarkdownLinesConverter))]
    public string PromptText { get; set; } = "";
    public bool IsOptional { get; set; }
}
