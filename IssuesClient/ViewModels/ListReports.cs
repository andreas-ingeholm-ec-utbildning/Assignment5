using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IssuesClient.Models;
using IssuesClient.Services;

namespace IssuesClient.ViewModels;

public partial class ListReports : ViewModel
{

    public ListReports() =>
        Reload();

    [ObservableProperty]
    private IEnumerable<Report> m_reports = Array.Empty<Report>();

    [ObservableProperty]
    private int m_selectedIndex;

    [RelayCommand]
    void AddReport() =>
        Redirect<AddReport>();

    [RelayCommand]
    void ViewReport(Report report) =>
        Redirect<ViewReport>(report);

    [RelayCommand]
    void Reload() =>
        _ = DoActionWithLoadingScreen(async () => Reports = (await ReportService.GetAllAsync()).OrderBy(r => r.Created));

    public override void OnRedirectDone() =>
        Reload();

}
