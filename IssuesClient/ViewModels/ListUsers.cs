using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IssuesClient.Models;
using IssuesClient.Services;

namespace IssuesClient.ViewModels;

public partial class ListUsers : ViewModel
{

    public override string Title => "Issue browser - Users";

    public override void OnOpen(bool comingFromRedirect) =>
        Reload();

    [ObservableProperty]
    private IEnumerable<User> m_users = Array.Empty<User>();

    [RelayCommand]
    void Reload() => DoActionWithLoadingScreen(async () => Users = await UserService.GetAllAsync());

    [RelayCommand]
    void AddUser() => Redirect<AddUser>();

    [RelayCommand]
    void RemoveUser(User user) =>
        DoActionWithLoadingScreen(async () =>
        {
            await UserService.RemoveAsync(user);
            Users = await UserService.GetAllAsync();
        });

}
