using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IssuesClient.Models;
using IssuesClient.Services;

namespace IssuesClient.ViewModels;

public partial class AddReport : ViewModel
{

    public override string Title => "Issue browser - Add report";

    public override void OnOpen(bool comingFromRedirect) =>
        ReloadUsers();

    void ReloadUsers() =>
        DoActionWithLoadingScreen(async () => Users = (await UserService.GetAllAsync()).ToArray());

    #region Properties

    [ObservableProperty] private User[] m_users = Array.Empty<User>();

    [NotifyDataErrorInfo]
    [ObservableProperty]
    [Required]
    private User? m_user = null!;

    [NotifyDataErrorInfo]
    [ObservableProperty]
    [Required]
    [MinLength(5, ErrorMessage = "Title must be at least 5 characters long.")]
    private string m_reportTitle = null!;

    [NotifyDataErrorInfo]
    [ObservableProperty]
    [Required]
    [MinLength(5, ErrorMessage = "Comment must be at least 5 characters long.")]
    private string m_comment = null!;

    #endregion
    #region Commands

    [RelayCommand]
    async void Add()
    {

        if (User is null)
            return;

        var report = new Report() { Title = ReportTitle, Created = DateTime.Now, User = User };
        report.Comments.Add(new() { Content = Comment, Created = DateTime.Now });

        await ReportService.CreateAsync(report);

        GoBack();

    }

    [RelayCommand]
    void Cancel() =>
        GoBack();

    [RelayCommand]
    void FillTestData()
    {

        User = Users.FirstOrDefault();
        ReportTitle = $"This is my {ReportService.CachedCount + 1} report! Please fix!!";
        Comment = $"I've already made {ReportService.CachedCount + 1} reports! Please fix!!";

    }

    [RelayCommand]
    void AddUser() =>
        Redirect<AddUser>();

    #endregion

}
