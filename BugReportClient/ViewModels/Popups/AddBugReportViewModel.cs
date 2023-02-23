using System;
using System.Collections.Generic;
using System.Linq;
using BugReportClient.Models;
using BugReportClient.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BugReportClient.ViewModels.Popups;

public partial class AddBugReportViewModel : PopupViewModel
{

    public AddBugReportViewModel() =>
        _ = UserService.GetAllAsync().ContinueWith(t => Users = t.Result);

    public BugReport Report { get; } = new();

    [ObservableProperty]
    public IEnumerable<User> users = Array.Empty<User>();

    [RelayCommand]
    public async void Save()
    {
        await BugReportService.SaveAsync(Report);
        Close();
    }

    [RelayCommand]
    public void FillTestData()
    {
        Report.Title = "Test" + BugReportService.CachedListCount;
        Report.Content = $"This is a bug report. This is the {BugReportService.CachedListCount + 1} time I've reported this, please respond.";
        Report.User = Users.FirstOrDefault()!;
    }

}
