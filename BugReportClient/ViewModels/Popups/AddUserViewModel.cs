using BugReportClient.Models;
using BugReportClient.Services;
using CommunityToolkit.Mvvm.Input;

namespace BugReportClient.ViewModels.Popups;

public partial class AddUserViewModel : PopupViewModel
{

    public User User { get; } = new() { Address = new() };

    [RelayCommand]
    public async void Save()
    {
        await UserService.Save(User);
        Close();
    }

    [RelayCommand]
    public void FillTestData()
    {
        User.FirstName = "Test" + UserService.CachedListCount;
        User.LastName = "Testsson";
        User.EmailAddress = $"test{UserService.CachedListCount}.testsson@gmail.com";
        User.Address.StreetName = "Testå";
        User.Address.StreetNumber = 4;
        User.Address.PostalCode = "111111";
        User.Address.City = "Teststad";
    }

}
