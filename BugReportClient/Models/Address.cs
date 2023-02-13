using CommunityToolkit.Mvvm.ComponentModel;

namespace BugReportClient.Models;

public partial class Address : ObservableValidator
{

    [ObservableProperty] public int id;

    [ObservableProperty] public string streetName = string.Empty;
    [ObservableProperty] public int streetNumber;
    [ObservableProperty] public string postalCode;
    [ObservableProperty] public string city = string.Empty;

}
