using System.Net.Http.Json;
using AgenticEngineeringLab.Models;

namespace AgenticEngineeringLab.Services;

public class ContentService
{
    private readonly HttpClient _http;
    private readonly List<Module> _cache = new();
    private List<ModuleSummary>? _moduleListCache;

    public ContentService(HttpClient http) => _http = http;

    public async Task<Module?> GetModuleAsync(int moduleNumber)
    {
        var cached = _cache.FirstOrDefault(m => m.ModuleNumber == moduleNumber);
        if (cached != null) return cached;

        var moduleSummaries = await GetModuleListAsync();
        var moduleSummary = moduleSummaries.FirstOrDefault(m => m.ModuleNumber == moduleNumber);
        var contentFile = ResolveContentFile(moduleSummary, moduleNumber);

        var options = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
        };

        var module = await _http.GetFromJsonAsync<Module>($"content/{contentFile}", options);

        if (module != null) _cache.Add(module);
        return module;
    }

    public async Task<Module?> GetModuleBySequenceAsync(int sequence)
    {
        var moduleSummaries = await GetModuleListAsync();
        var moduleSummary = moduleSummaries.FirstOrDefault(m =>
            (m.Sequence > 0 ? m.Sequence : m.ModuleNumber) == sequence);

        if (moduleSummary == null)
        {
            // Backward-compatibility fallback for legacy /module/{moduleNumber} URLs.
            return await GetModuleAsync(sequence);
        }

        return await GetModuleAsync(moduleSummary.ModuleNumber);
    }

    public async Task<List<ModuleSummary>> GetModuleListAsync()
    {
        if (_moduleListCache != null)
        {
            return _moduleListCache.ToList();
        }

        var options = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var modules = await _http.GetFromJsonAsync<List<ModuleSummary>>("content/modules.json", options) ?? new();

        _moduleListCache = modules
            .OrderBy(m => m.Sequence > 0 ? m.Sequence : int.MaxValue)
            .ThenBy(m => m.ModuleNumber)
            .ToList();

        return _moduleListCache.ToList();
    }

    private static string ResolveContentFile(ModuleSummary? moduleSummary, int moduleNumber)
    {
        var contentFile = moduleSummary?.ContentFile;
        if (string.IsNullOrWhiteSpace(contentFile))
        {
            return $"module-{moduleNumber:D2}.json";
        }

        contentFile = contentFile.Replace("\\", "/");
        return contentFile.StartsWith("content/", StringComparison.OrdinalIgnoreCase)
            ? contentFile["content/".Length..]
            : contentFile;
    }
}
