using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BugReportClient.Models;

public partial class User : ObservableValidator
{

    [ObservableProperty] public Guid id;
    [ObservableProperty] public string firstName = string.Empty;
    [ObservableProperty] public string lastName = string.Empty;
    [ObservableProperty] public string emailAddress = string.Empty;
    [ObservableProperty] public string streetName = string.Empty;
    [ObservableProperty] public int? streetNumber;
    [ObservableProperty] public string postalCode = string.Empty;
    [ObservableProperty] public string city = string.Empty;

    public override string ToString() =>
        $"{FirstName} {LastName}, {EmailAddress}";

}
