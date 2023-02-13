namespace BugReportClient.Models;

public class BugReport
{

    public int Id { get; set; }
    public User User { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

}
