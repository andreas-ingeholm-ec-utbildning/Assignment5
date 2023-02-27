using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IssuesClient.Models;

namespace IssuesClient.ViewModels;

public partial class ListReports : ViewModel
{

    [ObservableProperty]
    private IEnumerable<ViewReport> m_reports = Array.Empty<ViewReport>();

    [ObservableProperty]
    private int m_selectedIndex;

    [RelayCommand]
    void AddReport() =>
        Redirect<AddReport>();

    [RelayCommand]
    void ViewReport(Report report) =>
        Redirect<ViewReport>(report);

}
