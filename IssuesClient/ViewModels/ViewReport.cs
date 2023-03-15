using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IssuesClient.Models;
using IssuesClient.Services;

namespace IssuesClient.ViewModels;

public partial class ViewReport : ViewModel
{

    public override string Title =>
        string.IsNullOrWhiteSpace(Report?.Title)
        ? "Issue browser"
        : "Issue browser - " + Report.Title;

    public override void OnOpen(bool comingFromRedirect) =>
        ReloadWithLoadingScreen(RedirectParameter as Report);

    void ReloadWithLoadingScreen(Report? report = null) =>
      DoActionWithLoadingScreen(() => Reload(report));

    async Task Reload(Report? report = null)
    {

        report ??= await ReportService.GetAsync(Report.Id);
        Comment = "";

        if (report is not null)
        {
            Report = report;
            ReportStatus = report.Status;
            OnPropertyChanged(nameof(HasReportStatusChanged));
        }
        else
        {
            _ = MessageBox.Show("Could not open report, please try again.");
            GoBack();
        }

    }

    #region Properties

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Title))]
    private Report m_report = null!;

    [NotifyDataErrorInfo]
    [ObservableProperty]
    [MinLength(5)]
    [Required]
    private string m_comment = null!;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasReportStatusChanged))]
    private ReportStatus m_reportStatus;

    public bool HasReportStatusChanged => ReportStatus != Report?.Status;

    #endregion
    #region Commands

    [RelayCommand]
    void ReloadCommand() =>
        ReloadWithLoadingScreen(null);

    [RelayCommand]
    void AddComment() =>
       DoActionWithLoadingScreen(async () =>
       {
           await ReportService.AddCommentAsync(Report, new() { Content = Comment, Created = DateTime.Now });
           await Reload();
       });

    [RelayCommand]
    async void Remove()
    {
        await ReportService.RemoveAsync(Report);
        GoBack();
    }

    [RelayCommand]
    void UpdateStatus() =>
       DoActionWithLoadingScreen(async () =>
       {
           await ReportService.SetStatusAsync(Report, ReportStatus);
           await Reload();
       });

    [RelayCommand]
    void AddUser() =>
        Redirect<AddUser>();

    #endregion

}