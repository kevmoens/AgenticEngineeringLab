namespace AgenticEngineeringLab.Services;

public class ProgressService
{
    private readonly LocalStorageService _storage;

    public ProgressService(LocalStorageService storage) => _storage = storage;

    public async Task MarkCompleteAsync(string moduleId) =>
        await _storage.SetItemAsync($"complete:{moduleId}", "true");

    public async Task<bool> IsCompleteAsync(string moduleId)
    {
        var val = await _storage.GetItemAsync($"complete:{moduleId}");
        return val == "true";
    }

    public async Task SaveChecklistStateAsync(string key, List<bool> state) =>
        await _storage.SetItemAsync(key, System.Text.Json.JsonSerializer.Serialize(state));

    public async Task<List<bool>> LoadChecklistStateAsync(string key, int count)
    {
        var json = await _storage.GetItemAsync(key);
        if (json == null) return Enumerable.Repeat(false, count).ToList();
        return System.Text.Json.JsonSerializer.Deserialize<List<bool>>(json)
               ?? Enumerable.Repeat(false, count).ToList();
    }

    public async Task SaveReflectionAsync(string moduleId, Dictionary<string, string> answers) =>
        await _storage.SetItemAsync($"reflection:{moduleId}", System.Text.Json.JsonSerializer.Serialize(answers));

    public async Task<Dictionary<string, string>> LoadReflectionAsync(string moduleId)
    {
        var json = await _storage.GetItemAsync($"reflection:{moduleId}");
        if (json == null) return new();
        return System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new();
    }
}
