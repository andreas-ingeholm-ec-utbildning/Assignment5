using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IssuesClient.Models;
using IssuesClient.Services;

namespace IssuesClient.ViewModels;

public partial class ListReports : ViewModel
{

    public override void OnOpen(bool comingFromRedirect) =>
        Reload();

    #region Properties

    [ObservableProperty]
    private IEnumerable<Report> m_reports = Array.Empty<Report>();

    [ObservableProperty]
    private int m_selectedIndex;

    #endregion
    #region Commands

    [RelayCommand]
    void AddReport() => Redirect<AddReport>();

    [RelayCommand]
    void ViewReport(Report report) => Redirect<ViewReport>(report);

    [RelayCommand]
    void Reload() => DoActionWithLoadingScreen(async () => Reports = await ReportService.GetAllAsync());

    #endregion

}
