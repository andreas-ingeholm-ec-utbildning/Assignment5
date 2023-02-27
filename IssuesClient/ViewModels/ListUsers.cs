using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace IssuesClient.ViewModels;

public partial class ListUsers : ViewModel
{

    [ObservableProperty]
    private IEnumerable<ViewReport> m_users = Array.Empty<ViewReport>();

    [ObservableProperty]
    private int m_selectedIndex;

    [RelayCommand]
    void AddUser() =>
        Redirect<AddUser>();

    [RelayCommand]
    void RemoveUser()
    {
        //TODO: Support removing user
    }

}
