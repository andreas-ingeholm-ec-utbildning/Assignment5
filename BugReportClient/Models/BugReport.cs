using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugReportClient.Models;

public partial class BugReport : ObservableValidator
{

    [ObservableProperty] public Guid id;
    [ObservableProperty] public string title = string.Empty;
    [ObservableProperty] public string content = string.Empty;
    [ObservableProperty] public User user = null!;

}
