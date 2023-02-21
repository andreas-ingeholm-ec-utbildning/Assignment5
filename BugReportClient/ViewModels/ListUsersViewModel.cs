﻿using System;
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
    public IEnumerable<User> list = null!;

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

        List = await UserService.GetAllAsync();
        if (List.Any())
            SelectedUser = Math.Clamp(selectedIndex ?? 0, 0, List.Count() - 1);
        else
            SelectedUser = null;

        IsBusy = false;

    }

    [RelayCommand]
    public void OpenAddPopup() =>
        Parent.OpenAddUserPopup(callbackOnClose: Reload);

    [RelayCommand]
    public async void DeleteSelectedUser()
    {

        if (List is null || SelectedUser is null)
            return;

        var i = SelectedUser;

        IsBusy = true;
        await UserService.DeleteAsync(List.ElementAt(SelectedUser.Value));

        Reload(i);

    }

}
