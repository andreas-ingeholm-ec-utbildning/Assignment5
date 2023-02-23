using System;
using System.Collections.Generic;
using System.Linq;
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
    public IEnumerable<User> users = null!;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanDelete))]
    public int? selectedUser;

    public bool CanDelete => SelectedUser.HasValue;

    [RelayCommand]
    public void Reload() =>
        Reload(null);

    public async void Reload(int? selectedIndex)
    {

        IsBusy = true;
        selectedIndex ??= SelectedUser ?? 0;

        Users = await UserService.GetAllAsync();
        SelectedUser =
            Users.Any()
            ? Math.Clamp(selectedIndex ?? 0, 0, Users.Count() - 1)
            : null;

        IsBusy = false;

    }

    [RelayCommand]
    public void OpenAddPopup() =>
        Parent.OpenAddUserPopup(callbackOnClose: Reload);

    [RelayCommand]
    public async void DeleteSelectedUser()
    {

        if (Users is null || SelectedUser is null)
            return;

        var i = SelectedUser;

        IsBusy = true;
        await UserService.DeleteAsync(Users.ElementAt(SelectedUser.Value));

        Reload(i);

    }

}
