using System;
using System.Collections.Generic;
using System.Linq;
using BugReportClient.Models;
using BugReportClient.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BugReportClient.ViewModels;

public partial class ListBugReportsViewModel : ViewModel
{

    public ListBugReportsViewModel() =>
        Reload();

    public override string Title => "Bug reports";

    [ObservableProperty]
    public IEnumerable<BugReport> reports = null!;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanDelete))]
    public int? selectedReport;

    public bool CanDelete => SelectedReport.HasValue;

    [RelayCommand]
    public void Reload() =>
        Reload(null);

    public async void Reload(int? selectedIndex)
    {

        IsBusy = true;
        selectedIndex ??= SelectedReport ?? 0;

        Reports = await BugReportService.GetAllAsync();
        SelectedReport =
            Reports.Any()
            ? Math.Clamp(selectedIndex ?? 0, 0, Reports.Count() - 1)
            : null;

        IsBusy = false;

    }

    [RelayCommand]
    public void AddReport() =>
        Parent.OpenAddBugReportPopup(Reload);

    [RelayCommand]
    public async void DeleteSelectedReport()
    {

        if (Reports is null || SelectedReport is null)
            return;

        var i = SelectedReport;

        IsBusy = true;
        await BugReportService.DeleteAsync(Reports.ElementAt(SelectedReport.Value));

        Reload(i);

    }
}
