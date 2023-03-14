using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IssuesClient.Models;
using IssuesClient.Services;

namespace IssuesClient.ViewModels;

public partial class ViewReport : ViewModel
{

    public override void OnOpen()
    {

        ErrorsChanged += AddReport_ErrorsChanged;
        ValidateAllProperties();

        _ = Reload(RedirectParameter as Report);

    }

    void AddReport_ErrorsChanged(object? sender, DataErrorsChangedEventArgs e) =>
        Errors = GetErrors().Select(v => v.ErrorMessage).Where(s => !string.IsNullOrWhiteSpace(s)).OfType<string>().ToArray();

    [ObservableProperty]
    private string[] m_errors = Array.Empty<string>();

    [ObservableProperty]
    private Report m_report = null!;

    [NotifyDataErrorInfo]
    [ObservableProperty]
    [MinLength(5)]
    [Required]
    private string m_comment = null!;

    public override string Title => "Issue browser - Report";

    [RelayCommand]
    void AddComment() =>
       _ = DoActionWithLoadingScreen(async () =>
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
    Task Reload() =>
        Reload(null);

    Task Reload(Report? report = null) =>
      DoActionWithLoadingScreen(async () =>
      {

          report ??= await ReportService.GetAsync(Report.Id);
          Comment = "";

          if (report is not null)
              Report = report;
          else
          {
              _ = MessageBox.Show("Could not open report, please try again.");
              GoBack();
          }

      });

    [RelayCommand]
    void AddUser() =>
        Redirect<AddUser>();

    public override void OnRedirectDone() =>
        Reload();

}