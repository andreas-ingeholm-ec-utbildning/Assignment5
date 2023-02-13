using System.Collections.Generic;
using BugReportClient.Models;
using BugReportClient.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BugReportClient.ViewModels;

public partial class ListUsersViewModel : ViewModel
{

    public ListUsersViewModel() =>
        Reload();

    public override string Title => "Bug reports - Users";

    [ObservableProperty]
    public IEnumerable<User> list = null!;

    [RelayCommand]
    public void Reload() =>
        List = UserService.ListUsers();

    [RelayCommand]
    public void OpenAddPopup() =>
        Parent.OpenAddUserPopup(callbackOnClose: Reload);

}
