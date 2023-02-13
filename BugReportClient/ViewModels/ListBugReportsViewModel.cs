using System.Collections.ObjectModel;
using BugReportClient.Models;
using CommunityToolkit.Mvvm.Input;

namespace BugReportClient.ViewModels;

public partial class ListBugReportsViewModel : ViewModel
{

    public override string Title => "Bug reports";

    public ObservableCollection<BugReport> list;

    [RelayCommand]
    public void Reload()
    {

    }

}
