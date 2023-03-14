namespace IssuesClient.Services;

static class DBService
{
    public static DataContext Context { get; } = new();
}
