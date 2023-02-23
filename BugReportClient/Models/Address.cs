using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugReportClient.Models;

public partial class Address : ObservableValidator
{

    [ObservableProperty] public Guid id;

    [ObservableProperty] public string streetName = string.Empty;
    [ObservableProperty] public int? streetNumber;
    [ObservableProperty] public string postalCode = string.Empty;
    [ObservableProperty] public string city = string.Empty;

}
