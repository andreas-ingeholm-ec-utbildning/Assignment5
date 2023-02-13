using CommunityToolkit.Mvvm.ComponentModel;

namespace BugReportClient.Models;

public partial class User : ObservableValidator
{

    [ObservableProperty] public int id;
    [ObservableProperty] public string firstName = string.Empty;
    [ObservableProperty] public string lastName = string.Empty;
    [ObservableProperty] public string emailAddress = string.Empty;
    [ObservableProperty] public Address address = null!;

}
