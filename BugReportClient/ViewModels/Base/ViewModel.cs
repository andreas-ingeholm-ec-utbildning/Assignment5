using CommunityToolkit.Mvvm.ComponentModel;

namespace BugReportClient.ViewModels;

public class ViewModel : ObservableObject
{

    public MainViewModel Parent { get; private set; } = null!;

    public ViewModel SetParent(MainViewModel parent)
    {
        Parent = parent;
        return this;
    }

    public virtual string Title { get; } = string.Empty;

}
