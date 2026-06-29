namespace AgenticEngineeringLab.Models;

public class ModuleSummary
{
    public int ModuleNumber { get; set; }
    public string ShortTitle { get; set; } = "";
    public string Title { get; set; } = "";
    public string Id { get; set; } = "";
    public int Sequence { get; set; }
    public string ContentFile { get; set; } = "";
    public string SidebarSection { get; set; } = "";
    public int SidebarOrder { get; set; }
}
