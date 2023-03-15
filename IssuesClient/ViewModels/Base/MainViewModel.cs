using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace IssuesClient.ViewModels;

public partial class MainViewModel : ObservableObject
{

    public MainViewModel() =>
        OpenDefault();

    [ObservableProperty]
    private ViewModel m_viewModel = null!;

    public void Open<T>() where T : ViewModel, new()
    {
        redirectCallers.Clear();
        OpenInternal(new T());
    }

    readonly Stack<ViewModel> redirectCallers = new();

    void Redirect(Type redirectTo, object? parameter, ViewModel returnTo)
    {

        if (!typeof(ViewModel).IsAssignableFrom(redirectTo))
            throw new ArgumentException("Cannot redirect to " + redirectTo?.Name + ", it is not a valid view model.");

        redirectCallers.Push(returnTo);
        OpenInternal((ViewModel)Activator.CreateInstance(redirectTo)!, parameter, isRedirect: true);

    }

    void GoBack()
    {
        if (redirectCallers.TryPop(out var viewModel))
        {
            OpenInternal(viewModel);
            viewModel.OnOpen(true);
        }
        else
            OpenDefault();
    }

    void OpenInternal(ViewModel viewModel, object? parameter = null, bool isRedirect = false) =>
        ViewModel = viewModel.Initialize(parameter, isRedirect, (t, p) => Redirect(t, p, viewModel), GoBack);

    void OpenDefault() => Open<ListReports>();
    [RelayCommand] public void ViewReports() => Open<ListReports>();
    [RelayCommand] public void ViewUsers() => Open<ListUsers>();

}
