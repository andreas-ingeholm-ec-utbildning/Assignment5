using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace IssuesClient.ViewModels;

public abstract class ViewModel : ObservableValidator
{

    public bool IsRedirect { get; private set; }
    public object? Parameter { get; private set; }

    /// <summary>Redirects to a different view, then returns back to this view (maintaining data).</summary>
    public void Redirect<T>(object? parameter = null) where T : ViewModel, new() =>
        redirect.Invoke(typeof(T), parameter);

    public void GoBack() =>
        goBackCallback.Invoke();

    bool isInitialized;
    Action<Type, object?> redirect = null!;
    Action goBackCallback = null!;

    public ViewModel Initialize(object? parameter, bool isRedirect, Action<Type, object?> redirect, Action goBackCallback)
    {

        if (isInitialized)
            return this;
        isInitialized = true;

        this.Parameter = parameter;
        this.IsRedirect = isRedirect;
        this.redirect = redirect;
        this.goBackCallback = goBackCallback;

        return this;

    }

}
