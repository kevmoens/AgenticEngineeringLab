using System.Text.Json;
using System.Text.Json.Serialization;

namespace AgenticEngineeringLab.Models.Converters;

/// <summary>
/// Serializes multi-line markdown strings as JSON arrays of lines so that
/// git diffs show individual changed lines instead of one giant changed string.
/// Deserializes both array-of-strings (canonical) and plain-string (fallback)
/// formats so that PowerShell-edited files still load correctly.
/// </summary>
public class MarkdownLinesConverter : JsonConverter<string>
{
    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.StartArray)
        {
            var lines = new List<string>();
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                lines.Add(reader.GetString() ?? "");
            return string.Join("\n", lines);
        }

        // Fallback: plain string (written by PowerShell or older tooling)
        return reader.GetString() ?? "";
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        foreach (var line in value.Split('\n'))
            writer.WriteStringValue(line);
        writer.WriteEndArray();
    }
}
