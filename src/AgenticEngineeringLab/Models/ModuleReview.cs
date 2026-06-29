using System.Text.Json.Serialization;
using AgenticEngineeringLab.Models.Converters;

namespace AgenticEngineeringLab.Models;

public class ModuleReview
{
    [JsonConverter(typeof(MarkdownLinesConverter))]
    public string SummaryMarkdown { get; set; } = "";
    public List<string> ReadinessChecklist { get; set; } = new();
    public List<string> CommonMistakes { get; set; } = new();
    public string NextModuleTeaser { get; set; } = "";
}
