using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IssuesClient.Models;

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
        DoActionWithLoadingScreen(async () =>
        {
            Reports = Array.Empty<Report>();
            await Task.Delay(100);
        });

}
