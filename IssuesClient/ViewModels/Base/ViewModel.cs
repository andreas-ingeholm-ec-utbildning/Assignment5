using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace IssuesClient.ViewModels;

public abstract partial class ViewModel : ObservableValidator
{

    public virtual string Title { get; } = "Issue browser";

    #region Redirect

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

    public virtual void OnRedirectDone()
    { }

    #endregion
    #region DoActionWithLoadingScreen

    [ObservableProperty] private bool m_isBusy;

    protected async void DoActionWithLoadingScreen(Func<Task> task)
    {

        IsBusy = true;
        await Task.Delay(100);

        var t = task.Invoke();

        if (t.Status == TaskStatus.WaitingToRun)
            t.Start();
        await t;

        IsBusy = false;

    }

    #endregion

}
