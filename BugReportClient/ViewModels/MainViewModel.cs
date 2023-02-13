using System;
using BugReportClient.ViewModels.Popups;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BugReportClient.ViewModels;

public partial class MainViewModel : ObservableObject
{

    /// <summary>The currently active view.</summary>
    [ObservableProperty]
    private ViewModel view = null!;

    /// <summary>The currently active popup view, if any.</summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasPopup))]
    private PopupViewModel? popup = null;

    public bool HasPopup => Popup is not null;

    public MainViewModel() =>
        GoToListBugReportsView();

    /// <summary>Sets the currently active view.</summary>
    public void SetView<T>() where T : ViewModel, new() =>
        View = new T().SetParent(this);

    public void SetPopup<T>(Action? callbackOnClose = null) where T : PopupViewModel, new() =>
        Popup = new T().OnClose(() => { ClosePopup(); callbackOnClose?.Invoke(); });

    [RelayCommand] public void GoToListBugReportsView() => SetView<ListBugReportsViewModel>();
    [RelayCommand] public void GoToListUsersView() => SetView<ListUsersViewModel>();
    [RelayCommand] public void GoToListAddressesView() => SetView<ListAddressesViewModel>();

    public void OpenAddUserPopup(Action? callbackOnClose = null) => SetPopup<AddUserViewModel>(callbackOnClose);
    public void OpenAddBugReportPopup(Action? callbackOnClose = null) => SetPopup<AddBugReportViewModel>(callbackOnClose);
    public void ClosePopup() => Popup = null;

}
