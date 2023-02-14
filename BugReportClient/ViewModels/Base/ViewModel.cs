using CommunityToolkit.Mvvm.ComponentModel;

namespace BugReportClient.ViewModels;

public partial class ViewModel : ObservableObject
{

    public MainViewModel Parent { get; private set; } = null!;
    [ObservableProperty] private bool isBusy;

    public ViewModel SetParent(MainViewModel parent)
    {
        Parent = parent;
        return this;
    }

    public virtual string Title { get; } = string.Empty;

}
