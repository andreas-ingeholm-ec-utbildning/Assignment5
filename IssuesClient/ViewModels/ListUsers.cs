using System;
using System.Collections.Generic;
using System.Linq;
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
        Task.Run(Reload);

    [ObservableProperty]
    private IEnumerable<User> m_users = Array.Empty<User>();

    [ObservableProperty]
    private int m_selectedIndex;

    [RelayCommand]
    void AddUser() =>
        Redirect<AddUser>();

    [RelayCommand]
    void Reload() =>
        DoActionWithLoadingScreen(ReloadInternal);

    async Task ReloadInternal()
    {
        var service = new UserService();
        var sd = (await service.GetAll()).ToArray();
        Users = (sd).Select(u => (User)u);
    }

    [RelayCommand]
    void RemoveUser(User user) =>
        DoActionWithLoadingScreen(async () =>
        {
            var service = new UserService();
            await service.Remove(user, "");
            await ReloadInternal();
        });

    public override void OnRedirectDone()
    {
        Reload();
    }

}
