using System.Collections.Generic;
using BugReportClient.Models;
using BugReportClient.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BugReportClient.ViewModels;

public partial class ListAddressesViewModel : ViewModel
{

    public ListAddressesViewModel() =>
        Reload();

    public override string Title => "Bug reports - Addresses";

    [ObservableProperty]
    public IEnumerable<Address> addresses = null!;

    [RelayCommand]
    public async void Reload()
    {
        IsBusy = true;
        Addresses = await AddressService.GetAllAsync();
        IsBusy = false;
    }

}
