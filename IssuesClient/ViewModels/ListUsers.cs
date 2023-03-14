using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IssuesClient.Models;
using IssuesClient.Services;

namespace IssuesClient.ViewModels;

public partial class ListUsers : ViewModel
{

    public override string Title => "Issue browser - Users";

    public ListUsers() =>
       _ = Reload();

    [ObservableProperty]
    private IEnumerable<User> m_users = Array.Empty<User>();

    [RelayCommand]
    void AddUser() =>
        Redirect<AddUser>();

    [RelayCommand]
    Task Reload() =>
      DoActionWithLoadingScreen(async () => Users = (await UserService.GetAllAsync()));

    [RelayCommand]
    void RemoveUser(User user) =>
        _ = DoActionWithLoadingScreen(async () =>
        {
            await UserService.RemoveAsync(user);
            await Reload();
        });

    public override void OnRedirectDone() =>
        _ = Reload();

}
