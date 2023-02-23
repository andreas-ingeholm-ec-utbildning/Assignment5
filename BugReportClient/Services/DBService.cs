using BugReportClient.Contexts;

namespace BugReportClient.Services;

public static class DBService
{
    public static DataContext Context { get; } = new();
}
