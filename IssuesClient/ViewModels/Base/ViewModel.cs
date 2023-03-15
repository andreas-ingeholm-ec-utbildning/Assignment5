using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace IssuesClient.ViewModels;

public abstract partial class ViewModel : ObservableValidator
{

    public virtual string Title { get; } = "Issue browser";

    public virtual void OnOpen(bool comingFromRedirect)
    { }

    public ViewModel Initialize(object? parameter, bool isRedirect, Action<Type, object?> redirect, Action goBackCallback)
    {

        if (isInitialized)
            return this;
        isInitialized = true;

        this.RedirectParameter = parameter;
        this.IsRedirect = isRedirect;
        this.redirect = redirect;
        this.goBackCallback = goBackCallback;

        ErrorsChanged += AddReport_ErrorsChanged;
        ValidateAllProperties();

        OnOpen(false);

        return this;

    }

    [ObservableProperty]
    private string[] m_errors = Array.Empty<string>();

    void AddReport_ErrorsChanged(object? sender, DataErrorsChangedEventArgs e) =>
        Errors = GetErrors().Select(v => v.ErrorMessage).Where(s => !string.IsNullOrWhiteSpace(s)).OfType<string>().ToArray();

    #region Redirect

    public bool IsRedirect { get; private set; }
    public object? RedirectParameter { get; private set; }

    /// <summary>Redirects to a different view, then returns back to this view (maintaining data).</summary>
    public void Redirect<T>(object? parameter = null) where T : ViewModel, new() =>
        redirect.Invoke(typeof(T), parameter);

    public void GoBack() =>
        goBackCallback.Invoke();

    bool isInitialized;
    Action<Type, object?> redirect = null!;
    Action goBackCallback = null!;

    #endregion
    #region DoActionWithLoadingScreen

    [ObservableProperty] private bool m_isBusy;

    protected async void DoActionWithLoadingScreen(Func<Task> task)
    {

        IsBusy = true;

        await Task.Delay(100);
        await task.Invoke();
        await Task.Delay(100);

        IsBusy = false;

    }

    #endregion

}
