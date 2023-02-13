using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BugReportClient.ViewModels;

public partial class PopupViewModel : ObservableObject
{

    private Action closeCallback = null!;
    public PopupViewModel OnClose(Action callback)
    {
        closeCallback = callback;
        return this;
    }

    [RelayCommand]
    public void Close()
    {
        if (closeCallback is null)
            throw new InvalidOperationException("Close callback was not set.");
        closeCallback.Invoke();
    }

}