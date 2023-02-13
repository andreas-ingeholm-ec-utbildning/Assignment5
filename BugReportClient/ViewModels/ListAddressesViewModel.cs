using System.Collections.ObjectModel;
using BugReportClient.Models;
using CommunityToolkit.Mvvm.Input;

namespace BugReportClient.ViewModels;

public partial class ListAddressesViewModel : ViewModel
{

    public override string Title => "Bug reports - Addresses";

    public ObservableCollection<BugReport> list;

    [RelayCommand]
    public void Reload()
    {

    }

}
